namespace ES.Web.Models.DAO;

public record Material : Entity
{
    /// <summary>
    /// Название материала
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Хранящееся на складе количество материала
    /// </summary>
    public int CountStored { get; set; }

    /// <summary>
    /// Зарезервированное количество материала
    /// </summary>
    public int CountReserved { get; set; }
}
