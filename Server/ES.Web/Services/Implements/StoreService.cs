using ES.Web.Data;
using ES.Web.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace ES.Web.Services;

public class StoreService : IStoreService
{
    ESDbContext _context;
    public StoreService(ESDbContext context)
    {
        _context = context;
    }
    public async Task ReserveMaterialsAsync(OrderPart orderPart, Guid materialId, int count)
    {
        var material = await _context.Materials.SingleOrDefaultAsync(m => m.ObjectID == materialId);

        if (material is null)
        {
            throw new ArgumentException($"Материал {materialId} не существует");
        }

        if (material.CountStored - material.CountReserved < count)
        {
            throw new ArgumentException($"Недостаточно материала \"{material.Name}\".\nНеобходимо: {count}\nДоступно: {material.CountStored - material.CountReserved}");
        }

        material.CountReserved += count;

        var orderMaterial = new OrderMaterial
        {
            Part = orderPart,
            Material = material,
            Count = count,
            Sum = count * material.Price
        };

        await _context.OrderMaterials.AddAsync(orderMaterial);

        await _context.SaveChangesAsync();
    }

    public async Task AllocateMaterialsAsync(Guid orderPartId)
    {
        var orderMaterials = _context.OrderMaterials
            .AsQueryable()
            .Include(om => om.Material)
            .Where(om => om.PartID == orderPartId).ToArray();

        Parallel.ForEach(orderMaterials, om =>
        {
            om.Material.CountReserved -= om.Count;
            om.Material.CountStored -= om.Count;
        });

        await _context.SaveChangesAsync();
    }

    public async Task UnreserveMaterialsAsync(Guid orderId)
    {
        var orderMaterials = await _context.Orders
            .AsQueryable()
            .Include(o => o.Parts)
            .ThenInclude(p => p.Materials)
            .ThenInclude(om => om.Material)
            .SelectMany(o => o.Parts.SelectMany(p => p.Materials))
            .ToArrayAsync();

        foreach (var orderMaterial in orderMaterials)
        {
            var material = orderMaterial.Material;
            material.CountReserved -= orderMaterial.Count;
        }

        await _context.SaveChangesAsync();
    }
}
