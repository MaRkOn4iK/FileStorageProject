using BLL.Validation.Exceptions;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation.UserValidators
{
    static public  class UserValidator
    {
        static public bool RegistrationValidation(string login, string password, string name, string lastName, string email, IUnitOfWork _unitOfWork)
        {
            var tmpList = _unitOfWork.UserRepository.GetAll();
            foreach (var item in tmpList)
            {
                if (item.Login == login)
                    throw new AuthException("This login is taken");
            }
            CheckData(login, password, name, lastName, email);
            return true;
        }

        private static void CheckData(string login, string password, string name, string lastName, string email)
        {
            if (string.IsNullOrEmpty(login) || login.Length < 3)
                throw new AuthException("This login is invalid");
            if (string.IsNullOrEmpty(password) || password.Length < 5)
                throw new AuthException("This password unreliable");
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName))
                throw new AuthException("Please input correct Name and Last Name");
            if (email == null || email.Length < 10 || !email.Contains('@'))
                throw new AuthException("This email is invalid");
        }

        static public bool UserInformationChangedValidator(string oldLogin, string newLogin, string newPassword, string newName, string newLastName, string newEmail, IUnitOfWork _unitOfWork)
        {
            var tmpList = _unitOfWork.UserRepository.GetAll();
            if (oldLogin != newLogin)
            {
                foreach (var item in tmpList)
                {
                    if (item.Login == newLogin)
                        throw new AuthException("This login is taken");
                }
            }
            CheckData(newLogin, newPassword, newName, newLastName, newEmail);
            return true;
        }
    }
}
