using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;

namespace ES.Web.Models.DAO;

/// <summary>
/// Часть производственного заказа
/// </summary>
public record OrderPart : Entity
{
    /// <summary>
    /// Порядковый номер части производственного заказа
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// Дата выполнения части производственного заказа
    /// </summary>
    public Nullable<DateTime> EndDate { get; set; }

    /// <summary>
    /// Заказ выполнен
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Идентификатор <see cref="Order"/>
    /// </summary>
    public Guid OrderID { get; set; }

    /// <summary>
    /// Производственный заказ
    /// </summary>
    public Order Order { get; set; }

    public ICollection<OrderMaterial> Materials { get; set; }

}
