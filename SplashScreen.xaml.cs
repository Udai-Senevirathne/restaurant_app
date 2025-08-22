using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace POSSystem
{
    public partial class SplashScreen : Window
    {
        private DispatcherTimer _timer;
        private DispatcherTimer _typingTimer;
        private int _progress = 0;
        private string _titleText = " '2' Eden";
        private int _currentCharIndex = 0;
        private bool _isTypingComplete = false;

        public SplashScreen()
        {
            InitializeComponent();
            StartLoading();
            StartTypingEffect();
        }

        private void StartLoading()
        {
            // Professional loading simulation for restaurant POS
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(120);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void StartTypingEffect()
        {
            // Initialize typing effect for the title
            _typingTimer = new DispatcherTimer();
            _typingTimer.Interval = TimeSpan.FromMilliseconds(150); // Typing speed
            _typingTimer.Tick += TypingTimer_Tick;
            _typingTimer.Start();
        }

        private void TypingTimer_Tick(object sender, EventArgs e)
        {
            if (_currentCharIndex < _titleText.Length)
            {
                // Add the next character to the title
                TypingTitle.Text += _titleText[_currentCharIndex];
                _currentCharIndex++;
            }
            else
            {
                // Typing is complete
                _typingTimer.Stop();
                _isTypingComplete = true;
                ShowSubtitle();
            }
        }

        private void ShowSubtitle()
        {
            // Animate subtitle appearance
            var fadeIn = new System.Windows.Media.Animation.DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(800)
            };
            SubtitleText.BeginAnimation(OpacityProperty, fadeIn);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _progress += 3;

            // Update progress bar width smoothly
            ProgressBar.Width = (_progress / 100.0) * 350;

            // Update professional loading text
            LoadingText.Text = GetLoadingText(_progress);

            // When loading is complete
            if (_progress >= 100)
            {
                _timer.Stop();
                CompleteLoading();
            }
        }

        private string GetLoadingText(int progress)
        {
            return progress switch
            {
                < 25 => "Welcome to 2nd Eden...",
                < 50 => "Preparing your dining experience...",
                < 75 => "Setting up your workspace...",
                < 95 => "Getting everything ready...",
                _ => "Welcome! Let's get started..."
            };
        }

        private void CompleteLoading()
        {
            // Ensure typing effect is complete before transitioning
            if (!_isTypingComplete)
            {
                // If typing isn't done, wait a bit more
                var delayTimer = new DispatcherTimer();
                delayTimer.Interval = TimeSpan.FromMilliseconds(500);
                delayTimer.Tick += (s, e) =>
                {
                    delayTimer.Stop();
                    TransitionToMainWindow();
                };
                delayTimer.Start();
            }
            else
            {
                TransitionToMainWindow();
            }
        }

        private void TransitionToMainWindow()
        {
            // Smooth transition to main POS window
            var mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            
            // Close splash screen gracefully
            this.Close();
        }
    }
}