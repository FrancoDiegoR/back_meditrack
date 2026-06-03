using System.Text.Json.Serialization;

namespace TechnoByteLambders.MediTrackSensor.Platform.Iam.Domain.Model.Aggregates;

public partial class User(string name, string dni, string email, string phone, string jobTitle, DateOnly entryDate, string role, string passwordHash, string photo)
{
    public User() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, DateOnly.MinValue, string.Empty, string.Empty, string.Empty)
    {
    }

    public int Id { get; }
    public string Name { get; private set; } = name;
    public string Dni { get; private set; } = dni;
    public string Email { get; private set; } = email;
    public string Phone { get; private set; } = phone;
    public string JobTitle { get; private set; } = jobTitle;
    public DateOnly EntryDate { get; private set; } = entryDate;
    public string Role { get; private set; } = role;
    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
    public string Photo { get; private set; } = photo;

    public User UpdateName(string name)
    {
        Name = name;
        return this;
    }

    public User UpdateEmail(string email)
    {
        Email = email;
        return this;
    }

    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }

    public User UpdatePhone(string phone)
    {
        Phone = phone;
        return this;
    }

    public User UpdatePhoto(string photo)
    {
        Photo = photo;
        return this;
    }
}
