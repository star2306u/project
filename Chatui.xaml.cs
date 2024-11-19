using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace project
{

    public partial class Chatui : Window
    {
        public double Screen_Width { get; set; }
        public double Screen_Height { get; set; }
        public ObservableCollection<Unread> Images { get; set; }
        public ObservableCollection<Grid1Item> Grid1Images { get; set; }
        private bool isSidebarExpanded = true;

        private static readonly HttpClient client = new HttpClient();

        public Chatui()
        {

            InitializeComponent();
            LoadFriendData("83d930a7-490c-4a12-b083-94e0eb5639f8"); //83d930a7-490c-4a12-b083-94e0eb5639f8
            Screen_Width = SystemParameters.PrimaryScreenWidth;
            Screen_Height = SystemParameters.PrimaryScreenHeight;
            this.Width = Screen_Width;
            this.Height = Screen_Height;
            this.DataContext = this;
            MainFrame.Navigate(new Page1());

            Images = new ObservableCollection<Unread>();
            Grid1Images = new ObservableCollection<Grid1Item>();

            /*for (int i = 1; i <= 5; i++)
            {
                AddNewImage("C:\\Users\\SHASHANK\\Desktop\\random.jpg", i, "name");
            }
            for (int i = 1; i <= 7; i++)
            {
                AddGrid1Image("C:\\Users\\SHASHANK\\Desktop\\random.jpg", i * 1000, "name");
            }
            AddGrid1Image("C:\\Users\\SHASHANK\\Desktop\\random.jpg", 9 * 1000, "sahil");
            AddNewImage("C:\\Users\\SHASHANK\\Desktop\\random.jpg", 9000, "sahil");
            */
        }


        private async void LoadFriendData(string userId)
        {
            try
            {
                List<Friend> friends = await GetFriendDataAsync(userId);
                if (friends != null && friends.Count > 0)
                {
                    Dispatcher.Invoke(() =>
                    {
                        foreach (var friend in friends)
                        {
                            AddGrid1Image("C:\\Users\\SHASHANK\\Desktop\\random.jpg", friend.FriendId, friend.UserName);
                        }
                    });
                }

                else
                {
                    MessageBox.Show("No friends found or an error occurred.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading friend data: {ex.Message}");
            }
        }

        private async Task<List<Friend>> GetFriendDataAsync(string userId)
        {
            try
            {
                string url = "http://localhost:3000/api/friends/getall";
                var requestData = new { userid = userId };
                string json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<FriendResponse>(responseBody);

                    return responseData.Data;  // This is now a List<Friend>
                }
                else
                {
                    MessageBox.Show($"Error: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}");
                return null;
            }
        }
        public class FriendResponse
        {
            [JsonProperty("data")]
            public List<Friend> Data { get; set; } // Now expecting a list of friends
        }
        public class Friend
        {
            [JsonProperty("friendid")]
            public string FriendId { get; set; }

            [JsonProperty("user_name")]
            public string UserName { get; set; }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "Search a convo")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.White;
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Search a convo";
                textBox.Foreground = Brushes.Gray;
            }
        }

        public class Unread
        {
            public string ImagePath { get; set; }
            public string Unreadid { get; set; } // Changed from long to string
            public string name { get; set; }
        }

        public class Grid1Item
        {
            public string ImagePath { get; set; }
            public string MessageUID { get; set; } // Changed from long to string
            public string UserName { get; set; }
        }

        public void AddNewImage(string path, string id, string currentname) // Change id to string
        {
            Images.Add(new Unread { ImagePath = path, Unreadid = id, name = currentname });
        }

        public void AddGrid1Image(string path, string messageUID, string userNAme) // Change messageUID to string
        {
            Grid1Images.Add(new Grid1Item { ImagePath = path, MessageUID = messageUID, UserName = userNAme });
        }

        private void SidebarImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string unreadId) // Change long to string
            {
                string hlo = button.Tag.ToString();
                // Handle the button click event using the string unreadId
                MessageBox.Show($"Button clicked with Unread ID: {hlo}");
            }
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string messageUID) // Change long to string
            {
                string hlo = button.Tag.ToString();
                Application.Current.Properties["SharedData"] = hlo;
                var gridItem = Grid1Images.FirstOrDefault(img => img.MessageUID == messageUID);
                if (gridItem != null)
                {
                    dockname.Content = gridItem.UserName; // Set dockname.Content based on the button clicked
                    string otherid = gridItem.UserName;
                    Application.Current.Properties["other_user_id"] = otherid;
                    dockimage.Source = new BitmapImage(new Uri(gridItem.ImagePath, UriKind.Absolute));
                }
                MainFrame.Navigate(new Page1());
                dockimage.Visibility = Visibility.Visible;
                dockname.Visibility = Visibility.Visible;
                PageContainer.Visibility = Visibility.Visible;  // Show Grid 3
                topbutton1.Tag = messageUID;
                topbutton2.Tag = messageUID;
                topbutton3.Tag = messageUID;

                dockimage.Visibility = Visibility.Visible;
                dockname.Visibility = Visibility.Visible;
                //topbutton1.Visibility = Visibility.Visible;
                //topbutton2.Visibility = Visibility.Visible;
                //topbutton3.Visibility = Visibility.Visible;
                MainFrame.Navigate(new Page1());
                RemoveItemByTag(messageUID);
            }
        }

        private void RemoveItemByTag(string tag) // Change long to string
        {
            var unreadItem = Images.FirstOrDefault(img => img.Unreadid == tag);
            if (unreadItem != null)
            {
                Images.Remove(unreadItem);
            }
        }

        private void Grid1Image_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Content is Image image)
            {
                PopupImage.Source = image.Source;
                ImagePopup.IsOpen = true;
            }
        }

        private void ToggleSidebar_Click(object sender, RoutedEventArgs e)
        {
            if (isSidebarExpanded)
            {
                Storyboard collapse = (Storyboard)FindResource("CollapseSidebar");
                collapse.Begin();
            }
            else
            {
                Storyboard expand = (Storyboard)FindResource("ExpandSidebar");
                expand.Begin();
            }
            isSidebarExpanded = !isSidebarExpanded;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
            {
                ImagePopup.IsOpen = false;
            }
        }

        private void OpenSearchPopup(object sender, RoutedEventArgs e)
        {
            SearchPopup.IsOpen = true;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ResultsPanel.Children.Clear();

            if (FriendTextBox.Text == "sahil")
            {
                for (int i = 0; i < 5; i++)
                {
                    StackPanel friendPanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Margin = new Thickness(0, 5, 0, 5)
                    };

                    Ellipse imageCircle = new Ellipse
                    {
                        Width = 40,
                        Height = 40,
                        Fill = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri(@"C:\Users\SHASHANK\Desktop\random.jpg")),
                            Stretch = Stretch.UniformToFill
                        },
                        Margin = new Thickness(0, 0, 10, 0)
                    };

                    Label nameLabel = new Label
                    {
                        Content = "Friend " + (i + 1),
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 110,
                    };

                    Button addButton = new Button
                    {
                        Content = "Add",
                        Width = 50,
                        Margin = new Thickness(10, 0, 0, 0),
                        Tag = "friendID123"
                    };

                    addButton.Click += AddButton_Click; // event for click for this button  
                    friendPanel.Children.Add(imageCircle);
                    friendPanel.Children.Add(nameLabel);
                    friendPanel.Children.Add(addButton);

                    ResultsPanel.Children.Add(friendPanel);
                }
            }
            else
            {
                StackPanel friendPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 5, 0, 5)
                };
                Label error = new Label
                {
                    Content = "User  not found ! ",
                    Width = 200,
                    Foreground = Brushes.Red,
                    FontSize = 20
                };
                friendPanel.Children.Add(error);
                ResultsPanel.Children.Add(friendPanel);
            }

        }

        private void FriendPopup_Closed(object sender, EventArgs e)
        {
            ResultsPanel.Children.Clear();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string uniqueId = clickedButton.Tag.ToString();
                MessageBox.Show($"Button clicked for friend with ID: {uniqueId}");
            }
        }

        private void ConvoButton_Click(object sender, RoutedEventArgs e)
        {
            var searchText = SearchTextBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText) || searchText == "search a convo")
            {
                MessageBox.Show("Please enter a valid name to search.");
                return;
            }

            var matchingItems = Grid1Images.Where(item => item.UserName.ToLower().Contains(searchText)).ToList();

            if (matchingItems.Count > 0)
            {
                foreach (var match in matchingItems)
                {
                    Grid1Images.Remove(match);
                    Grid1Images.Insert(0, match);
                }
            }
            else
            {
                MessageBox.Show("No match found for the entered name.");
            }
        }

        private void convo_button_event(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ConvoButton_Click(sender, e); // Trigger the Send button click
            }
        }

        private void FriendTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SearchButton_Click(sender, e); // Trigger the Send button click
            }
        }

        private void topbutton1_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string unreadId) // Change long to string
            {
                string hlo = button.Tag.ToString();
                MessageBox.Show(hlo);
            }
        }

        private void topbutton2_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string unreadId) // Change long to string
            {
                string hlo = button.Tag.ToString();
                MessageBox.Show(hlo);
            }
        }

        private void topbutton3_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string unreadId) // Change long to string
            {
                string hlo = button.Tag.ToString();
                MessageBox.Show(hlo);
            }
        }
    }
}