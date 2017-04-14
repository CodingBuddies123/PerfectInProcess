using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PerfectInProcess.Models.DataModel
{
    public class AccountDataModel : BaseDataModel
    {
        public RoleDataModel Role { get; private set; }
       
    }
}