﻿namespace Totter.Users;

public class UpdateUserDTO {
    public long Id { get; set; }
    public string NickName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
}
