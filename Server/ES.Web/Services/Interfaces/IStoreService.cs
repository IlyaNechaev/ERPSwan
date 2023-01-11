using ES.Web.Models.DAO;

namespace ES.Web.Services;

public interface IStoreService
{
    /// <summary>
    /// Зарезервировать материалы на складе
    /// </summary>
    /// <param name="orderPart"></param>
    /// <param name="materialId"></param>
    /// <param name="count"></param>
    public Task ReserveMaterialsAsync(OrderPart orderPart, Guid materialId, int count);

    /// <summary>
    /// Выделить материалы для ПЗ со склада
    /// </summary>
    public Task AllocateMaterialsAsync(Guid orderPartId);

    /// <summary>
    /// Убрать резервацию материалов
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Task UnreserveMaterialsAsync(Guid orderId);
}
