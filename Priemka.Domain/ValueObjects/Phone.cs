using FluentResults;
using Priemka.Domain.Common;
using System.Text.RegularExpressions;

namespace Priemka.Domain.ValueObjects
{
    public class Phone : ValueObject
    {
        public const string PHONE_REGEX = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";

        public string Number { get; }

        private Phone(string number)
        {
            Number = number;
        }

        public static Result<Phone> Create(string input)
        {
            if (string.IsNullOrEmpty(input))
                return Result.Fail("Номер не может быть пустым");
            if (Regex.IsMatch(input, PHONE_REGEX) == false)
                return Result.Fail("Неверный ввод данных (телефон)");

            return Result.Ok(new Phone(input));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
