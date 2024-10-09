namespace TheJungleOfQuestsBackend.Entities;

public class User
{
    public int Id { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public int DateCreated { get; set; }
    public int Score { get; set; }
}