using System;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WindowsApp.Services;

namespace WindowsApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool isBusy;

        private string password;
        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        public IRelayCommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin()
        {
            return !IsBusy && 
                   !string.IsNullOrWhiteSpace(Username) && 
                   !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin()
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                if (DatabaseService.AuthenticateUser(Username, Password))
                {
                    var currentWindow = Application.Current.Windows[0];
                    NavigationService.NavigateToDashboard(currentWindow);
                }
                else
                {
                    ErrorMessage = "اسم المستخدم أو كلمة المرور غير صحيحة";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "حدث خطأ أثناء محاولة تسجيل الدخول";
                NavigationService.ShowError($"تفاصيل الخطأ: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        public void ClearForm()
        {
            Username = string.Empty;
            Password = string.Empty;
            ErrorMessage = string.Empty;
            IsBusy = false;
        }
    }
}
