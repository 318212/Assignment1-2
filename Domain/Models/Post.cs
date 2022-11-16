namespace Shared.Models;

public class Post
{
    public int Id { get; set; }
    public User owner { get; private set; }
    public string Title {get; private set; }
    public string Content {get; private set; }

    public Post(User owner, string title, string content)
    {
        this.owner = owner;
        Title = title;
        Content = content;

    }
    private Post(){}
}