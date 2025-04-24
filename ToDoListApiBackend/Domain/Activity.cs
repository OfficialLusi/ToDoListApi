namespace ToDoListApiBackend.Domain;

public class Activity
{
    public int ActivityId { get; set; }
    public int UserId { get; set; }
    public Guid ActivityGuid { get; set; }
    public string ActivityTitle { get; set; } = string.Empty;
    public string ActivityDescription { get; set; } = string.Empty;
    public DateTime ActivityCreatedOn { get; set; }
    public DateTime? ActivityModifiedOn { get; set; }

    public Activity(Guid activityGuid, string activityTitle, string activityDescription, DateTime activityCreatedOn, DateTime? activityModifiedOn = null)
    {
        ActivityGuid = activityGuid;
        ActivityTitle = activityTitle;
        ActivityDescription = activityDescription;
        ActivityCreatedOn = activityCreatedOn;
        if (activityModifiedOn != null)
            ActivityModifiedOn = activityModifiedOn;
    }

}
