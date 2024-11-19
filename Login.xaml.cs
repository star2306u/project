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
using System.Text.Json; // or Newtonsoft.Json if preferred
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using Newtonsoft.Json;
using static project.Login;

namespace project
{

    public partial class Login : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public double Screen_Width { get; set; }
        public double Screen_Height { get; set; }
        public class MyDataModel
        {
            public string user { get; set; }
            public string password { get; set; }

            // Add more properties as needed
        }
        public class User
        {
            public string user_id { get; set; }
            public string user_name { get; set; }
            public string email { get; set; }
            public string avatar { get; set; }
            public string name { get; set; }
            public string password { get; set; }
            public string status { get; set; }

        }

        public class ApiResponse
        {
            public User data { get; set; }
            public int foundStatus { get; set; }
        }
        public Login()
        {
            InitializeComponent();
            Screen_Width = SystemParameters.PrimaryScreenWidth;
            Screen_Height = SystemParameters.PrimaryScreenHeight;
            this.Width = Screen_Width;
            this.Height = Screen_Height;
            this.DataContext = this;
        }
        private async Task<ApiResponse> SendAndReceiveDataAsync(string url, MyDataModel userRequest)
            {
                try
                {
                    // Serialize the user request object to JSON
                    string jsonRequest = JsonConvert.SerializeObject(userRequest);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    // Send POST request
                    HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();

                    // Read JSON response as a string
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON to ApiResponse object
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                    return apiResponse;
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Request error: {e.Message}");
                    return null;
                }
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // URL of the API endpoint
            string url = "http://localhost:3000/api/login";


            // Prepare data to send
            var data = new MyDataModel
            {
               
                user = email.Text,
                password = Password.Password,

            };

            // Send data and get the response
            ApiResponse result = await SendAndReceiveDataAsync(url, data);
            if (result != null && result.foundStatus == 1)
            {
                User user = result.data;
                // MessageBox.Show(user.name);
                 string id = user.user_id.ToString(); //for accesing my id 
                Application.Current.Properties["myuserid"] = id;
                string username = user.user_name;
                Application.Current.Properties["myusername"] = username;
                //var data = Application.Current.Properties["SharedData"] as string;
                this.Hide();
                MessageBox.Show(username);
                Chatui secondwindow = new Chatui();
                secondwindow.Show();


            }
            else
            {
                email_verify.Content = "Please check your email or password";
                login_password_verify.Content = "Please check your email or password";
            }

        }
    }
}
