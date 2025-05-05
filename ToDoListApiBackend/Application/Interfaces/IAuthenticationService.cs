using TodoListApi.Dtos;
using ToDoListApiBackend.Domain;

namespace ToDoListApiBackend.Application.Interfaces;

public interface IAuthenticationService
{
    public User AuthenticateUser(UserDto userDto);
}
