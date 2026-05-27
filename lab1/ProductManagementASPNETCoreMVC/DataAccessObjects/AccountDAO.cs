using System.Linq;
using BusinessObjects;

namespace DataAccessObjects
{
    public class AccountDAO
    {
        public static AccountMember GetAccountById(string accountID)
        {
            using var db = new MyStoreContext();
            return db.AccountMembers.FirstOrDefault(a => a.MemberId.Equals(accountID))!;
        }
    }
}

