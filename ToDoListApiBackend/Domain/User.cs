using LusiUtilsLibrary.Backend.Crypting;
using ToDoListApiBackend.Application.DTOs;

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

    public User(int userId, Guid userGuid, string userName, string userSurname, string userEmail, DateTime userCreatedOn, byte[] salt, byte[] hashCode)
    {
        UserId = userId; // todo, sicuro? è settabile?
        UserGuid = userGuid;
        UserName = userName;
        UserSurname = userSurname;
        UserEmail = userEmail;
        UserCreatedOn = userCreatedOn;
        Salt = salt;
        HashCode = hashCode;
    }

    // todo: implement password change

}
