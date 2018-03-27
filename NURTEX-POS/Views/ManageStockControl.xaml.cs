using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using POS.Models;
using POS.BusinessLogics;

namespace NURTEX_POS.Views
{
    /// <summary>
    /// Interaction logic for ManageStockControl.xaml
    /// </summary>
    public partial class ManageStockControl : UserControl
    {
        private ProductLogics productLogics = new ProductLogics();
        public ManageStockControl()
        {
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            TextBoxProductId.Text = "";
            TextBoxProductname.Text = "";
            TextBoxBrand.Text = "";
            TextBoxColor.Text = "";
            TextBoxPossibleDiscount.Text = "";
            TextBoxQuantity.Text = "";
            TextBoxSize.Text = "";
            TextBoxUnitBuyingPrice.Text = "";
            TextBoxUnitSellingPrice.Text = "";
            DataGridInventory.ItemsSource = productLogics.GetAllProduct();
        }

        private void DataGridInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = (Product)DataGridInventory.SelectedItem;
            if (selectedRow == null) return;
            ShowProduct(selectedRow);
        }

        private void ShowProduct(Product product)
        {
            TextBoxProductId.Text = product.ProductId;
            TextBoxProductname.Text = product.Name;
            TextBoxBrand.Text = product.Brand;
            TextBoxColor.Text = product.Color;
            TextBoxPossibleDiscount.Text = product.PossibleDiscountPercentage.ToString();
            TextBoxQuantity.Text = product.Quantity.ToString();
            TextBoxSize.Text = product.Size;
            TextBoxUnitBuyingPrice.Text = product.UnitBuyingPrice.ToString();
            TextBoxUnitSellingPrice.Text = product.UnitSellingPrice.ToString();
            DatePickerEntryDate.SelectedDate = product.EntryDateTime;
        }

        private void ButtonDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete the product?", "Confirmaion", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            var selectedRow = (Product)DataGridInventory.SelectedItem;
            MessageBox.Show(productLogics.DeleteProduct(selectedRow));
            Refresh();
        }

        private void ButtonUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to update the product?", "Confirmaion", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            var selectedRow = (Product)DataGridInventory.SelectedItem;
            selectedRow.Brand = TextBoxBrand.Text;
            selectedRow.UnitBuyingPrice = Convert.ToDouble(TextBoxUnitBuyingPrice.Text);
            selectedRow.Color = TextBoxColor.Text;
            selectedRow.EntryDateTime = DateTime.Now;
            selectedRow.Name = TextBoxProductname.Text;
            selectedRow.UnitSellingPrice = Convert.ToDouble(TextBoxUnitSellingPrice.Text);
            selectedRow.Size = TextBoxSize.Text;
            selectedRow.Quantity = Convert.ToInt32(TextBoxQuantity.Text);
            selectedRow.PossibleDiscountPercentage = Convert.ToDouble(TextBoxPossibleDiscount.Text);
            MessageBox.Show(productLogics.UpdateProduct(selectedRow));
            Refresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void TextBoxProductId_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key!=Key.Enter || TextBoxProductId.Text=="") return;
            var product = productLogics.GetAllProduct().FirstOrDefault(p => p.ProductId == TextBoxProductId.Text);
            ShowProduct(product);
        }
    }
}
