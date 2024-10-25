using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace project
{

    public partial class Login : Window
    {
        public double Screen_Width { get; set; }
        public double Screen_Height { get; set; }
        public Login()
        {
            InitializeComponent();
            Screen_Width = SystemParameters.PrimaryScreenWidth;
            Screen_Height = SystemParameters.PrimaryScreenHeight;
            this.Width = Screen_Width;
            this.Height = Screen_Height;
            this.DataContext = this;
        }
        private void TogglePasswordVisibility3(object sender, RoutedEventArgs e)
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

    }
}
