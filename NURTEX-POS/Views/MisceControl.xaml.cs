using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using POS.BusinessLogics;
using POS.Models;

namespace NURTEX_POS.Views
{
    /// <summary>
    /// Interaction logic for MisceControl.xaml
    /// </summary>
    public partial class MisceControl : UserControl
    {
        private DiscountTokenLogics _discountTokenLogics = new DiscountTokenLogics();
        private CustomerLogics _customerLogics = new CustomerLogics();
        public MisceControl()
        {
            InitializeComponent();
        }

        private void Refresh()
        {
            TitleTextBox.Text = "";
            PercentageTextBox.Text = "";
            TitleTextBox.IsEnabled = true;

            DiscountsDataGrid.ItemsSource = null;
            DiscountsDataGrid.ItemsSource = _discountTokenLogics.GetAllTokens();

            CustomerIdTextBox.Text = "";
            CustomerNameTextBox.Text = "";
            ContactNoTextBox.Text = "";
            CustomerAddressTextBox.Text = "";
            PointsTextBox.Text = "";
            CustomerIdTextBox.IsEnabled = true;

            CustomersDataGrid.ItemsSource = null;
            CustomersDataGrid.ItemsSource = _customerLogics.GetAllCustomers();
        }

        private void ShowCustomerDetals(Customer customer)
        {
            CustomerIdTextBox.Text = customer.Id.ToString();
            CustomerNameTextBox.Text = customer.Name;
            ContactNoTextBox.Text = customer.ContactNo;
            CustomerAddressTextBox.Text = customer.Address;
            PointsTextBox.Text = customer.Points.ToString();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            double percentage;
            if(string.IsNullOrEmpty(TitleTextBox.Text) ||
                !double.TryParse(PercentageTextBox.Text, out percentage)) return;

            var token = new DiscountToken()
            {
                Title = TitleTextBox.Text,
                Percentage = percentage
            };
            if(_discountTokenLogics.AddToken(token)== "Token Added.")
                Refresh();
        }

        private void CustomersDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = (Customer) CustomersDataGrid.SelectedItem;
            if(customer==null) return;
            ShowCustomerDetals(customer);
            CustomerIdTextBox.IsEnabled = false;
        }

        private void AddCustomerButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CustomerIdTextBox.Text != "" || ContactNoTextBox.Text=="")
            {
                MessageBox.Show("Invalid information.");
                return;
            }

            var customer = new Customer()
            {
                Name = CustomerNameTextBox.Text,
                Address = CustomerAddressTextBox.Text,
                ContactNo = ContactNoTextBox.Text
            };

            MessageBox.Show(_customerLogics.AddCustomer(customer));
            Refresh();
        }

        private void CustomerIdTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key!=Key.Enter || CustomerIdTextBox.Text=="") return;
            var customer = _customerLogics.FindCustomerById(Convert.ToInt32(CustomerIdTextBox.Text));
            if (customer == null)
            {
                MessageBox.Show("Customer Not found.");
                return;
            }
            ShowCustomerDetals(customer);
        }

        private void DiscountsDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var val = (DiscountToken) DiscountsDataGrid.SelectedItem;
            if (val == null) return;

            TitleTextBox.Text = val.Title;
            PercentageTextBox.Text = val.Percentage.ToString();
            TitleTextBox.IsEnabled = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_discountTokenLogics.DeleteToken(TitleTextBox.Text));
            Refresh();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            double percentage;
            if (string.IsNullOrEmpty(TitleTextBox.Text) ||
                !double.TryParse(PercentageTextBox.Text, out percentage)) return;

            var token = new DiscountToken()
            {
                Title = TitleTextBox.Text,
                Percentage = percentage
            };
            MessageBox.Show(_discountTokenLogics.Update(token));
            Refresh();
        }

        private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var thisCustomer = new Customer()
            {
                Id = Convert.ToInt16(CustomerIdTextBox.Text),
                Name = CustomerNameTextBox.Text,
                ContactNo = ContactNoTextBox.Text,
                Address = CustomerAddressTextBox.Text
            };

            MessageBox.Show(_customerLogics.UpdateCustomer(thisCustomer));
            Refresh();
        }
    }
}
