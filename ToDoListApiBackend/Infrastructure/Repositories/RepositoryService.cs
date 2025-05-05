using Microsoft.Data.Sqlite;
using ToDoListApiBackend.Application.Interfaces;
using ToDoListApiBackend.Domain;

namespace ToDoListApiBackend.Infrastructure.Repositories;

public class RepositoryService : IRepositoryService
{
    private readonly ILogger<RepositoryService> _logger;
    private readonly string _dbPath;
    private readonly string _creationFilePath;
    private readonly string _connectionString;

    public RepositoryService(ILogger<RepositoryService> logger, string dbPath, string creationFilePath, string connectionString)
    {
        _logger = logger;
        _dbPath = dbPath;
        _creationFilePath = creationFilePath;
        _connectionString = connectionString;

        if (!File.Exists(dbPath))
            InitializeRepository();
    }


    #region Users
    public void AddUser(User user)
    {
        try
        {
            string query = "INSERT INTO Users (UserGuid, UserName, UserSurname, UserEmail, UserCreatedOn, Salt, HashCode) " +
                           "VALUES (@userGuid, @userName, @userSurname, @userEmail, @userCreatedOn, @salt, @hashCode)";

            using var connection = GetConnection();
            connection.Open();

            using var command = new SqliteCommand(query, connection);
            IEnumerable<SqliteParameter> parameters =
            [
                new SqliteParameter("UserGuid", user.UserGuid),
                new SqliteParameter("UserName", user.UserName),
                new SqliteParameter("UserSurname", user.UserSurname),
                new SqliteParameter("UserEmail", user.UserEmail),
                new SqliteParameter("UserCreatedOn", user.UserCreatedOn),
                new SqliteParameter("Salt", user.Salt),
                new SqliteParameter("Hashcode", user.HashCode),
            ];

            command.Parameters.AddRange(parameters);

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RepositoryService - {Method} failed. User not added. UserGuid <{userGuid}>", nameof(GetUser), user.UserGuid);
        }
    }

    public void UpdateUser(User user)
    {
        try
        {
            string query = "UPDATE Users                    " +
                           "SET UserGuid = @userGuid,       " +
                           "UserName = @userName,           " +
                           "UserSurname = @userSurname,     " +
                           "UserEmail = @userEmail,         " +
                           "UserCreatedOn = @userCreatedOn, " +
                           "Salt = @salt,                   " +
                           "HashCode = @hashCode            " +
                           "WHERE UserId = @userId          ";

            using var connection = GetConnection();
            connection.Open();

            using var command = new SqliteCommand(query, connection);
            IEnumerable<SqliteParameter> parameters =
            [
                    new SqliteParameter("@userId", user.UserId),
                    new SqliteParameter("@userGuid", user.UserGuid),
                    new SqliteParameter("@userName", user.UserName),
                    new SqliteParameter("@userSurname", user.UserSurname),
                    new SqliteParameter("@userEmail", user.UserEmail),
                    new SqliteParameter("@userCreatedOn", user.UserCreatedOn),
                    new SqliteParameter("@salt", user.Salt),
                    new SqliteParameter("@hashcode", user.HashCode),
            ];

            command.Parameters.AddRange(parameters);

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RepositoryService - {Method} failed. User not updated. UserGuid <{userGuid}>", nameof(GetUser), user.UserGuid);
        }
    }

    public void DeleteUser(User user)
    {

    }


    public User? GetUser(UserProperties userProperty, object userPropertyObject)
    {
        try
        {
            string query = string.Empty;
            string parameterName = string.Empty;

            switch (userProperty)
            {
                case UserProperties.UserId:
                    query = "SELECT * FROM Users " +
                            "WHERE UserId = @userId";
                    parameterName = "UserId";
                    if (userPropertyObject.GetType() != typeof(int))
                        throw new Exception($"Passed value: <{userPropertyObject.GetType()}> is different from expected type: <{typeof(int)}>");
                    break;
                case UserProperties.UserGuid:
                    query = "SELECT * FROM Users " +
                            "WHERE UserGuid = @userGuid";
                    parameterName = "UserGuid";
                    if (userPropertyObject.GetType() != typeof(Guid))
                        throw new Exception($"Passed value: <{userPropertyObject.GetType()}> is different from expected type: <{typeof(Guid)}>");
                    break;
                case UserProperties.UserName:
                    query = "SELECT * FROM Users " +
                            "WHERE UserName = @userName";
                    parameterName = "UserName";
                    if (userPropertyObject.GetType() != typeof(string))
                        throw new Exception($"Passed value: <{userPropertyObject.GetType()}> is different from expected type: <{typeof(string)}>");
                    break;
                case UserProperties.UserSurname:
                    query = "SELECT * FROM Users " +
                            "WHERE UserSurname = @userSurname";
                    parameterName = "UserSurname";
                    if (userPropertyObject.GetType() != typeof(string))
                        throw new Exception($"Passed value: <{userPropertyObject.GetType()}> is different from expected type: <{typeof(string)}>");
                    break;
                case UserProperties.UserEmail:
                    query = "SELECT * FROM Users " +
                            "WHERE UserEmail = @userEmail";
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

            using var connection = GetConnection();
            connection.Open();

            using var command = new SqliteCommand(query, connection);
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
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RepositoryService - {Method} failed. Input: {Input}", nameof(GetUser), userProperty);
        }
        return null;
    }

    public List<User>? GetAllUsers()
    {
        try
        {
            List<User> users = new List<User>();

            string query = "SELECT * FROM Users";

            using var connection = GetConnection();
            connection.Open();

            using var command = new SqliteCommand(query, connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                User user = new User(
                    reader.GetInt32(reader.GetOrdinal("UserId")),
                    reader.GetGuid(reader.GetOrdinal("UserGuid")),
                    reader.GetString(reader.GetOrdinal("UserName")),
                    reader.GetString(reader.GetOrdinal("UserSurname")),
                    reader.GetString(reader.GetOrdinal("UserEmail")),
                    reader.GetDateTime(reader.GetOrdinal("UserCreatedOn")),
                    (byte[])reader["Salt"],
                    (byte[])reader["HashCode"]
                    );

                users.Add(user);
            }
            return users;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RepositoryService - {Method} failed.", nameof(GetUser));
        }
        return null;
    }

    #endregion


    #region Activities

    public void AddActivity(Activity activity)
    {

    }

    public void UpdateActivity(Activity activity)
    {

    }

    public void DeleteActivity(Activity activity)
    {

    }

    public Activity? GetActivity(ActivityProperties activityProperty, object activityPropertyObject)
    {
        try
        {
            string query = string.Empty;
            string parameterName = string.Empty;

            switch (activityProperty)
            {
                case ActivityProperties.ActivityId:
                    query = "SELECT * FROM Activities " +
                            "WHERE ActivityId = @activityId";
                    parameterName = "ActivityId";
                    if (activityPropertyObject.GetType() != typeof(int))
                        throw new Exception($"Passed value: <{activityPropertyObject.GetType()}> is different from expected type: <{typeof(int)}>");
                    break;
                case ActivityProperties.ActivityGuid:
                    query = "SELECT * FROM Activities " +
                            "WHERE ActivityGuid = @activityGuid";
                    parameterName = "ActivityGuid";
                    if (activityPropertyObject.GetType() != typeof(Guid))
                        throw new Exception($"Passed value: <{activityPropertyObject.GetType()}> is different from expected type: <{typeof(Guid)}>");
                    break;
                case ActivityProperties.ActivityTitle:
                    query = "SELECT * FROM Activities " +
                            "WHERE ActivityTitle = @activityTitle";
                    parameterName = "ActivityTitle";
                    if (activityPropertyObject.GetType() != typeof(string))
                        throw new Exception($"Passed value: <{activityPropertyObject.GetType()}> is different from expected type: <{typeof(string)}>");
                    break;
                case ActivityProperties.ActivityDescription:
                    query = "SELECT * FROM Activities " +
                            "WHERE ActivityDescription = @activityDescription";
                    parameterName = "ActivityDescription";
                    if (activityPropertyObject.GetType() != typeof(string))
                        throw new Exception($"Passed value: <{activityPropertyObject.GetType()}> is different from expected type: <{typeof(string)}>");
                    break;
                case ActivityProperties.ActivityCreatedOn:
                    query = "SELECT * FROM Activities " +
                            "WHERE ActivityCreatedOn = @activityCreatedOn";
                    parameterName = "ActivityCreatedOn";
                    if (activityPropertyObject.GetType() != typeof(DateTime))
                        throw new Exception($"Passed value: <{activityPropertyObject.GetType()}> is different from expected type: <{typeof(DateTime)}>");
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

            using var connection = GetConnection();
            connection.Open();

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue(parameterName, activityPropertyObject);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Activity(
                    reader.GetInt32(reader.GetOrdinal("ActivityId")),
                    reader.GetGuid(reader.GetOrdinal("ActivityGuid")),
                    reader.GetString(reader.GetOrdinal("ActivityTitle")),
                    reader.GetString(reader.GetOrdinal("ActivityDescription")),
                    reader.GetDateTime(reader.GetOrdinal("ActivityCreatedOn")),
                    reader.GetDateTime(reader.GetOrdinal("ActivityModifiedOn"))
                );
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RepositoryService - {Method} failed. Input: {Input}", nameof(GetUser), activityProperty);

        }
        return null;
    }

    public List<Activity>? GetAllActivities()
    {
        try
        {
            List<Activity> activities = new List<Activity>();

            string query = "SELECT * FROM Activities";

            using var connection = GetConnection();
            connection.Open();

            using var command = new SqliteCommand(query, connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Activity activity = new Activity(
                    reader.GetInt32(reader.GetOrdinal("ActivityId")),
                    reader.GetGuid(reader.GetOrdinal("ActivityGuid")),
                    reader.GetString(reader.GetOrdinal("ActivityTitle")),
                    reader.GetString(reader.GetOrdinal("ActivityDescription")),
                    reader.GetDateTime(reader.GetOrdinal("ActivityCreatedOn")),
                    reader.GetDateTime(reader.GetOrdinal("ActivityModifiedOn"))
                    );

                activities.Add(activity);
            }
            return activities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RepositoryService - {Method} failed.", nameof(GetUser));
        }
        return null;
    }

    #endregion

    public List<Activity>? GetUserActivities(UserProperties userProperty, object userPropertyObject)
    {
        try
        {
            User user = GetUser(userProperty, userPropertyObject);

            if (user == null)
            {
                _logger.LogError("RepositoryService - GetUserActivities user not found for {userProperty} = {value}", userProperty, userPropertyObject);
                return null;
            }

            int userId = user.UserId;

            List<Activity> activities = new List<Activity>();

            string query = "SELECT * FROM Activities " +
                           "WHERE UserId = @userId";

            using var connection = GetConnection();
            connection.Open();

            using var command = new SqliteCommand(query, connection);

            command.Parameters.AddWithValue("@userId", userId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Activity activity = new Activity(
                    reader.GetInt32(reader.GetOrdinal("ActivityId")),
                    reader.GetGuid(reader.GetOrdinal("ActivityGuid")),
                    reader.GetString(reader.GetOrdinal("ActivityTitle")),
                    reader.GetString(reader.GetOrdinal("ActivityDescription")),
                    reader.GetDateTime(reader.GetOrdinal("ActivityCreatedOn")),
                    reader.GetDateTime(reader.GetOrdinal("ActivityModifiedOn"))
                    );

                activities.Add(activity);
            }

            return activities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RepositoryService - {Method} failed. Input: {Input}", nameof(GetUser), userProperty);
        }
        return null;
    }

    #region Private Helpers
    private SqliteConnection GetConnection()
    {
        return new SqliteConnection(_connectionString);
    }
    #endregion

    #region Initialize Repository
    private void InitializeRepository()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_dbPath)!);

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string sqlScript = File.ReadAllText(_creationFilePath);

        using var command = connection.CreateCommand();
        foreach (string statement in sqlScript.Split(';'))
        {
            var trimmed = statement.Trim();
            if (!string.IsNullOrWhiteSpace(trimmed))
            {
                command.CommandText = trimmed;
                command.ExecuteNonQuery();
            }
        }
    }
    #endregion
}
