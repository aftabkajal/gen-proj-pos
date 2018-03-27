using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using POS.BusinessLogics;
using POS.DataAccess;
using POS.Models;

namespace AppOperations
{
    public static class AppOperations
    {
        private static User _currentUser = null;
        private static UserLogics _userLogics = new UserLogics();

        public static bool CheckConnection()
        {
            return NurTexDbContext.GetContext().Database.Exists();
        }

        public static bool Login(string username, string password)
        {
            if (!_userLogics.Authenticate(username, password)) return false;
            _currentUser = _userLogics.GetUserByUsername(username);
            return true;
        }

        public static User GetCurrentUser()
        {
            return _currentUser;
        }

        public static void Logout()
        {
            _currentUser = null;
        }

        public static void MakePdf(Sale thisSale)
        {
            var totalDiscount = thisSale.Orders.Sum(order => order.Product.UnitSellingPrice - order.SellingPrice);

            MemoryStream myMemoryStream = new MemoryStream();
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    //Generate Invoice (Bill) Header.
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                    sb.Append("<tr><td align='center'><b>...::: NURTEX Lifestyle :::...</b></td></tr>");
                    sb.Append("<tr><td align='center'><b>Purobi, Mirpur-6, Dhaka.</b></td></tr>");
                    sb.Append("<tr><td colspan = '2'></td></tr>");
                    sb.Append("<tr><td><b>Bill No: </b>");
                    sb.Append(thisSale.Id);
                    sb.Append("</td><td align = 'right'><b>Date: </b>");
                    sb.Append(thisSale.PurcheseDateTime);
                    sb.Append(" </td></tr>");
                    sb.Append("<tr><td colspan = '2'><b>Customer Name: </b>");
                    sb.Append(thisSale.CustomerName);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td colspan = '2'><b>Customer Address: </b>");
                    sb.Append(thisSale.Address);
                    sb.Append("</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");

                    //Generate Invoice (Bill) Items Grid.
                    sb.Append("<table border = '1'>");
                    sb.Append("<tr>");
                    sb.Append("<th align='center'><b>Product</b></th>");
                    sb.Append("<th align='center'><b>Quantity</b></th>");
                    sb.Append("<th align='center'><b>Unit Price</b></th>");
                    sb.Append("<th align='center'><b>Discount</b></th>");
                    sb.Append("<th align='center'><b>Total</b></th>");
                    sb.Append("</tr>");
                    foreach (var order in thisSale.Orders)
                    {
                        sb.Append("<tr>");

                        sb.Append("<td>");
                        sb.Append(order.Product.Name);
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(order.Quantity);
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(order.SellingPrice);
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(order.Discount);
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(order.GetTotal());
                        sb.Append("</td>");

                        sb.Append("</tr>");
                    }
                    sb.Append("<tr><td align = 'right' colspan = '4'>Total Amount</td>");
                    sb.Append("<td>");
                    sb.Append(thisSale.GetTotalAmmount());
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td align = 'right' colspan = '4'>Product Discount</td>");
                    sb.Append("<td>");
                    sb.Append(totalDiscount);
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td align = 'right' colspan = '4'>Token Discount</td>");
                    sb.Append("<td>");
                    sb.Append(thisSale.GetTotalAmmount() * (thisSale.DiscountToken.Percentage / 100));
                    sb.Append("</td></tr>");
                    sb.Append("</table>");

                    //Export HTML String as PDF.
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A5, 30f, 30f, 30f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, myMemoryStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    byte[] content = myMemoryStream.ToArray();

                    // Write out PDF from memory stream.
                    if (!Directory.Exists("Bills")) Directory.CreateDirectory("Bills");
                    FileStream fs = File.Create("Bills/" + thisSale.Id + ".pdf");
                    fs.Write(content, 0, (int)content.Length);
                    fs.Close();
                }
            }
        }
    }
}
