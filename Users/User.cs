using System;
using System.Collections.Generic;
using System.Text;

namespace Console_CCServer.Users
{
    public enum UserType
    {
        Unregistered,
        Customer,
        Worker,
        Moderator
    }
    public class User
    {
        #region Data fields
        protected uint id;
        protected UserType userType;
        protected string Name;
        protected string Surname;
        protected string Email;
        protected string Phone;
        #endregion

        

        #region Setters and Getters
        public UserType GetUserType() { return this.userType; }
        #endregion

        #region Methods
        public static UserType ConvertToEnum(string UEnum)
        {
            switch (UEnum)
            {
                case "Customer":
                    {
                        return UserType.Customer;
                    }
                case "Worker":
                    {
                        return UserType.Worker;
                    }
                case "Moderator":
                    {
                        return UserType.Moderator;
                    }
                default: return UserType.Unregistered;
            }
        }
        #endregion
    }
}
