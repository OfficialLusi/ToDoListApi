namespace TodoListApi.Dtos;

public class UserDto
{
    public int UserId { get; set; }
    public Guid UserGuid { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserSurname { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string UserPassword { get; set; } = string.Empty;
    public DateTime UserCreatedOn { get; set; }

    // from frontend to backend
    public UserDto(Guid userGuid, string userName, string userSurname, string userEmail, string userPassword, DateTime userCreatedOn)
    {
        UserGuid = userGuid;
        UserName = userName;
        UserSurname = userSurname;
        UserEmail = userEmail;
        UserPassword = userPassword;
        UserCreatedOn = userCreatedOn;
    }

    // from backend to frontend
    public UserDto(int userId, Guid userGuid, string userName, string userSurname, string userEmail, DateTime userCreatedOn)
    {
        UserId = userId;
        UserGuid = userGuid;
        UserName = userName;
        UserSurname = userSurname;
        UserEmail = userEmail;
        UserCreatedOn = userCreatedOn;
    }
}
