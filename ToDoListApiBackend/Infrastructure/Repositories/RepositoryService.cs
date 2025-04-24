using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Data.SQLite;
using ToDoListApiBackend.Application.Interfaces;
using ToDoListApiBackend.Domain;

namespace ToDoListApiBackend.Infrastructure.Repositories;

public class RepositoryService : IRepositoryService
{
    private readonly ILogger<RepositoryService> _logger;

    public RepositoryService(ILogger<RepositoryService> logger)
    {
        _logger = logger;
    }

    public User? GetUser(UserProperties userProperty, object userPropertyObject)
    {
        string query = string.Empty;
        string parameterName = string.Empty;

        switch (userProperty){
            case UserProperties.UserId:
                query = "SELECT * FROM Users " +
                        "WHERE UserId = @userId  ";
                parameterName = "UserId";
                if(userPropertyObject.GetType() != typeof(int))
                    throw new Exception($"Passed value: <{userPropertyObject.GetType()}> is different from expected type: <{typeof(int)}>");
                break;
            case UserProperties.UserGuid:
                query = "SELECT * FROM Users " +
                        "WHERE UserGuid = @userGuid  ";
                parameterName = "UserGuid";
                if (userPropertyObject.GetType() != typeof(Guid))
                    throw new Exception($"Passed value: <{userPropertyObject.GetType()}> is different from expected type: <{typeof(Guid)}>");
                break;
            case UserProperties.UserName:
                query = "SELECT * FROM Users " +
                        "WHERE UserName = @userName  ";
                parameterName = "UserName";
                if (userPropertyObject.GetType() != typeof(string))
                    throw new Exception($"Passed value: <{userPropertyObject.GetType()}> is different from expected type: <{typeof(string)}>");
                break;
            case UserProperties.UserSurname:
                query = "SELECT * FROM Users " +
                        "WHERE UserSurname = @userSurname  ";
                parameterName = "UserSurname";
                if (userPropertyObject.GetType() != typeof(string))
                    throw new Exception($"Passed value: <{userPropertyObject.GetType()}> is different from expected type: <{typeof(string)}>");
                break;
            case UserProperties.UserEmail:
                query = "SELECT * FROM Users " +
                        "WHERE UserEmail = @userEmail  ";
                parameterName = "UserEmail";
                if (userPropertyObject.GetType() != typeof(string))
                    throw new Exception($"Passed value: <{userPropertyObject.GetType()}> is different from expected type: <{typeof(string)}>");
                break;
            default:
                _logger.LogInformation("No property found with name :<{userProperty}>", userProperty.ToString());
                break;
        }

        if (string.IsNullOrWhiteSpace(query))
        {
            _logger.LogInformation("No valid query for <{userProperty}>", userProperty.ToString());
            return null;
        }

        using var connection = new SQLiteConnection("Data Source=ToDoListApi.db");
        connection.Open();

        using var command = new SQLiteCommand(query, connection);
        command.Parameters.AddWithValue(parameterName, userPropertyObject);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new User(
                reader.GetInt32(reader.GetOrdinal("UserId")),
                reader.GetGuid(reader.GetOrdinal("UserGuid")),
                reader.GetString(reader.GetOrdinal("UserName")),
                reader.GetString(reader.GetOrdinal("UserSurname")),
                reader.GetString(reader.GetOrdinal("UserEmail")),
                reader.GetDateTime(reader.GetOrdinal("UserCreatedOn")),
                (byte[])reader["Salt"],
                (byte[])reader["HashCode"]
            );
        }

        return null;
    }

    public List<User> GetAllUsers()
    {
        return [];
    }

    public Activity GetActivity(ActivityProperties activityProperty, object activityPropertyObject)
    {
        string query = string.Empty;

        switch (activityProperty)
        {
            case ActivityProperties.ActivityId:
                query = "";
                break;
            case ActivityProperties.ActivityGuid:
                query = "";
                break;
            case ActivityProperties.ActivityTitle:
                query = "";
                break;
            case ActivityProperties.ActivityDescription:
                query = "";
                break;
            case ActivityProperties.ActivityCreatedOn:
                query = "";
                break;
            default:
                _logger.LogInformation("No property found with name :<{activityProperty}>", activityProperty.ToString());
                break;
        }

        if (string.IsNullOrWhiteSpace(query))
        {
            _logger.LogInformation("No valid query for <{activityProperty}>", activityProperty.ToString());
            return null;
        }

        // todo eseguire la query
        return null;
    }

    public List<Activity> GetAllActivities()
    {
        return [];
    }

    public List<Activity> GetUserActivities(UserProperties userProperty, object userPropertyObject)
    {
        int userId = GetUser(userProperty, userPropertyObject).UserId;



        return [];
    }
}
