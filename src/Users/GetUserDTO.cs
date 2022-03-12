using System;

namespace Totter.Users {
    public class GetUserDTO {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime Date { get; set; }
        public DateTime LastLoggedIn { get; set; }
    }
}
