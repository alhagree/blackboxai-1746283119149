using System;
using System.Windows;
using System.Windows.Threading;
using WindowsApp.ViewModels;

namespace WindowsApp.Views
{
    public partial class DashboardView : Window
    {
        private readonly DashboardViewModel _viewModel;
        private readonly DispatcherTimer _timer;

        public DashboardView()
        {
            InitializeComponent();
            _viewModel = (DashboardViewModel)DataContext;

            // إنشاء مؤقت لتحديث الوقت كل ثانية
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // تحديث الوقت في ViewModel
            _viewModel.CurrentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            
            // إيقاف المؤقت عند إغلاق النافذة
            _timer.Stop();
        }
    }
}
