using System.Windows;
namespace NURTEX_POS.Views
{
    /// <summary>
    /// Interaction logic for MainPanel.xaml
    /// </summary>
    public partial class MainPanel : Window
    {
        public MainPanel()
        {
            InitializeComponent();

            if (AppOperations.AppOperations.GetCurrentUser() == null ||
                AppOperations.AppOperations.GetCurrentUser().Role != "Admin")
                ManageStaffsTabItem.IsEnabled = false;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            AppOperations.AppOperations.Logout();
            new MainWindow().Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //MessageBox.Show("Cannot be closed without Logging out.");
        }
    }
}
