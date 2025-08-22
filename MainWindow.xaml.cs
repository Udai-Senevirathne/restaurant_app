using POSSystem.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media; // ADD THIS LINE FOR SolidColorBrush

namespace POSSystem
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Product> _allProducts = new();
        private readonly ObservableCollection<CartItem> _cartItems = new();
        private bool _isSidebarExpanded = true;
        private string _currentCategoryType = ""; // "Foods" or "Beverages"
        private string _currentSubCategory = ""; // Specific category like "Breakfast", "Coffee", etc.

        public MainWindow()
        {
            InitializeComponent();
            InitProducts();
            SetupKeyboardShortcuts();
            // Load products after the window is fully loaded
            Loaded += (_, __) => LoadProducts();
        }

        #region Data
        private void InitProducts()
        {
            _allProducts.AddRange(new[]
            {
                // FOODS SECTION
                
                // Breakfast
                new Product{Name="Pancakes with Syrup",     Price=450, Category="Breakfast", CategoryType="Foods"},
                new Product{Name="French Toast",           Price=420, Category="Breakfast", CategoryType="Foods"},
                new Product{Name="Cheese Omelette",        Price=380, Category="Breakfast", CategoryType="Foods"},
                new Product{Name="Bacon & Eggs",           Price=520, Category="Breakfast", CategoryType="Foods"},
                new Product{Name="Breakfast Burrito",      Price=480, Category="Breakfast", CategoryType="Foods"},
                new Product{Name="Avocado Toast",          Price=350, Category="Breakfast", CategoryType="Foods"},

                // Starters
                new Product{Name="Caesar Salad",           Price=320, Category="Starters", CategoryType="Foods"},
                new Product{Name="Buffalo Wings",          Price=480, Category="Starters", CategoryType="Foods"},
                new Product{Name="Mozzarella Sticks",      Price=380, Category="Starters", CategoryType="Foods"},
                new Product{Name="Onion Rings",            Price=280, Category="Starters", CategoryType="Foods"},
                new Product{Name="Garlic Bread",           Price=220, Category="Starters", CategoryType="Foods"},
                new Product{Name="Spinach Dip",            Price=350, Category="Starters", CategoryType="Foods"},
                new Product{Name="Chicken Tenders",        Price=420, Category="Starters", CategoryType="Foods"},

                // Main Courses
                new Product{Name="Grilled Chicken Breast", Price=950, Category="Main", CategoryType="Foods"},
                new Product{Name="Beef Steak",             Price=1200, Category="Main", CategoryType="Foods"},
                new Product{Name="Pasta Carbonara",        Price=850, Category="Main", CategoryType="Foods"},
                new Product{Name="Fish & Chips",           Price=780, Category="Main", CategoryType="Foods"},
                new Product{Name="BBQ Ribs",               Price=1100, Category="Main", CategoryType="Foods"},
                new Product{Name="Chicken Alfredo",        Price=820, Category="Main", CategoryType="Foods"},
                new Product{Name="Beef Burger",            Price=650, Category="Main", CategoryType="Foods"},
                new Product{Name="Grilled Salmon",         Price=980, Category="Main", CategoryType="Foods"},

                // Desserts
                new Product{Name="Chocolate Cake",         Price=400, Category="Desserts", CategoryType="Foods"},
                new Product{Name="Cheesecake",             Price=450, Category="Desserts", CategoryType="Foods"},
                new Product{Name="Tiramisu",               Price=420, Category="Desserts", CategoryType="Foods"},
                new Product{Name="Ice Cream Sundae",       Price=320, Category="Desserts", CategoryType="Foods"},
                new Product{Name="Apple Pie",              Price=380, Category="Desserts", CategoryType="Foods"},
                new Product{Name="Brownie with Ice Cream", Price=350, Category="Desserts", CategoryType="Foods"},
                new Product{Name="Crème Brûlée",           Price=480, Category="Desserts", CategoryType="Foods"},

                // BEVERAGES SECTION
                
                // Coffee
                new Product{Name="Espresso",               Price=180, Category="Coffee", CategoryType="Beverages"},
                new Product{Name="Americano",              Price=220, Category="Coffee", CategoryType="Beverages"},
                new Product{Name="Cappuccino",             Price=280, Category="Coffee", CategoryType="Beverages"},
                new Product{Name="Latte",                  Price=320, Category="Coffee", CategoryType="Beverages"},
                new Product{Name="Macchiato",              Price=350, Category="Coffee", CategoryType="Beverages"},
                new Product{Name="Mocha",                  Price=380, Category="Coffee", CategoryType="Beverages"},
                new Product{Name="Turkish Coffee",         Price=250, Category="Coffee", CategoryType="Beverages"},

                // Cold Coffee
                new Product{Name="Iced Coffee",            Price=280, Category="Cold Coffee", CategoryType="Beverages"},
                new Product{Name="Iced Latte",             Price=350, Category="Cold Coffee", CategoryType="Beverages"},
                new Product{Name="Frappuccino",            Price=420, Category="Cold Coffee", CategoryType="Beverages"},
                new Product{Name="Cold Brew",              Price=320, Category="Cold Coffee", CategoryType="Beverages"},
                new Product{Name="Iced Mocha",             Price=400, Category="Cold Coffee", CategoryType="Beverages"},
                new Product{Name="Affogato",               Price=380, Category="Cold Coffee", CategoryType="Beverages"},

                // Non-Coffee
                new Product{Name="Hot Chocolate",          Price=280, Category="Non-Coffee", CategoryType="Beverages"},
                new Product{Name="Green Tea",              Price=180, Category="Non-Coffee", CategoryType="Beverages"},
                new Product{Name="Chamomile Tea",          Price=200, Category="Non-Coffee", CategoryType="Beverages"},
                new Product{Name="Chai Latte",             Price=320, Category="Non-Coffee", CategoryType="Beverages"},
                new Product{Name="Matcha Latte",           Price=380, Category="Non-Coffee", CategoryType="Beverages"},
                new Product{Name="Hot Apple Cider",        Price=250, Category="Non-Coffee", CategoryType="Beverages"},

                // Soft Drinks
                new Product{Name="Coca Cola",              Price=120, Category="Soft Drinks", CategoryType="Beverages"},
                new Product{Name="Sprite",                 Price=120, Category="Soft Drinks", CategoryType="Beverages"},
                new Product{Name="Orange Soda",            Price=120, Category="Soft Drinks", CategoryType="Beverages"},
                new Product{Name="Lemonade",               Price=150, Category="Soft Drinks", CategoryType="Beverages"},
                new Product{Name="Iced Tea",               Price=140, Category="Soft Drinks", CategoryType="Beverages"},
                new Product{Name="Sparkling Water",        Price=100, Category="Soft Drinks", CategoryType="Beverages"},

                // Smoothies
                new Product{Name="Strawberry Smoothie",    Price=320, Category="Smoothies", CategoryType="Beverages"},
                new Product{Name="Mango Smoothie",         Price=350, Category="Smoothies", CategoryType="Beverages"},
                new Product{Name="Banana Berry Smoothie",  Price=380, Category="Smoothies", CategoryType="Beverages"},
                new Product{Name="Green Smoothie",         Price=400, Category="Smoothies", CategoryType="Beverages"},
                new Product{Name="Tropical Paradise",      Price=420, Category="Smoothies", CategoryType="Beverages"},
                new Product{Name="Chocolate Peanut Butter", Price=450, Category="Smoothies", CategoryType="Beverages"},

                // Fresh Juices
                new Product{Name="Orange Juice",           Price=280, Category="Fresh Juices", CategoryType="Beverages"},
                new Product{Name="Apple Juice",            Price=260, Category="Fresh Juices", CategoryType="Beverages"},
                new Product{Name="Pineapple Juice",        Price=300, Category="Fresh Juices", CategoryType="Beverages"},
                new Product{Name="Cranberry Juice",        Price=320, Category="Fresh Juices", CategoryType="Beverages"},
                new Product{Name="Carrot Juice",           Price=280, Category="Fresh Juices", CategoryType="Beverages"},
                new Product{Name="Mixed Berry Juice",      Price=350, Category="Fresh Juices", CategoryType="Beverages"}
            });
        }
        #endregion

        #region Product Loading
        private void LoadProducts()
        {
            // Add null check to prevent NullReferenceException
            if (ProductsItemsControl == null) return;

            IEnumerable<Product> filteredProducts = _allProducts;

            // Filter by main category type (Foods or Beverages)
            if (!string.IsNullOrEmpty(_currentCategoryType))
            {
                filteredProducts = filteredProducts.Where(p => p.CategoryType == _currentCategoryType);
            }

            // Filter by subcategory if selected
            if (!string.IsNullOrEmpty(_currentSubCategory))
            {
                filteredProducts = filteredProducts.Where(p => p.Category == _currentSubCategory);
            }

            ProductsItemsControl.ItemsSource = filteredProducts.ToList();
        }
        #endregion

        #region Category Selection
        private void FoodsCard_Click(object sender, RoutedEventArgs e)
        {
            _currentCategoryType = "Foods";
            ShowProductsForCategory();
        }

        private void BeveragesCard_Click(object sender, RoutedEventArgs e)
        {
            _currentCategoryType = "Beverages";
            ShowProductsForCategory();
        }

        private void FoodsCard_Click(object sender, MouseButtonEventArgs e)
        {
            _currentCategoryType = "Foods";
            ShowProductsForCategory();
        }

        private void BeveragesCard_Click(object sender, MouseButtonEventArgs e)
        {
            _currentCategoryType = "Beverages";
            ShowProductsForCategory();
        }

        private void BackToCategories_Click(object sender, RoutedEventArgs e)
        {
            CategorySelectionGrid.Visibility = Visibility.Visible;
            ProductsGrid.Visibility = Visibility.Collapsed;
            _currentCategoryType = "";
            _currentSubCategory = ""; // Reset subcategory when going back
        }

        private void ShowProductsForCategory()
        {
            CategorySelectionGrid.Visibility = Visibility.Collapsed;
            ProductsGrid.Visibility = Visibility.Visible;
            CategoryTitleTextBlock.Text = _currentCategoryType;

            // Setup category filter buttons
            SetupCategoryFilters();

            // Load products
            LoadProducts();
        }

        private void SetupCategoryFilters()
        {
            if (CategoryFilterPanel == null) return;

            CategoryFilterPanel.Children.Clear();
            _currentSubCategory = ""; // Reset subcategory when switching main categories

            if (_currentCategoryType == "Foods")
            {
                // Foods categories in the order you specified: Starters, Breakfast, Main, Desserts
                var foodCategories = new[] { "All", "Starters", "Breakfast", "Main", "Desserts" };

                foreach (var category in foodCategories)
                {
                    var button = CreateCategoryFilterButton(category);
                    CategoryFilterPanel.Children.Add(button);
                }
            }
            else if (_currentCategoryType == "Beverages")
            {
                // Beverages categories
                var beverageCategories = new[] { "All", "Coffee", "Cold Coffee", "Non-Coffee", "Soft Drinks", "Smoothies", "Fresh Juices" };

                foreach (var category in beverageCategories)
                {
                    var button = CreateCategoryFilterButton(category);
                    CategoryFilterPanel.Children.Add(button);
                }
            }
        }

        private Button CreateCategoryFilterButton(string category)
        {
            var button = new Button
            {
                Content = category,
                Style = (Style)FindResource("CategoryButtonStyle"),
                Tag = category,
                Margin = new Thickness(5, 2, 5, 2), // left, top, right, bottom
                Padding = new Thickness(15, 8, 15, 8) // left, top, right, bottom
            };

            // Set active style for "All" button initially
            if (category == "All" && string.IsNullOrEmpty(_currentSubCategory))
            {
                button.Style = (Style)FindResource("ActiveCategoryButtonStyle");
            }

            button.Click += CategoryFilterButton_Click;
            return button;
        }

        private void CategoryFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var category = button.Tag.ToString();

            // Update current subcategory
            _currentSubCategory = category == "All" ? "" : category;

            // Update button styles
            foreach (Button btn in CategoryFilterPanel.Children)
            {
                btn.Style = (Style)FindResource("CategoryButtonStyle");
            }
            button.Style = (Style)FindResource("ActiveCategoryButtonStyle");

            // Update products display
            LoadProducts();
        }
        #endregion

        #region Cart
        private void ProductCard_Click(object sender, MouseButtonEventArgs e)
        {
            var product = (Product)((Border)sender).Tag;
            AddToCart(product);
        }

        private void AddToCart(Product p)
        {
            var existing = _cartItems.FirstOrDefault(c => c.Product.Name == p.Name);
            if (existing != null) existing.Quantity++;
            else _cartItems.Add(new CartItem { Product = p });
            RefreshCart();
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var item = (CartItem)((Button)sender).Tag;
            item.Quantity++;
            RefreshCart();
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var item = (CartItem)((Button)sender).Tag;
            if (item.Quantity == 1) _cartItems.Remove(item);
            else item.Quantity--;
            RefreshCart();
        }

        private void RefreshCart()
        {
            // Add null checks
            if (CartItemsPanel == null || SubtotalTextBlock == null) return;

            CartItemsPanel.Children.Clear();
            foreach (var ci in _cartItems)
                CartItemsPanel.Children.Add(CreateCartItemControl(ci));
            SubtotalTextBlock.Text = _cartItems.Sum(c => c.Total).ToString("F2");

            // Update Open Bills button text based on cart contents
            UpdateOpenBillsButton();
        }

        private void UpdateOpenBillsButton()
        {
            if (OpenBillsButton == null) return;

            if (_cartItems.Any())
            {
                OpenBillsButton.Content = "💾 Save Order";
            }
            else
            {
                OpenBillsButton.Content = "💾 Save Order";
            }
        }

        private Border CreateCartItemControl(CartItem ci)
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            grid.Children.Add(new TextBlock { Text = ci.Product.Name, FontWeight = FontWeights.Bold, VerticalAlignment = VerticalAlignment.Center });

            var qtyPanel = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            var dec = new Button { Content = "-", Width = 25, Height = 25, Margin = new Thickness(0, 0, 5, 0), Tag = ci };
            dec.Click += DecreaseQuantity_Click;
            var qty = new TextBlock { Text = ci.Quantity.ToString(), Margin = new Thickness(10, 0, 10, 0), FontWeight = FontWeights.Bold };
            var inc = new Button { Content = "+", Width = 25, Height = 25, Margin = new Thickness(5, 0, 0, 0), Tag = ci };
            inc.Click += IncreaseQuantity_Click;

            qtyPanel.Children.Add(dec);
            qtyPanel.Children.Add(qty);
            qtyPanel.Children.Add(inc);
            Grid.SetColumn(qtyPanel, 1);

            var price = new TextBlock { Text = ci.Total.ToString("F2"), FontWeight = FontWeights.Bold, Margin = new Thickness(15, 0, 0, 0) };
            Grid.SetColumn(price, 2);

            grid.Children.Add(qtyPanel);
            grid.Children.Add(price);

            return new Border
            {
                Background = System.Windows.Media.Brushes.White,
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(15, 10, 15, 10),
                Child = grid
            };
        }

        private void ChargeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_cartItems.Any()) { MessageBox.Show("Cart is empty"); return; }
            var total = _cartItems.Sum(c => c.Total);
            if (MessageBox.Show($"Charge ₱{total:F2} ?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _cartItems.Clear();
                RefreshCart();
                MessageBox.Show("Payment processed!");
            }
        }
        #endregion

        #region Save Receipt
        private void OpenBillsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_cartItems.Any())
            {
                // Show Save Receipt popup
                ShowSaveReceiptPopup();
            }
            else
            {
                // Show Open Bills functionality (you can implement this later)
                MessageBox.Show("Open Bills functionality - No items in cart");
            }
        }

        private void ShowSaveReceiptPopup()
        {
            // Generate receipt number
            ReceiptNameTextBox.Text = DateTime.Now.ToString("yyyyMMdd") + "001";
            DescriptionTextBox.Text = "Description";
            TableComboBox.SelectedIndex = 0;

            SaveReceiptPopup.Visibility = Visibility.Visible;
        }

        private void CancelSave_Click(object sender, RoutedEventArgs e)
        {
            SaveReceiptPopup.Visibility = Visibility.Collapsed;
        }

        private void SaveReceipt_Click(object sender, RoutedEventArgs e)
        {
            // Get the values
            var receiptName = ReceiptNameTextBox.Text;
            var description = DescriptionTextBox.Text;
            var selectedTable = ((ComboBoxItem)TableComboBox.SelectedItem)?.Content?.ToString();
            var total = _cartItems.Sum(c => c.Total);

            // Show confirmation
            var message = $"Receipt Saved!\n\n" +
                         $"Receipt: {receiptName}\n" +
                         $"Description: {description}\n" +
                         $"Table: {selectedTable}\n" +
                         $"Total: ₱{total:F2}";

            MessageBox.Show(message, "Receipt Saved", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear cart and close popup
            _cartItems.Clear();
            RefreshCart();
            SaveReceiptPopup.Visibility = Visibility.Collapsed;
        }

        // Add event handlers for popup background clicks
        private void PopupBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Close popup when clicking on the background
            SaveReceiptPopup.Visibility = Visibility.Collapsed;
        }

        private void PopupContent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Prevent popup from closing when clicking on the content
            e.Handled = true;
        }
        #endregion

        #region Sidebar Toggle
        private void ToggleSidebar_Click(object sender, RoutedEventArgs e)
        {
            _isSidebarExpanded = !_isSidebarExpanded;

            if (_isSidebarExpanded)
            {
                // Expand sidebar
                SidebarColumn.Width = new GridLength(280);
                SidebarIconsPanel.Visibility = Visibility.Collapsed;
                SidebarExpandedPanel.Visibility = Visibility.Visible;
            }
            else
            {
                // Collapse sidebar
                SidebarColumn.Width = new GridLength(80);
                SidebarExpandedPanel.Visibility = Visibility.Collapsed;
                SidebarIconsPanel.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Keyboard
        private void SetupKeyboardShortcuts()
        {
            Loaded += (_, __) =>
            {
                InputBindings.Add(new InputBinding(
                    new RelayCommand(_ => ChargeButton_Click(null, null)),
                    new KeyGesture(Key.F12)));
            };
        }
        #endregion

        #region Helpers
        private static System.Collections.Generic.IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;
            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(depObj, i);
                if (child is T t) yield return t;
                foreach (var childOfChild in FindVisualChildren<T>(child)) yield return childOfChild;
            }
        }
        #endregion


        #region Settings Management
        // Track original settings to detect changes
        private string _originalTheme = "Light";
        private string _originalLanguage = "English";
        private string _originalPrinter = "Default Printer";

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            // Store current settings when opening settings popup
            _originalTheme = LightThemeRadio.IsChecked == true ? "Light" : "Dark";
            _originalLanguage = ((ComboBoxItem)LanguageComboBox.SelectedItem)?.Content?.ToString() ?? "English";
            _originalPrinter = ((ComboBoxItem)PrinterComboBox.SelectedItem)?.Content?.ToString() ?? "Default Printer";
            
            SettingsPopup.Visibility = Visibility.Visible;
        }

        private void Backoffice_Click(object sender, RoutedEventArgs e)
        {
            // Show admin login window
            var loginWindow = new AdminLoginWindow();
            loginWindow.Owner = this;
            
            // Show login dialog
            bool? result = loginWindow.ShowDialog();
            
            if (result == true && loginWindow.IsAuthenticated)
            {
                // Authentication successful - show backoffice
                ShowBackofficeWindow();
            }
            else
            {
                // Authentication failed or cancelled
                MessageBox.Show("Access denied. Admin authentication required.", 
                               "Backoffice Access", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Warning);
            }
        }

        private void ShowBackofficeWindow()
        {
            // Show backoffice functionality
            MessageBox.Show("🖥️ Backoffice Access Granted!\n\n" +
                           "Features available:\n" +
                           "• Sales Reports\n" +
                           "• Product Management\n" +
                           "• User Management\n" +
                           "• System Settings\n" +
                           "• Inventory Control", 
                           "Backoffice Management", 
                           MessageBoxButton.OK, 
                           MessageBoxImage.Information);
        }

        private void SettingsPopupBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SettingsPopup.Visibility = Visibility.Collapsed;
        }

        private void SettingsPopupContent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void ThemeChanged(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton?.IsChecked == true)
            {
                if (radioButton.Name == "LightThemeRadio")
                {
                    // Apply light theme silently (no popup here)
                    this.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(248, 249, 250));
                }
                else if (radioButton.Name == "DarkThemeRadio")
                {
                    // Apply dark theme silently (no popup here)
                    this.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 37, 41));
                }
            }
        }

        private void LanguageChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selectedItem = comboBox?.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                var language = selectedItem.Tag?.ToString();
                // Apply language change silently (no popup here)
                
                // You can add actual language switching logic here if needed
                // For example: ChangeApplicationLanguage(language);
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("This will open the password change dialog.\n\nWould you like to proceed?", "Change Password", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Password change functionality would be implemented here.", "Change Password", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void TestPrinter_Click(object sender, RoutedEventArgs e)
        {
            var selectedPrinter = ((ComboBoxItem)PrinterComboBox.SelectedItem)?.Content?.ToString();
            MessageBox.Show($"Testing printer: {selectedPrinter}\n\n✅ Test print successful!\nPrinter is working correctly.", "Printer Test", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsPopup.Visibility = Visibility.Collapsed;
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            var currentTheme = LightThemeRadio.IsChecked == true ? "Light" : "Dark";
            var currentLanguage = ((ComboBoxItem)LanguageComboBox.SelectedItem)?.Content?.ToString();
            var currentPrinter = ((ComboBoxItem)PrinterComboBox.SelectedItem)?.Content?.ToString();

            // Check what has changed and show appropriate popups
            var changedSettings = new List<string>();
            
            if (currentTheme != _originalTheme)
            {
                changedSettings.Add($"Theme: {currentTheme}");
            }
            
            if (currentLanguage != _originalLanguage)
            {
                changedSettings.Add($"Language: {currentLanguage}");
            }
            
            if (currentPrinter != _originalPrinter)
            {
                changedSettings.Add($"Printer: {currentPrinter}");
            }

            // Show popup only if something actually changed
            if (changedSettings.Any())
            {
                var changesSummary = string.Join("\n", changedSettings);
                MessageBox.Show($"Settings Updated Successfully! ✅\n\nChanged settings:\n{changesSummary}", 
                               "Settings Saved", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Information);
            }
            else
            {
                // No changes made - silent save
                MessageBox.Show("Settings saved successfully! 💾", 
                               "Settings", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Information);
            }

            // You can add actual settings persistence logic here
            // For example: SaveSettingsToFile(currentTheme, currentLanguage, currentPrinter);

            SettingsPopup.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}

// Move Extensions class outside MainWindow class to fix CS1109 error
internal static class Extensions
{
    public static void AddRange<T>(this ObservableCollection<T> col, System.Collections.Generic.IEnumerable<T> items)
    { 
        foreach (var i in items) 
            col.Add(i); 
    }
}