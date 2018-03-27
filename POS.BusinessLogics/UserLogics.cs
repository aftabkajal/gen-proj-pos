using System;
using System.Collections.Generic;
using System.Linq;
using POS.DataAccess;
using POS.Models;

namespace POS.BusinessLogics
{
    public class UserLogics
    {
        private UserAccess _userAccess = new UserAccess();


        public List<User> GetAllUsers()
        {
            return _userAccess.GetAll().ToList();
        } 
        public string AddUser(User user)
        {
            try
            {
                if (_userAccess.GetSingle(u => u.Username == user.Username) != null)
                    return "Duplicate username.";
                return _userAccess.Add(user) ? "Added successfully." : "Doesn't Added.";
            }
            catch (Exception e)
            {
                return "In AddUser " + e.Message;
            }
        }

        public string UpdateUser(User user)
        {
            try
            {
                var auser = _userAccess.GetSingle(u => u.Username == user.Username);
                auser.Address = user.Address;
                auser.ContactNo = user.ContactNo;
                auser.LegalInfo = user.LegalInfo;
                auser.Name = user.Name;
                auser.Password = user.Name;
                auser.Role = user.Role;

                return _userAccess.Update(auser) ? "Updated successfully." : "Doesn't Updated.";
            }
            catch (Exception e)
            {
                return "In UpdateUser " + e.Message;
            }
        }

        public User GetUserByUsername(string username)
        {
            return _userAccess.GetSingle(u => u.Username == username);
        }

        public bool Authenticate(string username, string password)
        {
            try
            {
                var user = _userAccess.GetSingle(u => u.Username == username);
                return user != null && user.Password == password;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
