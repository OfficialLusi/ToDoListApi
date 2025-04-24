using ToDoListApiBackend.Application.DTOs;
using ToDoListApiBackend.Domain;

namespace ToDoListApiBackend.Application;

/// <summary>
/// Class to convert domain classes into dto for the transfer
/// </summary>
public static class DtoFactory
{
    #region user
 
    public static UserDto GetUserDto(User user)
    {
        // todo: what do i need in the frontend/other services? (do i need hash and salt and do the auth in the frontend?)
        return new UserDto(
            user.GetUserGuid(),
            user.GetUserName(),
            user.GetUserSurname(),
            user.GetUserEmail(),
            user.GetUserCreationDate()
        );  
    }
    
    #endregion

    #region activity
    
    
    #endregion

}
