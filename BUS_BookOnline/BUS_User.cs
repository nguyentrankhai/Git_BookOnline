﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_BookOnline;
using DAL_BookOnline;
using API_BookOnline;
namespace BUS_BookOnline
{
    public class BUS_User
    {
        DAL_User dalUser = new DAL_User();
        
        public bool buyBook(User user, Book book)
        {
            return dalUser.buyBook(user, book);
        }
        public async Task<bool> insertUser(User user)
        {
            API_ResgisterAccount api = new API_ResgisterAccount();
            //return dalUser.insertUser(user);
            bool isApiRegister = await api.registerAccount(user);
            return dalUser.insertUser(user) && isApiRegister;
        }
        public async Task<bool> forgotPassword(User user, string newPassword)
        {
            API_ResetPassword api = new API_ResetPassword();
            bool isResetPassword = await api.forgotPassword(user, newPassword);
            return isResetPassword;
        }
        public bool updateUser(User user, string pathAvatar)
        {
            //return dalUser.insertUser(user);
            return dalUser.updateUser(user, pathAvatar);
        }
        public User getUser(String ID)
        {
            //  return dalUser.getUser(user);
            User user = dalUser.getUser(ID);
            if (user != null)
            {
                API_BookOfUser api = new API_BookOfUser();
                api.GetBookOfUser(user);
                return user;
            }
            return null;
        }
    }
}
