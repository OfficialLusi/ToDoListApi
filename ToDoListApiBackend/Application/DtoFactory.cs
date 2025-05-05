using System;
using TodoListApi.Dtos;
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
        return new UserDto(
            user.UserId,
            user.UserGuid,
            user.UserName,
            user.UserSurname,
            user.UserEmail,
            user.UserCreatedOn
        );
    }
    public static User GetUser(UserDto userDto, byte[] salt, byte[] hashCode)
    {
        return new User(
            userDto.UserGuid,
            userDto.UserName,
            userDto.UserSurname,
            userDto.UserEmail,
            userDto.UserCreatedOn,
            salt,
            hashCode
        );
    }

    #endregion

    #region activity


    #endregion

}
