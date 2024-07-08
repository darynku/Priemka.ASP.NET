using FluentResults;
using Priemka.Domain.Common;
using System.Text.RegularExpressions;

namespace Priemka.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; }

        public Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string input)
        {
            input = input.Trim();

            if (input.Length is < 1)
                return Result.Fail("Слишком короткий");

            if (Regex.IsMatch(input, "^(.+)@(.+)$") == false)
                return Result.Fail("Неправильный ввод данных (почта)");

            return Result.Ok(new Email(input));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
