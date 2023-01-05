namespace ES.Web.Models.DAO;

/// <summary>
/// Производственный заказ
/// </summary>
public record Order : Entity
{
    /// <summary>
    /// Название заказа
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Дата регистрации ПЗ
    /// </summary>
    public DateTime RegDate { get; set; }

    /// <summary>
    /// Заказ утвержден
    /// </summary>
    public bool IsApproved { get; set; }
    /// <summary>
    /// Заказ выполнен
    /// </summary>
    public bool IsCompleted { get; set; }
    /// <summary>
    /// Заказ проверен
    /// </summary>
    public bool IsChecked { get; set; }

    /// <summary>
    /// Части производственного заказа
    /// </summary>
    public ICollection<OrderPart> Parts { get; set; }
}
