using System;
using System.Windows;
using System.Windows.Controls;
using POS.BusinessLogics;
using POS.Models;

namespace NURTEX_POS.Views
{
    /// <summary>
    /// Interaction logic for SalesReportControl.xaml
    /// </summary>
    public partial class SalesReportControl : UserControl
    {
        public SalesReportControl()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            FromDatePicker.SelectedDate = DateTime.Today;
            ToDatePicker.SelectedDate = DateTime.Today;
            ReceiptNoTextBox.Text = "";
            ProductCountDataGrid.ItemsSource = null;
            SalesReportDataGrid.ItemsSource = null;
        }

        private SaleLogics _saleLogics = new SaleLogics();

        private void DateButton_Click(object sender, RoutedEventArgs e)
        {
            var report = _saleLogics.SalesReport((DateTime)FromDatePicker.SelectedDate, (DateTime)ToDatePicker.SelectedDate);
            var productWiseReport = _saleLogics.ProductWiseSaleReport(DateTime.Today, DateTime.Today);
            SalesReportDataGrid.ItemsSource = report;
            ProductCountDataGrid.ItemsSource = productWiseReport;
        }

        private void TodayButton_OnClick(object sender, RoutedEventArgs e)
        {
            var report = _saleLogics.SalesReport(DateTime.Today, DateTime.Today);
            var productWiseReport = _saleLogics.ProductWiseSaleReport(DateTime.Today, DateTime.Today);
            SalesReportDataGrid.ItemsSource = report;
            ProductCountDataGrid.ItemsSource = productWiseReport;
        }

        private void ThisMonthButton_OnClick(object sender, RoutedEventArgs e)
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var report = _saleLogics.SalesReport(startDate, endDate);
            var productWiseReport = _saleLogics.ProductWiseSaleReport(startDate, endDate);
            SalesReportDataGrid.ItemsSource = report;
            ProductCountDataGrid.ItemsSource = productWiseReport;
            //MessageBox.Show(startDate+"\n"+endDate);
        }

        private void ThisYearButton_OnClick(object sender, RoutedEventArgs e)
        {
            var startDate = new DateTime(DateTime.Now.Year,1,1);
            var endDate = new DateTime(DateTime.Now.Year, 12, 31);
            var report = _saleLogics.SalesReport(startDate, endDate);
            var productWiseReport = _saleLogics.ProductWiseSaleReport(startDate, endDate);
            SalesReportDataGrid.ItemsSource = report;
            ProductCountDataGrid.ItemsSource = productWiseReport;
            //MessageBox.Show(startDate + "\n" + endDate);
        }

        private void SalesReportDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSale = (Sale) SalesReportDataGrid.SelectedItem;
            if(selectedSale!=null) ReceiptNoTextBox.Text = selectedSale.Id.ToString();
        }

        private void OpenReceiptButton_OnClick(object sender, RoutedEventArgs e)
        {
            if(ReceiptNoTextBox.Text=="") return;
            new ReceiptViewer(ReceiptNoTextBox.Text).ShowDialog();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
