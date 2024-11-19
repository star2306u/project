using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
   
    public partial class Window1 : Window
    {
        
        public double Screen_Width { get; set; }
        public double Screen_Height { get; set; }
       
        public Window1()
        {
            InitializeComponent();
            Screen_Width = SystemParameters.PrimaryScreenWidth;
            Screen_Height = SystemParameters.PrimaryScreenHeight;
            this.Width = Screen_Width;
            this.Height = Screen_Height;
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Login secondwindow = new Login();
            //this.Visibility =Visibility.Hidden;
            this.Close();
            secondwindow.Show();
        }
    }
    }


