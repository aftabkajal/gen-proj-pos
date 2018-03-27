using System;
using System.Collections.Generic;
using System.Linq;
using POS.DataAccess;
using POS.Models;

namespace POS.BusinessLogics
{
    public class DiscountTokenLogics
    {
        private DiscountTokenAccess _discountTokenAccess=new DiscountTokenAccess();

        public string AddToken(DiscountToken token)
        {
            try
            {
                var getToken = _discountTokenAccess.GetSingle(discountToken => discountToken.Title == token.Title);
                if (getToken != null) return "Token Already inserted.";
                return _discountTokenAccess.Add(token) ? "Token Added." : "Token doesnot added.";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<DiscountToken> GetAllTokens()
        {
            return _discountTokenAccess.GetAll().ToList();
        }

        public string DeleteToken(string title)
        {
            try
            {
                var getToken = _discountTokenAccess.GetSingle(discountToken => discountToken.Title == title);
                return _discountTokenAccess.Delete(getToken) ? "Token deleted." : "Token doesnot deleted.";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Update(DiscountToken dt)
        {
            try
            {
                var getToken = _discountTokenAccess.GetSingle(discountToken => discountToken.Title == dt.Title);
                getToken.Percentage = dt.Percentage;
                return _discountTokenAccess.Update(getToken) ? "Token updated." : "Token doesnot updated.";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
