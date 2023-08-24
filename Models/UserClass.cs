
namespace API.Models{
    public class UserClass{
        public int Id { get; set; } = 0;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public int? Role { get; set;}

    }
}

