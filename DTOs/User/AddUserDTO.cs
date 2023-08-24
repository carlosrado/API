namespace API.DTOs.User{
    public class AddUserDTO{
                public string? Name { get; set; }
                public string? Email { get; set; }
                public string? Password { get; set; }
                public int? Role { get; set;}
    }
}