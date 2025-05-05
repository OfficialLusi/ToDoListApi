using TodoListApi.Dtos;

namespace ToDoListApiBackend.Application.Interfaces;

public interface IApplicationService
{
    public void AddUser(UserDto userDto);
    public void UpdateUser(UserDto userDto);
    public void UpdateUserPassword(UserDto userDto);
    public void DeleteUser(UserDto userDto);
    public UserDto GetUserByGuid(Guid guid);
    public List<UserDto> GetAllUsers();

}
