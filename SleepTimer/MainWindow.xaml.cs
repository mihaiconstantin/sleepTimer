using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace SleepTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _StartTimer();
        }

        // instance fields
        private int _startTime = 180;
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        // timer methods
        private void _StartTimer()
        {
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
        }
        private void _StopTimer()
        {
            _dispatcherTimer.Stop();
        }


        // tick method event
        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _startTime = _startTime - 1;

            lbCounter.Content = _startTime.ToString() + "'.";

            if (_startTime < 10)
                Activate();
            Focus();

            if (_startTime != 0)
                return;

            _StopTimer();
            _Sleep();
            _KillAppProcess();
        }


        // system methods
        private static void _Sleep()
        {
            System.Windows.Forms.Application.SetSuspendState(PowerState.Suspend, true, false);
        }

        private static void _KillAppProcess()
        {
            Environment.Exit(0);
        }

        // Event: Window_MouseDown -> makes the window dragable
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        // Event: btnCancel_Click
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _KillAppProcess();
        }

        // Event: btnAdd_Click
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _startTime = _startTime + 60;
        }
    }
}
