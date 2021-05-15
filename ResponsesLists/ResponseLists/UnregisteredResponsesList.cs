using Console_CCServer.ResponsesLists.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Console_CCServer.ResponsesLists.ResponseLists
{
    class UnregisteredResponsesList : ResponseList
    {
        
        private static UnregisteredResponsesList unregisteredResponsesList;
        protected override void InitResponseList()
        {
            ListResponse.Add(new RegistrationResponse());
            ListResponse.Add(new LoginResponse());
            ListResponse.Add(new TestImage());
            Console.WriteLine("Вроді викликався цей метод!");
        }

        private UnregisteredResponsesList() : base() { }
        public static UnregisteredResponsesList GetInstance()
        {
            return unregisteredResponsesList == null ? (unregisteredResponsesList = new UnregisteredResponsesList()) : unregisteredResponsesList;
        }
        
    }
}
