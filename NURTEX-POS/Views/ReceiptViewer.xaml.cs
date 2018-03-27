using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using POS.BusinessLogics;
using POS.Models;

namespace NURTEX_POS.Views
{
    /// <summary>
    /// Interaction logic for ReceiptViewer.xaml
    /// </summary>
    public partial class ReceiptViewer : Window
    {
        //private AppOperations appOperations = new AppOperations();
        public ReceiptViewer(Sale sale)
        {
            InitializeComponent();
            WebBrowser1.Navigate(Environment.CurrentDirectory + @"\Bills\"+sale.Id+".pdf");
        }

        public ReceiptViewer(string saleId)
        {
            InitializeComponent();
            if(File.Exists(Environment.CurrentDirectory + @"\Bills\" + saleId + ".pdf"))
            {
                WebBrowser1.Navigate(Environment.CurrentDirectory + @"\Bills\" + saleId + ".pdf");
            }
            else
            {
                var sale = new SaleLogics().GetSaleById(saleId);
                AppOperations.AppOperations.MakePdf(sale);
                WebBrowser1.Navigate(Environment.CurrentDirectory + @"\Bills\" + saleId + ".pdf");
            }
        }
    }
}
