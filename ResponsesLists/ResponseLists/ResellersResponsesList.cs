using Console_CCServer.ResponsesLists.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Console_CCServer.ResponsesLists.ResponseLists
{
    class ResellersResponsesList : ResponseList
    {
        private static ResellersResponsesList resellersResponsesList;
        private ResellersResponsesList() : base() { }
        protected override void InitResponseList() { /*ДОПИСАТИ*/}
        public static ResellersResponsesList GetInstance()
        {
            return resellersResponsesList == null ? resellersResponsesList = new ResellersResponsesList() : resellersResponsesList;
        }
    }
}
