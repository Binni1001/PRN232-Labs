using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public class SystemAccountDAO
{
    private static SystemAccountDAO? instance;

    private SystemAccountDAO() { }

    public static SystemAccountDAO Instance
    {
        get
        {
            instance ??= new SystemAccountDAO();
            return instance;
        }
    }

    public async Task<SystemAccount?> Login(string email, string password)
    {
        using var context = new CosmeticsDbContext();

        return await context.SystemAccounts
            .FirstOrDefaultAsync(x =>
                x.EmailAddress == email &&
                x.AccountPassword == password);
    }
}