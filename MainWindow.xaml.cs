using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace project
{
    public partial class MainWindow : Window
    {
        public double Screen_Width { get; set; }
        public double Screen_Height { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Screen_Width = SystemParameters.PrimaryScreenWidth;
            Screen_Height = SystemParameters.PrimaryScreenHeight;
            this.Width = Screen_Width;
            this.Height = Screen_Height;
            this.DataContext = this;

            Years();
            Month();
            UpdateDays(DateTime.Now.Year, DateTime.Now.Month);
        }

        private void Years()
        {
            int currentYear = DateTime.Now.Year;
            YearComboBox.Items.Clear();
            YearComboBox.Items.Add(new ComboBoxItem { Content = "YEAR", IsSelected = true });
            for (int year = 1980; year <= currentYear; year++)
            {
                YearComboBox.Items.Add(new ComboBoxItem { Content = year.ToString() });
            }
            YearComboBox.SelectedIndex = 0;
        }

        private void Month()
        {
            MonthComboBox.Items.Clear();
            MonthComboBox.Items.Add(new ComboBoxItem { Content = "MONTHS", IsSelected = true });

            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            foreach (var month in months)
            {
                MonthComboBox.Items.Add(new ComboBoxItem { Content = month });
            }
            MonthComboBox.SelectedIndex = 0;
        }

  
        private void UpdateDays(int year, int month)
        {
            DayComboBox.Items.Clear();
            DayComboBox.Items.Add(new ComboBoxItem { Content = "DAY", IsSelected = true });

            int daysInMonth = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                DayComboBox.Items.Add(new ComboBoxItem { Content = day.ToString() });
            }

            DayComboBox.SelectedIndex = 0;
        }

    
        private void UpdateDaysIfValid()
        {
            if (YearComboBox.SelectedItem != null && MonthComboBox.SelectedItem != null)
            {
                ComboBoxItem selectedYearItem = YearComboBox.SelectedItem as ComboBoxItem;
                ComboBoxItem selectedMonthItem = MonthComboBox.SelectedItem as ComboBoxItem;

                if (selectedYearItem != null && selectedMonthItem != null)
                {
                    
                    ApplySelectionColor(selectedYearItem);
                    ApplySelectionColor(selectedMonthItem);

                    if (selectedMonthItem.Content != null && selectedYearItem.Content != null)
                    {
                        if (selectedMonthItem.Content.ToString() != "MONTHS" && selectedYearItem.Content.ToString() != "YEAR")
                        {
                            int year = int.Parse(selectedYearItem.Content.ToString());
                            int month = DateTime.ParseExact(selectedMonthItem.Content.ToString(), "MMMM", CultureInfo.InvariantCulture).Month;
                            UpdateDays(year, month);
                        }
                    }
                }
            }
        }

        
        private void ApplySelectionColor(ComboBoxItem selectedItem)
        {
            selectedItem.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 179));
            selectedItem.Foreground = new SolidColorBrush(Colors.White);
            selectedItem.BorderBrush = new SolidColorBrush(Colors.DarkBlue);
        }

        
        private void MonthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDaysIfValid();
        }

        
        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDaysIfValid();
        }

        
        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            if (Password.Visibility == Visibility.Visible)
            {
                Passwordtext.Text = Password.Password;
                Password.Visibility = Visibility.Collapsed;
                Passwordtext.Visibility = Visibility.Visible;
            }
            else
            {
                Password.Password = Passwordtext.Text;
                Password.Visibility = Visibility.Visible;
                Passwordtext.Visibility = Visibility.Collapsed;
            }
        }

        
        private void TogglePasswordVisibility1(object sender, RoutedEventArgs e)
        {
            if (rePassword.Visibility == Visibility.Visible)
            {
                rePasswordtext.Text = rePassword.Password;
                rePassword.Visibility = Visibility.Collapsed;
                rePasswordtext.Visibility = Visibility.Visible;
            }
            else
            {
                rePassword.Password = rePasswordtext.Text;
                rePassword.Visibility = Visibility.Visible;
                rePasswordtext.Visibility = Visibility.Collapsed;
            }
        }

        
        private void Signup(object sender, RoutedEventArgs e)
        {
            bool isValid = true;

            // Reset validation messages
            Email_verify.Content = "";
            Password_verify.Content = "";
            Repassword_verify.Content = "";
            Day_verify.Content = "";
            Month_verify.Content = "";
            Year_verify.Content = "";
            User_name.Content = "";
            Name_verify.Content = "";

            // Email validation
            string email = Useremail.Text;
            if (!IsValidEmail(email))
            {
                Email_verify.Content = "*Invalid email format!";
                isValid = false;
            }

            // Name validation
            string name = Name.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Name_verify.Content = "*Name cannot be empty!";
                isValid = false;
            }

            // Username validation
            string userName = Username.Text.Trim();
            if (string.IsNullOrWhiteSpace(userName))
            {
                User_name.Content = "*User Name cannot be empty!";
                isValid = false;
            }

            // Password validation
            string password = Password.Password;
            string repassword = rePassword.Password;
            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                Password_verify.Content = "*Password must be at least 6 characters!";
                isValid = false;
            }

            if (password != repassword)
            {
                Repassword_verify.Content = "*Passwords do not match!";
                isValid = false;
            }

            

          
            if (DayComboBox.SelectedItem is ComboBoxItem selectedDay && selectedDay.Content.ToString() == "DAY")
            {
                Day_verify.Content = "*Please select a day!";
                isValid = false;
            }

            
            if (MonthComboBox.SelectedItem is ComboBoxItem selectedMonth && selectedMonth.Content.ToString() == "MONTHS")
            {
                Month_verify.Content = "*Please select a month!";
                isValid = false;
            }

            
            if (YearComboBox.SelectedItem is ComboBoxItem selectedYear && selectedYear.Content.ToString() == "YEAR")
            {
                Year_verify.Content = "*Please select a year!";
                isValid = false;
            }

            
            if (isValid)
            {
                Window1 secondwindow = new Window1();
                //this.Visibility =Visibility.Hidden;
                CloseWindowWithAnimation();
                this.Close();
                secondwindow.Show();
            }

            
        }

       
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                return false;
            }

            string[] parts = email.Split('@');
            if (parts.Length != 2)
            {
                return false;
            }

            string localPart = parts[0];
            string domainPart = parts[1];
            if (localPart.Length > 64 || domainPart.Length > 253)
            {
                return false;
            }
            if (localPart.Contains("..") || domainPart.Contains(".."))
            {
                return false;
            }
            if (localPart.StartsWith(".") || localPart.EndsWith(".") ||
                domainPart.StartsWith(".") || domainPart.EndsWith("."))
            {
                return false;
            }

            return true;
        }
        private async Task CloseWindowWithAnimation()
        {
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(1))  
            };
            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);
            await Task.Delay(1000);  
            this.Close();
        }
        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
            await Task.Delay(1000); 
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login secondwindow = new Login();
            //this.Visibility =Visibility.Hidden;
            CloseWindowWithAnimation();
            this.Close();
            secondwindow.Show();
        }
    }
}
