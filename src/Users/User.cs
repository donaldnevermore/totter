using System;

namespace Totter.Users {
    public class User {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; } = "";
        public string Avatar { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime LastLoggedIn { get; set; }
    }
}
