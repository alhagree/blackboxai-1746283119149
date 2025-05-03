using System;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WindowsApp.Services;

namespace WindowsApp.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private string welcomeMessage = "مرحباً بك في لوحة التحكم";

        [ObservableProperty]
        private string currentTime;

        [ObservableProperty]
        private string currentUser;

        public IRelayCommand LogoutCommand { get; }
        public IRelayCommand RefreshCommand { get; }

        public DashboardViewModel()
        {
            LogoutCommand = new RelayCommand(ExecuteLogout);
            RefreshCommand = new RelayCommand(ExecuteRefresh);
            
            // تحديث الوقت الحالي
            UpdateCurrentTime();
            
            // تعيين اسم المستخدم الحالي
            CurrentUser = "المستخدم الحالي"; // في التطبيق الحقيقي، سيتم جلب اسم المستخدم من الجلسة
        }

        private void ExecuteLogout()
        {
            if (NavigationService.ShowConfirmation("هل أنت متأكد من تسجيل الخروج؟"))
            {
                var currentWindow = Application.Current.Windows[0];
                NavigationService.NavigateToLogin(currentWindow);
            }
        }

        private void ExecuteRefresh()
        {
            UpdateCurrentTime();
        }

        private void UpdateCurrentTime()
        {
            CurrentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
