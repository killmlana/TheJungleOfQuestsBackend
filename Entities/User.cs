namespace TheJungleOfQuestsBackend.Entities;

public class User
{
    public Guid Id { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public long DateCreated { get; set; }
    public int Score { get; set; }

    public User()
    {
        DateCreated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        Score = 0;
        Id = Guid.NewGuid();
    }
}