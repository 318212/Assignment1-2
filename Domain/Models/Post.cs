namespace Shared.Models;

public class Post
{
    public int Id { get; set; }
    public User owner { get; }
    public string Title {get;}
    public string Content {get;}

    public Post(User owner, string title, string content)
    {
        this.owner = owner;
        Title = title;
        Content = content;

    }
}