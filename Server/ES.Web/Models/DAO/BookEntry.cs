namespace ES.Web.Models.DAO;

public record BookEntry : Entity
{
    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
}
