using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace NURTEX_POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (AppOperations.AppOperations.Login(TextBoxUsername.Text, TextBoxPassword.Password))
            {
                ErrorLabel.Content = "";
                new Views.MainPanel().Show();
                this.Close();
            }
            else
            {
                ErrorLabel.Content = "Invalid Login. Try again.";
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!AppOperations.AppOperations.CheckConnection())
            {
                ConnectionStatusEllipse.Fill = new SolidColorBrush(Colors.Red);
                ConnectionStatusLabel.Foreground=new SolidColorBrush(Colors.Red);
                ConnectionStatusLabel.Content = "Disconnected";
            }
            else
            {
                ConnectionStatusEllipse.Fill = new SolidColorBrush(Colors.LimeGreen);
                ConnectionStatusLabel.Foreground = new SolidColorBrush(Colors.LimeGreen);
                ConnectionStatusLabel.Content = "Connected";
            }
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter) 
                ButtonLogin_Click(new object(), new RoutedEventArgs());
        }
    }
}
