using Console_CCServer.ResponsesLists.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Console_CCServer.ResponsesLists.ResponseLists
{
    class WorkerResponsesList : ResponseList
    {
        private static WorkerResponsesList manufacturersResponsesList;
        private WorkerResponsesList() : base() { }
        protected override void InitResponseList()
        {
            ListResponse.Add(new GetUserData());
            ListResponse.Add(new Logout());
        }
        public static WorkerResponsesList GetInstance()
        {
            return manufacturersResponsesList == null ? manufacturersResponsesList = new WorkerResponsesList() : manufacturersResponsesList;
        }
    }
}
