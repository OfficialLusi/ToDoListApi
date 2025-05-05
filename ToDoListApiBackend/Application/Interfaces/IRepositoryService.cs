using System.Linq.Expressions;
using ToDoListApiBackend.Domain;

namespace ToDoListApiBackend.Application.Interfaces;

public interface IRepositoryService
{
    #region Users
    // add
    public void AddUser(User user);
    public void UpdateUser(User user);
    public void DeleteUser(User user);

    // get
    public User? GetUser(UserProperties userProperty, object userPropertyObject);
    public List<User>? GetAllUsers();
    #endregion

    #region Activities
    // add
    public void AddActivity(Activity activity);
    public void UpdateActivity(Activity activity);
    public void DeleteActivity(Activity activity);

    // get
    public Activity? GetActivity(ActivityProperties activityProperty, object activityPropertyObject);
    public List<Activity>? GetAllActivities();
    #endregion

    public List<Activity>? GetUserActivities(UserProperties userProperty, object userPropertyObject);
}
