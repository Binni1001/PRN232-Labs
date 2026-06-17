using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public class CosmeticInformationDAO
{
    private static CosmeticInformationDAO? instance;

    private CosmeticInformationDAO() { }

    public static CosmeticInformationDAO Instance
    {
        get
        {
            instance ??= new CosmeticInformationDAO();
            return instance;
        }
    }

    public async Task<List<CosmeticInformation>> GetAllCosmetics()
    {
        using var context = new CosmeticsDbContext();

        return await context.CosmeticInformations
            .Include(x => x.Category)
            .ToListAsync();
    }

    public async Task<List<CosmeticCategory>> GetAllCategories()
    {
        using var context = new CosmeticsDbContext();

        return await context.CosmeticCategories.ToListAsync();
    }

    public async Task<CosmeticInformation?> GetById(string id)
    {
        using var context = new CosmeticsDbContext();

        return await context.CosmeticInformations
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.CosmeticId == id);
    }

    public async Task<CosmeticInformation> AddCosmeticInformation(CosmeticInformation cosmeticInformation)
    {
        using var context = new CosmeticsDbContext();

        var category = await context.CosmeticCategories
            .FirstOrDefaultAsync(x => x.CategoryId == cosmeticInformation.CategoryId);

        if (category == null)
            throw new Exception("Category is not found");

        cosmeticInformation.CosmeticId = GenerateId();

        await context.CosmeticInformations.AddAsync(cosmeticInformation);
        await context.SaveChangesAsync();

        return cosmeticInformation;
    }

    public async Task<CosmeticInformation> Update(CosmeticInformation cosmeticInformation)
    {
        using var context = new CosmeticsDbContext();

        var updateObject = await context.CosmeticInformations
            .FirstOrDefaultAsync(x => x.CosmeticId == cosmeticInformation.CosmeticId);

        if (updateObject == null)
            throw new Exception("CosmeticInformation not found");

        var category = await context.CosmeticCategories
            .FirstOrDefaultAsync(x => x.CategoryId == cosmeticInformation.CategoryId);

        if (category == null)
            throw new Exception("Category not found");

        updateObject.CosmeticName = cosmeticInformation.CosmeticName;
        updateObject.SkinType = cosmeticInformation.SkinType;
        updateObject.ExpirationDate = cosmeticInformation.ExpirationDate;
        updateObject.CosmeticSize = cosmeticInformation.CosmeticSize;
        updateObject.DollarPrice = cosmeticInformation.DollarPrice;
        updateObject.CategoryId = cosmeticInformation.CategoryId;

        await context.SaveChangesAsync();

        return updateObject;
    }

    public async Task<CosmeticInformation> Delete(string id)
    {
        using var context = new CosmeticsDbContext();

        var deleteObject = await context.CosmeticInformations
            .FirstOrDefaultAsync(x => x.CosmeticId == id);

        if (deleteObject == null)
            throw new Exception("CosmeticInformation not found");

        context.CosmeticInformations.Remove(deleteObject);
        await context.SaveChangesAsync();

        return deleteObject;
    }

    private string GenerateId()
    {
        var random = new Random();
        return "PL" + random.Next(100000, 999999);
    }
}