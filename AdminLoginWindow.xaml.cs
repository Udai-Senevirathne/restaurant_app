using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace POSSystem
{
    public partial class AdminLoginWindow : Window
    {
        // Default admin credentials (in real app, these would be stored securely)
        private const string ADMIN_USERNAME = "admin";
        private const string ADMIN_PASSWORD = "admin123";

        public bool IsAuthenticated { get; private set; } = false;

        public AdminLoginWindow()
        {
            InitializeComponent();
            
            // Set focus to username field
            Loaded += (s, e) => 
            {
                UsernameTextBox.Focus();
                PlayWelcomeAnimation();
            };
            
            // Center window on owner with enhanced positioning
            if (Owner != null)
            {
                Left = Owner.Left + (Owner.Width - Width) / 2;
                Top = Owner.Top + (Owner.Height - Height) / 2;
            }
        }

        private void PlayWelcomeAnimation()
        {
            // Add entrance animation for better UX
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(800));
            BeginAnimation(OpacityProperty, fadeIn);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            PerformLogin();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Add exit animation
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            fadeOut.Completed += (s, args) => Close();
            BeginAnimation(OpacityProperty, fadeOut);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Add exit animation
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            fadeOut.Completed += (s, args) => Close();
            BeginAnimation(OpacityProperty, fadeOut);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void HeaderBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            // Enhanced keyboard navigation
            if (e.Key == Key.Enter)
            {
                PerformLogin();
            }
            else if (e.Key == Key.Tab)
            {
                if (sender == UsernameTextBox)
                {
                    PasswordBox.Focus();
                    e.Handled = true;
                }
                else if (sender == PasswordBox)
                {
                    LoginButton.Focus();
                    e.Handled = true;
                }
            }
            else if (e.Key == Key.Escape)
            {
                CancelButton_Click(null, null);
            }
        }

        private void PerformLogin()
        {
            var username = UsernameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            // Hide previous error message with animation
            HideError();

            // Enhanced input validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("?? Please enter both username and password to proceed.");
                if (string.IsNullOrEmpty(username))
                    UsernameTextBox.Focus();
                else
                    PasswordBox.Focus();
                return;
            }

            // Add visual feedback during authentication
            LoginButton.IsEnabled = false;
            LoginButton.Content = "?? Authenticating...";
            
            // Add loading animation to login button
            var pulseAnimation = new DoubleAnimation(1, 0.7, TimeSpan.FromMilliseconds(500))
            {
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            LoginButton.BeginAnimation(OpacityProperty, pulseAnimation);

            // Simulate authentication process with enhanced feedback
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1200); // Slightly longer for better UX
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                
                // Stop loading animation
                LoginButton.BeginAnimation(OpacityProperty, null);
                LoginButton.Opacity = 1;
                
                // Check credentials
                if (username.Equals(ADMIN_USERNAME, StringComparison.OrdinalIgnoreCase) && 
                    password == ADMIN_PASSWORD)
                {
                    // Authentication successful
                    IsAuthenticated = true;
                    
                    // Enhanced success feedback
                    LoginButton.Content = "? Success!";
                    LoginButton.Background = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(72, 187, 120));
                    
                    // Direct transition without popup delay
                    var successTimer = new System.Windows.Threading.DispatcherTimer();
                    successTimer.Interval = TimeSpan.FromMilliseconds(400); // Shorter delay
                    successTimer.Tick += (ss, ee) =>
                    {
                        successTimer.Stop();
                        
                        DialogResult = true;
                        
                        // Smooth exit animation
                        var exitAnimation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(400));
                        exitAnimation.Completed += (sss, eee) => Close();
                        BeginAnimation(OpacityProperty, exitAnimation);
                    };
                    successTimer.Start();
                }
                else
                {
                    // Authentication failed with enhanced error handling
                    ShowError("? Invalid credentials detected!\n\n" +
                             "?? Hint: Try the demo credentials shown above.");
                    
                    // Add shake animation to the window for visual feedback
                    var shakeAnimation = new DoubleAnimation()
                    {
                        From = Left,
                        To = Left + 10,
                        Duration = TimeSpan.FromMilliseconds(50),
                        AutoReverse = true,
                        RepeatBehavior = new RepeatBehavior(3)
                    };
                    BeginAnimation(LeftProperty, shakeAnimation);
                    
                    // Clear password field for security
                    PasswordBox.Clear();
                    PasswordBox.Focus();
                    
                    // Reset login button state
                    LoginButton.IsEnabled = true;
                    LoginButton.Content = "?? Sign In";
                    LoginButton.Background = null; // Reset to default style
                }
            };
            timer.Start();
        }

        private void ShowError(string message)
        {
            ErrorMessage.Text = message;
            ErrorBorder.Visibility = Visibility.Visible;
            
            // Enhanced error animation
            var scaleX = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300))
            {
                EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut }
            };
            var scaleY = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300))
            {
                EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut }
            };
            
            var errorScale = (System.Windows.Media.ScaleTransform)ErrorBorder.RenderTransform;
            if (errorScale == null)
            {
                errorScale = new System.Windows.Media.ScaleTransform();
                ErrorBorder.RenderTransform = errorScale;
                ErrorBorder.RenderTransformOrigin = new Point(0.5, 0.5);
            }
            
            errorScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty, scaleX);
            errorScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleYProperty, scaleY);
            
            // Auto-hide error after 6 seconds with fade animation
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(6);
            timer.Tick += (s, e) =>
            {
                HideError();
                timer.Stop();
            };
            timer.Start();
        }

        private void HideError()
        {
            if (ErrorBorder.Visibility == Visibility.Visible)
            {
                var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
                fadeOut.Completed += (s, e) =>
                {
                    ErrorBorder.Visibility = Visibility.Collapsed;
                    ErrorMessage.Text = "";
                    ErrorBorder.Opacity = 1; // Reset for next time
                };
                ErrorBorder.BeginAnimation(OpacityProperty, fadeOut);
            }
        }

        // Enhanced window dragging with smooth movement
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            
            // Only allow dragging if clicked on the window background, not on controls
            if (e.OriginalSource == this)
            {
                DragMove();
            }
        }

        // Enhanced window closing with proper cleanup
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            
            // If not authenticated and window is being closed, ensure DialogResult is false
            if (!IsAuthenticated)
            {
                DialogResult = false;
            }
        }

        // Add keyboard shortcuts for better accessibility
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            // Global keyboard shortcuts
            if (e.Key == Key.F1)
            {
                MessageBox.Show("?? Admin Login Help\n\n" +
                               "• Use Tab to navigate between fields\n" +
                               "• Press Enter to login\n" +
                               "• Press Escape to cancel\n" +
                               "• Demo credentials are shown below the form",
                               "Help", MessageBoxButton.OK, MessageBoxImage.Information);
                e.Handled = true;
            }
        }
    }
}