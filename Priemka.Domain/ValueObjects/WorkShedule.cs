using FluentResults;
using Priemka.Domain.Common;
using System.Text.Json.Serialization;

namespace Priemka.Domain.ValueObjects;

/// <summary>
/// График работы
/// </summary>
public class WorkShedule : ValueObject
{
    [JsonConstructor]
    public WorkShedule(string dayOfWeek, DateTime shiftStart, DateTime shiftEnd)
    {
        DayOfWeek = dayOfWeek;
        ShiftStart = shiftStart;
        ShiftEnd = shiftEnd;
    }
    private WorkShedule() { }
    public string DayOfWeek { get; } = string.Empty;
    //Начало смены
    public DateTime ShiftStart { get; }
    public DateTime ShiftEnd { get; }
    
    public static Result<WorkShedule> Create(string dayOfWeek, DateTime shiftStart, DateTime shiftEnd)
    {
        if (shiftStart >= shiftEnd)
            return Result.Fail("Дата начала смены, не может быть больше или равна дате конца смены");

        var workShedule = new WorkShedule(dayOfWeek, shiftStart, shiftEnd);
        return Result.Ok(workShedule);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DayOfWeek;
        yield return ShiftStart;
        yield return ShiftEnd;
    }
}