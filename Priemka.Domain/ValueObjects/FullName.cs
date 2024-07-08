using FluentResults;
using Priemka.Domain.Common;

namespace Priemka.Domain.ValueObjects
{
    public class FullName : ValueObject
    {
        private FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public static Result<FullName>Create(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
                return Result.Fail("Имя не может быть пустым");
            if (string.IsNullOrEmpty(lastName))
                return Result.Fail("Фамилия не может быть пустой");

            return Result.Ok(new FullName(firstName, lastName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
