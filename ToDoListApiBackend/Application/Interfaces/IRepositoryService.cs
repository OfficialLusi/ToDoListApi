using System.Linq.Expressions;
using ToDoListApiBackend.Domain;

namespace ToDoListApiBackend.Application.Interfaces;

public interface IRepositoryService
{
    public User? GetUser(UserProperties userProperty, object userPropertyObject);
    public List<User> GetAllUsers();


    public Activity GetActivity(ActivityProperties activityProperty, object activityPropertyObject);
    public List<Activity> GetAllActivities();
    public List<Activity> GetUserActivities(UserProperties userProperty, object userPropertyObject);

}
