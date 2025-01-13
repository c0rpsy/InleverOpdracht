using SQLite;

public class UserProfile
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Username { get; set; }

    public string Bio { get; set; }

    public string ProfileImagePath { get; set; }

    [NotNull, Unique]
    public string Email { get; set; }

    [NotNull]
    public string Password { get; set; }
}