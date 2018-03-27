using System.Windows;
using System.Windows.Controls;
using POS.BusinessLogics;
using POS.Models;

namespace NURTEX_POS.Views
{
    /// <summary>
    /// Interaction logic for ManageStaffControl.xaml
    /// </summary>
    public partial class ManageStaffControl : UserControl
    {
        private UserLogics _userLogics=new UserLogics();

        public ManageStaffControl()
        {
            InitializeComponent();
        }

        private void Refresh()
        {
            UsernameTextBox.Text = "";
            PasswordTextBox.Text = "";
            NameTextBox.Text = "";
            ContactNoBox.Text = "";
            AddressNoBox.Text = "";
            LegalInfoTextBox.Text = "";

            UsernameTextBox.IsEnabled = true;

            StaffsDataGrid.ItemsSource = null;
            StaffsDataGrid.ItemsSource = _userLogics.GetAllUsers();
        }

        private void ManageStaffControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void AddStaffButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "" || PasswordTextBox.Text == "" ||
                NameTextBox.Text == "" || ContactNoBox.Text == "" ||
                AddressNoBox.Text == "" || LegalInfoTextBox.Text == "" ||
                RoleComboBox.Text == "")
            {
                MessageBox.Show("Invalid information.");
                return;
            }

            var user = new User()
            {
                Name = NameTextBox.Text,
                Username = UsernameTextBox.Text,
                Password = PasswordTextBox.Text,
                ContactNo = ContactNoBox.Text,
                Address = AddressNoBox.Text,
                LegalInfo = LegalInfoTextBox.Text,
                Role = RoleComboBox.Text
            };

            MessageBox.Show(_userLogics.AddUser(user));
            Refresh();
        }

        private void StaffsDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedUser = (User) StaffsDataGrid.SelectedItem;
            if(selectedUser==null) return;
            UsernameTextBox.Text = selectedUser.Username;
            PasswordTextBox.Text = selectedUser.Password;
            NameTextBox.Text = selectedUser.Name;
            ContactNoBox.Text = selectedUser.ContactNo;
            AddressNoBox.Text = selectedUser.Address;
            LegalInfoTextBox.Text = selectedUser.LegalInfo;

            UsernameTextBox.IsEnabled = false;
        }

        private void UpdateStaffButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "" || PasswordTextBox.Text == "" ||
                NameTextBox.Text == "" || ContactNoBox.Text == "" ||
                AddressNoBox.Text == "" || LegalInfoTextBox.Text == "" ||
                RoleComboBox.Text == "")
            {
                MessageBox.Show("Invalid information.");
                return;
            }

            var user = new User()
            {
                Name = NameTextBox.Text,
                Username = UsernameTextBox.Text,
                Password = PasswordTextBox.Text,
                ContactNo = ContactNoBox.Text,
                Address = AddressNoBox.Text,
                LegalInfo = LegalInfoTextBox.Text,
                Role = RoleComboBox.Text
            };

            MessageBox.Show(_userLogics.UpdateUser(user));

            Refresh();
        }
    }
}
