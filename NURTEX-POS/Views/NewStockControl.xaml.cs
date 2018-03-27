using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using POS.BusinessLogics;
using POS.Models;

namespace NURTEX_POS.Views
{
    /// <summary>
    /// Interaction logic for NewStockControl.xaml
    /// </summary>
    public partial class NewStockControl : UserControl
    {
        public NewStockControl()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            DatePickerEntryDate.SelectedDate = DateTime.Now;
            TextBoxProductId.Text = "";
            TextBoxProductname.Text = "";
            TextBoxBrand.Text = "";
            TextBoxColor.Text = "";
            TextBoxSize.Text = "";
            TextBoxUnitBuyingPrice.Text = "";
            TextBoxUnitSellingPrice.Text = "";
            TextBoxQuantity.Text = "";
            TextBoxPossibleDiscount.Text = "";
        }
        
        private ProductLogics _procuctLogics = new ProductLogics();
        private List<Product> latestProducts = new List<Product>();

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            double buy, sale, discount;
            int quantity;

            if ( string.IsNullOrEmpty(TextBoxProductId.Text)|| string.IsNullOrEmpty(TextBoxProductname.Text) || string.IsNullOrEmpty(TextBoxBrand.Text) || string.IsNullOrEmpty(TextBoxColor.Text) ||
                !double.TryParse(TextBoxUnitBuyingPrice.Text, out buy) || !double.TryParse(TextBoxUnitSellingPrice.Text, out sale) ||
                !int.TryParse(TextBoxQuantity.Text,out quantity) || !double.TryParse(TextBoxPossibleDiscount.Text, out discount))
            {
                MessageBox.Show("Requered inputs are not supplied.");
                return;
            }
            var newProduct = new Product()
            {
                Brand = TextBoxBrand.Text,
                UnitBuyingPrice = Convert.ToDouble(TextBoxUnitBuyingPrice.Text),
                Color = TextBoxColor.Text,
                EntryDateTime = DateTime.Now,
                Name = TextBoxProductname.Text,
                ProductId = TextBoxProductId.Text,
                UnitSellingPrice = Convert.ToDouble(TextBoxUnitSellingPrice.Text),
                Size = TextBoxSize.Text,
                Quantity = Convert.ToInt32(TextBoxQuantity.Text),
                PossibleDiscountPercentage = Convert.ToDouble(TextBoxPossibleDiscount.Text)
            };
            var result = _procuctLogics.AddProduct(newProduct);

            if (result == "Product Added Successfully.") latestProducts.Add(newProduct);
            DataGridInventory.ItemsSource = null;
            DataGridInventory.ItemsSource = latestProducts;
            MessageBox.Show(result);
            Refresh();
        }
        private void ButtonClearall_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            latestProducts=new List<Product>();
            Refresh();
            DataGridInventory.ItemsSource = null;
        }
    }
}
