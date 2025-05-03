using System.Windows;
using WindowsApp.Views;

namespace WindowsApp.Services
{
    public static class NavigationService
    {
        public static void NavigateToDashboard(Window currentWindow)
        {
            var dashboardWindow = new DashboardView();
            dashboardWindow.Show();
            currentWindow?.Close();
        }

        public static void NavigateToLogin(Window currentWindow)
        {
            var loginWindow = new LoginView();
            loginWindow.Show();
            currentWindow?.Close();
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "خطأ",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }

        public static bool ShowConfirmation(string message)
        {
            var result = MessageBox.Show(
                message,
                "تأكيد",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            
            return result == MessageBoxResult.Yes;
        }
    }
}
