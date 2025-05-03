using System.Windows;
using WindowsApp.Services;

namespace WindowsApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                DatabaseService.InitializeDatabase();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"خطأ في تهيئة قاعدة البيانات: {ex.Message}", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
            base.OnStartup(e);
        }
    }
}
