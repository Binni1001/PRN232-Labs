using BusinessObjects;
using DataAccessObjects;

namespace Repositories;

public class CosmeticInformationRepository : ICosmeticInformationRepository
{
    public async Task<List<CosmeticInformation>> GetAllCosmetics()
        => await CosmeticInformationDAO.Instance.GetAllCosmetics();

    public async Task<CosmeticInformation?> GetOne(string id)
        => await CosmeticInformationDAO.Instance.GetById(id);

    public async Task<CosmeticInformation> Add(CosmeticInformation cosmeticInformation)
        => await CosmeticInformationDAO.Instance.AddCosmeticInformation(cosmeticInformation);

    public async Task<CosmeticInformation> Update(CosmeticInformation cosmeticInformation)
        => await CosmeticInformationDAO.Instance.Update(cosmeticInformation);

    public async Task<CosmeticInformation> Delete(string id)
        => await CosmeticInformationDAO.Instance.Delete(id);

    public async Task<List<CosmeticCategory>> GetAllCategories()
        => await CosmeticInformationDAO.Instance.GetAllCategories();
}