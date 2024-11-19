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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SocketIOClient;
using Newtonsoft.Json.Linq;
namespace project
{
    public partial class Page1 : Page
    {
        public string friendid = Application.Current.Properties["SharedData"] as string; //friend id
        public string friendname = Application.Current.Properties["other_user_id"] as string; // freiendname
        public string myid = Application.Current.Properties["myuserid"] as string; //my id
        private SocketIOClient.SocketIO socket;
        private bool isConnected = false; // Track connection state

        public Page1()
        {
            InitializeComponent();
            friendid = Application.Current.Properties["SharedData"] as string;
            friendname = Application.Current.Properties["other_user_id"] as string;

            // Add a test message for the other user
            AddMessageOtheruser(friendname, "hlo", "999");
        }


        private async void InitializeSocket()
        {
            if (string.IsNullOrEmpty(friendid))
            {
                MessageBox.Show("Friend ID is not set. Please select a friend to chat.");
                return;
            }

            socket = new SocketIOClient.SocketIO("http://localhost:3000"); // Replace with your server's address

            try
            {
                // Connect to the server
                await socket.ConnectAsync();
                isConnected = true; // Update connection state
                await socket.EmitAsync("setName", myid);
                await socket.EmitAsync("joinRoom",friendid);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to socket: {ex.Message}");
            }
        }

        private void RegisterSocketEvents()
        {
            socket.On("message", response =>
            {
                // Parse the response as JSON
                var data = JObject.Parse(response.GetValue<object>().ToString());

                // Extract 'from' and 'text' fields
                string sender = data["from"].ToString();
                string text = data["text"].ToString();

                // Display the message
                Dispatcher.Invoke(() => AddMessageOtheruser(sender, text, DateTime.Now.ToString("hh:mm tt")));
            });
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            // Initialize the socket only if it's not already connected
            if (!isConnected)
            {
                InitializeSocket();
                RegisterSocketEvents();
            }

            string message = MessageTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(message) && socket != null && isConnected)
            {
                AddMessage("You", message);
                socket.EmitAsync("sendMessage", message); // Send message to the server
                MessageTextBox.Clear();
            }
            else if (socket == null || !isConnected)
            {
                MessageBox.Show("Socket is not connected. Please try again later.");
            }
        }

        private void AddMessage(string user, string message)
        {
            // Get the current timestamp
            string timestamp = DateTime.Now.ToString("hh:mm tt");

            Border messageBorder = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(46, 54, 57)),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(5, 5, 5, 10),
                Padding = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                MinHeight = 40,
                MinWidth = 50,
                MaxWidth = 600
            };

            StackPanel messageContainer = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            TextBlock userTextBlock = new TextBlock
            {
                Text = user,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.LightGray),
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
            };

            TextBlock messageTextBlock = new TextBlock
            {
                Text = message,
                FontSize = 16,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 5)
            };

            TextBlock timestampTextBlock = new TextBlock
            {
                Text = timestamp,
                FontSize = 10,
                Foreground = new SolidColorBrush(Color.FromRgb(170, 170, 170)),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            messageContainer.Children.Add(userTextBlock);
            messageContainer.Children.Add(messageTextBlock);
            messageContainer.Children.Add(timestampTextBlock);

            messageBorder.Child = messageContainer;

            MessageStackPanel.Children.Add(messageBorder);

            ScrollToBottom();
        }

        private void AddMessageOtheruser(string user, string message, string time)
        {
            string timestamp = DateTime.Now.ToString("hh:mm tt");

            Border messageBorder = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(46, 54, 57)),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(5, 5, 5, 10),
                Padding = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                MinHeight = 40,
                MinWidth = 50,
                MaxWidth = 600
            };

            StackPanel messageContainer = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            TextBlock userTextBlock = new TextBlock
            {
                Text = user,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.LightGray),
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
            };

            TextBlock messageTextBlock = new TextBlock
            {
                Text = message,
                FontSize = 16,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 5)
            };

            TextBlock timestampTextBlock = new TextBlock
            {
                Text = time,
                FontSize = 10,
                Foreground = new SolidColorBrush(Color.FromRgb(170, 170, 170)),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            messageContainer.Children.Add(userTextBlock);
            messageContainer.Children.Add(messageTextBlock);
            messageContainer.Children.Add(timestampTextBlock);

            messageBorder.Child = messageContainer;

            MessageStackPanel.Children.Add(messageBorder);

            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            MessageScrollViewer.ScrollToEnd();
        }

        private void MessageTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MessageTextBox.Text == "Type your message here...")
            {
                MessageTextBox.Text = string.Empty;
                MessageTextBox.Foreground = Brushes.White;
            }
        }

        private void MessageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MessageTextBox.Text))
            {
                MessageTextBox.Text = "Type your message here...";
                MessageTextBox.Foreground = Brushes.Gray;
            }
        }

        private void MessageTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SendButton_Click(sender, e); // Trigger the Send button click
            }
        }
    }
}