using System.Windows;
using System.Windows.Controls;
using WindowsApp.ViewModels;

namespace WindowsApp.Views
{
    public partial class LoginView : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginView()
        {
            InitializeComponent();
            _viewModel = (LoginViewModel)DataContext;

            // ربط حدث تغيير كلمة المرور مع ViewModel
            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;

            // التركيز على حقل اسم المستخدم عند فتح النافذة
            Loaded += (s, e) => UsernameTextBox.Focus();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                // تحديث كلمة المرور في ViewModel
                _viewModel.Password = passwordBox.Password;
            }
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            
            // تنظيف البيانات الحساسة عند إغلاق النافذة
            _viewModel.ClearForm();
            PasswordBox.Clear();
        }
    }
}
