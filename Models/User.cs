using System;

namespace WebApi.Models {
    public class User {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
