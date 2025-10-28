namespace Outbox.Common;

public class EmployeeEvent
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
}
