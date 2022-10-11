namespace Shared.DTOs;

public class SearchPostParametersDto
{
    public string? Username { get; }
    public int? UserId { get; }
    public string? TitleContains { get; }
    public string? ContentContains { get; }
}