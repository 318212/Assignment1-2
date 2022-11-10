namespace Shared.DTOs;

public class UserBasicDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    //public string Password { get; set; }

    public UserBasicDto(int id, string username)
    {
        Id = id;
        Username = username;
        
    }
}