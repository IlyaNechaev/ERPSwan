using System.ComponentModel.DataAnnotations.Schema;

namespace ES.Web.Models.DAO;

/// <summary>
/// Проводка
/// </summary>
public record Book : Entity
{
    /// <summary>
    /// Дебет
    /// </summary>
    public BookEntry? DebetEntry { get; set; }

    public Guid? DebetID { get; set; }

    /// <summary>
    /// Кредит
    /// </summary>
    public BookEntry? CreditEntry { get; set; }

    public Guid? CreditID { get; set; }

    /// <summary>
    /// Сумма
    /// </summary>
    public float Sum { get; set; }

    /// <summary>
    /// Дата регистрации проводки
    /// </summary>
    public DateTime RegDate { get; set; }

    /// <summary>
    /// Производственный заказ
    /// </summary>
    public Order Order { get; set; }

    public Guid OrderID { get; set; }
}