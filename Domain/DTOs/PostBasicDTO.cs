namespace Shared.DTOs;

public class PostBasicDTO
{
    public int Id { get; }
    public int OwnerId { get; }
    public string Title { get; }
    public string Content { get; }

    public PostBasicDTO(int id, int ownerId, string title, string content)
    {
        Id = id;
        OwnerId = ownerId;
        Title = title;
        Content = content;
    }
}