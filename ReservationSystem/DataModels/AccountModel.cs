using System.Text.Json.Serialization;


class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("date_of_birth")]
    public string DateOfBirth { get; set; }

    [JsonPropertyName("invalid")]
    public bool Invalid { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }

    public AccountModel(int id, string emailAddress, string password, string fullName, string phone, string dateofbirth, bool invalid, string role)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        Phone = phone;
        DateOfBirth = dateofbirth;
        Invalid = invalid;
        Role = role;
    }

}




