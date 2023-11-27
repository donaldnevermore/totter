namespace Totter.Users;

public class GetUserDto {
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime LastLoggedIn { get; set; }
}
