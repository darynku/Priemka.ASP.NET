using FluentResults;
using Priemka.Domain.Common;
using System.Text.Json.Serialization;

namespace Priemka.Domain.ValueObjects
{
    public class Achivments : ValueObject
    {
        private Achivments() { }

        public Achivments(string achivmentName, DateTime? achivmentDate)
        {
            AchivmentName = achivmentName;
            AchivmentDate = achivmentDate;
        }

        public string AchivmentName { get; } = null!;
        public DateTime? AchivmentDate { get; }
        
        public static Result<Achivments> Create(string achivmentName, DateTime? achivmentDate)
        {
            if (string.IsNullOrEmpty(achivmentName))
                return Result.Fail("Название не может быть пустым");
            if (achivmentName == "-")
                return Result.Ok(new Achivments(achivmentName, null));
            return Result.Ok(new Achivments(achivmentName, achivmentDate));
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AchivmentName;
        }
    }
}
