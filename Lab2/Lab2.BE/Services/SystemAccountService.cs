using BusinessObjects;
using Repositories;

namespace Services;

public class SystemAccountService : ISystemAccountService
{
    private readonly ISystemAccountRepository _repository;

    public SystemAccountService(ISystemAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<SystemAccount?> Login(string email, string password)
        => await _repository.Login(email, password);
}