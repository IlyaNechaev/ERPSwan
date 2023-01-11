namespace ES.Web.Models;

public record User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DateTime BirthDay { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
}

public enum UserRole
{
    /// <summary>
    /// Администратор
    /// </summary>
    ADMIN = 1 << 0,

    /// <summary>
    /// Бухгалтер
    /// </summary>
    BOOKER = 1 << 2,

    /// <summary>
    /// Рабочий
    /// </summary>
    WORKER = 1 << 3,

    /// <summary>
    /// Бригадир
    /// </summary>
    FOREMAN = 1 << 4
}