using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using POS.BusinessLogics;
using POS.Models;

namespace NURTEX_POS.Views
{
    /// <summary>
    /// Interaction logic for AddControll.xaml
    /// </summary>
    public partial class NewSaleControl : UserControl
    {
        private ProductLogics _productLogics = new ProductLogics();
        private SaleLogics _saleLogics = new SaleLogics();
        private DiscountTokenLogics _discountTokenLogics = new DiscountTokenLogics();
        private CustomerLogics _customerLogics = new CustomerLogics();

        private Order currentOrder;
        private Sale currentSale = new Sale() {Orders = new List<Order>()};
        public NewSaleControl()
        {
            InitializeComponent();
        }

        private void ShowCurrentOrder()
        {
            TextBoxOrderQuantity.Text = currentOrder.Quantity.ToString();
            TextBoxUnitPrice.Text = currentOrder.SellingPrice.ToString();
            TextBoxDiscount.Text = currentOrder.Discount.ToString();
            TextBoxItemTotal.Text = currentOrder.GetTotal().ToString();



            TextBlockProductDetails.Text = "Name \t: " + currentOrder.Product.Name + "\n" +
                                           "Brand \t: " + currentOrder.Product.Brand + "\n" +
                                           "Color \t: " + currentOrder.Product.Color + "\n" +
                                           "Size \t: " + currentOrder.Product.Size + "\n" +
                                           "Buy \t: " + currentOrder.Product.UnitBuyingPrice + "\n" +
                                           "Sale \t: " + currentOrder.Product.UnitSellingPrice + "\n" +
                                           "Disc. \t: " + currentOrder.Product.PossibleDiscountPercentage + "% \n" +
                                           "Stock \t: " + currentOrder.Product.Quantity;
        }

        private void Refresh()
        {
            ItemClear();

            var allTokens = _discountTokenLogics.GetAllTokens().ToList();
            allTokens.Add(new DiscountToken() {Id = 0,Title = "No discount"});
            DiscountDropdown.ItemsSource = allTokens.OrderBy(token => token.Id);
            DiscountDropdown.DisplayMemberPath = "Title";
            DiscountDropdown.SelectedValuePath = "Id";

            DiscountDropdown.SelectedIndex = 0;
        }

        private void ItemClear()
        {
            TextBoxOrderQuantity.Text="";
            TextBoxUnitPrice.Text="";
            TextBoxDiscount.Text="";
            TextBoxItemTotal.Text="";
            TextBlockProductDetails.Text = "";
            TextBoxSearch.Text="";
            TextBoxSearch.Focus();


            LabelNumberofItems.Content = currentSale.Orders.Count.ToString();
            LabelTotalAmmount.Content = currentSale.GetTotalAmmount();
            
            
            DataGridOrders.ItemsSource = null;
            DataGridOrders.ItemsSource = currentSale.Orders;
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxSearch.Text == null) return;
            var product = _productLogics.SearchProduct(TextBoxSearch.Text);
            if (product == null)
            {
                MessageBox.Show("Product not found.\nTry again.");
                return;
            }
            currentOrder = new Order {Product = product, Quantity = 1};
            currentOrder.SellingPrice = currentOrder.Product.UnitSellingPrice;
            currentOrder.Discount = 0;
            ShowCurrentOrder();

            TextBoxOrderQuantity.Focus();
        }

        private void TextBoxOrderQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int quantity;
            if (!int.TryParse(TextBoxOrderQuantity.Text, out quantity)) return;

            if (quantity > currentOrder.Product.Quantity)
            {
                MessageBox.Show("Insufficient stock.");
                ButtonAddToOrder.IsEnabled = false;
                return;
            }
            ButtonAddToOrder.IsEnabled = true;

            currentOrder.Quantity = quantity;
            ShowCurrentOrder();
        }

        private void TextBoxUnitPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            double unitPrice;
            if (!double.TryParse(TextBoxUnitPrice.Text, out unitPrice)) return;
            ButtonAddToOrder.IsEnabled = !(unitPrice < currentOrder.Product.UnitBuyingPrice);
            currentOrder.SellingPrice = unitPrice;
            ShowCurrentOrder();
        }

        private void TextBoxDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            double discount;
            if (!double.TryParse(TextBoxDiscount.Text, out discount))
            {
                if(currentOrder!=null) currentOrder.Discount = 0;
                return;
            }
            ButtonAddToOrder.IsEnabled = (discount <= currentOrder.Product.PossibleDiscountPercentage);
            currentOrder.Discount = discount;
            ShowCurrentOrder();
        }

        private void ButtonAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (currentOrder == null) return;
            currentSale.Orders.Add(currentOrder);
            currentOrder = null;
            ItemClear();
        }

        private void DiscountDropdown_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedVal = (DiscountToken)DiscountDropdown.SelectedItem;
            if (selectedVal == null || DiscountDropdown.SelectedIndex==0 ||
                MessageBox.Show("Are you sure to add discount coupon?\n" +
                                "All product discount will be removed.", "Confirmation",MessageBoxButton.YesNo)==MessageBoxResult.No)
                return;

            TextBoxDiscount.IsEnabled = false;

            //MessageBox.Show(selectedVal.Title + "\n" + selectedVal.Id + "\n" + DiscountDropdown.SelectedIndex);

            foreach (var order in currentSale.Orders)
            {
                order.Discount = 0;
            }
            currentSale.DiscountToken = selectedVal;
            ItemClear();
        }

        private void ButtonConfirmSale_Click(object sender, RoutedEventArgs e)
        {
            currentSale.PurcheseDateTime = DateTime.Now;
            currentSale.CustomerName = TextBoxCustomerName.Text;
            currentSale.Address = TextBoxCustomerAddress.Text;
            currentSale.Issuer = AppOperations.AppOperations.GetCurrentUser();
            var result = _saleLogics.SaveNewSale(currentSale);
            if (result == "Sale successfull")
            {
                try
                {
                    AppOperations.AppOperations.MakePdf(currentSale);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
                if(MessageBox.Show("Sale complete. Generate receipt?","Confirmation",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    new ReceiptViewer(currentSale).ShowDialog();
            }
            else
            {
                MessageBox.Show(result);
            }

            currentOrder = null;
            currentSale = new Sale() { Orders = new List<Order>() };
            ItemClear();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            currentOrder = null;
            currentSale = new Sale()
            {
                Orders = new List<Order>(), 
                DiscountToken = null//new DiscountToken()
            };
            Refresh();
        }

        private void ButtonDeleteOrder_OnClick(object sender, RoutedEventArgs e)
        {
            currentSale.Orders.Remove(currentOrder);
            ButtonDeleteOrder.IsEnabled = false;
            ItemClear();

        }

        private void DataGridOrders_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentOrder = (Order) DataGridOrders.SelectedItem;
            if (currentOrder == null) return;
            ShowCurrentOrder();
            ButtonDeleteOrder.IsEnabled = true;
        }

        private void TextBoxSearch_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                ButtonSearch_Click(new object(), new RoutedEventArgs());
            }
        }

        private void TextBoxCustomerId_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key!=Key.Enter || TextBoxCustomerId.Text=="") return;
            var customer = _customerLogics.FindCustomerById(Convert.ToInt32(TextBoxCustomerId.Text));
            if (customer == null)
            {
                MessageBox.Show("Customer not found.");
                return;
            }
            TextBoxCustomerName.Text = customer.Name;
            TextBoxCustomerAddress.Text = customer.Address;

            currentSale.Customer = customer;
        }
    }
}
