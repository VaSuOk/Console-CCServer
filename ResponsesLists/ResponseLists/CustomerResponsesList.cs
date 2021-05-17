using Console_CCServer.ResponsesLists.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Console_CCServer.ResponsesLists.ResponseLists
{
    class CustomerResponsesList : ResponseList
    {
        private static CustomerResponsesList resellersResponsesList;
        private CustomerResponsesList() : base() { }
        protected override void InitResponseList()
        {
            ListResponse.Add(new GetUserData());
            ListResponse.Add(new Logout());
        }
        public static CustomerResponsesList GetInstance()
        {
            return resellersResponsesList == null ? resellersResponsesList = new CustomerResponsesList() : resellersResponsesList;
        }
    }
}
