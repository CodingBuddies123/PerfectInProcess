using System.Collections;

namespace PerfectInProcess.Models.DataModel
{
    public class AccountDataModel : BaseDataModel
    {
        public RoleDataModel Role { get; private set; }
        public ArrayList listOfErrors = new ArrayList();

        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //empty constructor
        public AccountDataModel()
        {
            Role = new RoleDataModel(1);
        }

        public AccountDataModel(int AccountId, string UserName, string Email, string FirstName, string LastName)
        {
            this.AccountId = AccountId;
            this.UserName = UserName;
            this.Email = Email;
            this.FirstName = FirstName;
            this.LastName = LastName;

            Role = new RoleDataModel(1);
        }
    }
}