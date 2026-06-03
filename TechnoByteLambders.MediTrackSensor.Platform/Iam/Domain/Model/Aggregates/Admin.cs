namespace TechnoByteLambders.MediTrackSensor.Platform.Iam.Domain.Model.Aggregates;

public partial class Admin(string entityName, string entityCode, string schedule, int userId)
{
    public Admin() : this(string.Empty, string.Empty, string.Empty, 0)
    {
    }

    public int Id { get; }
    public string EntityName { get; private set; } = entityName;
    public string EntityCode { get; private set; } = entityCode;
    public string Schedule { get; private set; } = schedule;
    public int UserId { get; private set; } = userId;

    public Admin UpdateEntityName(string entityName)
    {
        EntityName = entityName;
        return this;
    }

    public Admin UpdateSchedule(string schedule)
    {
        Schedule = schedule;
        return this;
    }
}
