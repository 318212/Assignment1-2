namespace Shared.DTOs;

public class UserBasicDto
{
    public string Username { get; set; }
    public string Password { get; set; }

    public UserBasicDto(string username, string password)
    {
        Username = username;
        Password = password;
    }
}