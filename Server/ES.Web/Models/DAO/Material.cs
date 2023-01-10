namespace ES.Web.Models.DAO;

public record Material : Entity
{
    /// <summary>
    /// Название материала
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Артикул
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Единицы измерения
    /// </summary>
    public MeasureUnits Units { get; set; }

    /// <summary>
    /// Хранящееся на складе количество материала
    /// </summary>
    public int CountStored { get; set; }

    /// <summary>
    /// Зарезервированное количество материала
    /// </summary>
    public int CountReserved { get; set; }
}

public enum MeasureUnits
{
    /// <summary>
    /// Метр 
    /// </summary>
    METER = 1,

    /// <summary>
    /// Штука
    /// </summary>
    PIECE = 2,

    /// <summary>
    /// Килограмм
    /// </summary>
    KILO = 3
}