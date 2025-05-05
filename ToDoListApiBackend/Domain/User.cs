namespace ToDoListApiBackend.Domain;

public class User
{
    public int UserId { get; private set; }
    public Guid UserGuid { get; private set; } 
    public string UserName { get; private set; } = string.Empty;
    public string UserSurname { get; private set; } = string.Empty;
    public string UserEmail { get; private set; } = string.Empty;
    public DateTime UserCreatedOn { get; private set; }
    public byte[] Salt { get; private set; }
    public byte[] HashCode { get; private set; }


    // constructor that take the dto from frontend and saves it into db
    public User(Guid userGuid, string userName, string userSurname, string userEmail, DateTime userCreatedOn, byte[] salt, byte[] hashCode)
    {
        UserGuid = userGuid;
        UserName = userName;
        UserSurname = userSurname;
        UserEmail = userEmail;
        UserCreatedOn = userCreatedOn;
        Salt = salt;
        HashCode = hashCode;
    }

    // constructor that take the user from db and send it out
    public User(int userId, Guid userGuid, string userName, string userSurname, string userEmail, DateTime userCreatedOn, byte[] salt, byte[] hashCode)
    {
        UserId = userId;
        UserGuid = userGuid;
        UserName = userName;
        UserSurname = userSurname;
        UserEmail = userEmail;
        UserCreatedOn = userCreatedOn;
        Salt = salt;
        HashCode = hashCode;
    }

}
