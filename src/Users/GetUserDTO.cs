namespace Totter.Users;

public class GetUserDTO {
    public long Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime LastLoggedIn { get; set; }
}
