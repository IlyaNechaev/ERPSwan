namespace ES.Web.Models.DAO;

/// <summary>
/// Производственный заказ
/// </summary>
public record Order : Entity
{
    /// <summary>
    /// Номер ПЗ
    /// </summary>
    public int Number { get; set; }

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
    /// Заказ отмене
    /// </summary>
    public bool IsCanceled { get; set; }

    /// <summary>
    /// Итоговая сумма ПЗ
    /// </summary>
    public float? Sum { get; set; }

    /// <summary>
    /// Части производственного заказа
    /// </summary>
    public ICollection<OrderPart> Parts { get; set; }

    /// <summary>
    /// Проводки
    /// </summary>
    public ICollection<Book> Books { get; set; }

    /// <summary>
    /// Бригадир
    /// </summary>
    public User Foreman { get; set; }

    /// <summary>
    /// Идентификатор бригадира
    /// </summary>
    public Guid ForemanID { get; set; }
}
