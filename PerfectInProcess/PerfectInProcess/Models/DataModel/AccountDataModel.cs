using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace PerfectInProcess.Models.DataModel
{
    public class AccountDataModel : BaseDataModel
    {
        public RoleDataModel Role { get; private set; }
        public ArrayList listOfErrors = new ArrayList();

        public int AccountId {get; set;}
        public string UserName { get; set; }  
        public string Email { get; set; }               
        public string FirstName { get; set; }     
        public string LastName { get; set; }

        //empty constructor
        public AccountDataModel()
        {

        }
        public AccountDataModel(string userName,string email, string firstName,string lastName)
        {      

            UserName = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
       
    }
   
}