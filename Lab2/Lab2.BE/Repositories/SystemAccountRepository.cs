using BusinessObjects;
using DataAccessObjects;

namespace Repositories;

public class SystemAccountRepository : ISystemAccountRepository
{
    public async Task<SystemAccount?> Login(string email, string password)
        => await SystemAccountDAO.Instance.Login(email, password);
}