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
    GUEST = 0,
    ADMIN = 1 << 0,
    USER = 1 << 1,
    SECRETARY = 1 << 2
}