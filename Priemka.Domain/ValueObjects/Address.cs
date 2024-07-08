using FluentResults;
using Priemka.Domain.Common;

namespace Priemka.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; init; }
        public string City { get; init; }


        private Address(string street, string city)
        {
            Street = street;
            City = city;
        }

        public static Result<Address> Create(string street, string city)
        {
            if (string.IsNullOrWhiteSpace(street))
                return Result.Fail("Street cannot be empty");
            if (string.IsNullOrWhiteSpace(city))
                return Result.Fail("City cannot be empty");


            var address = new Address(street, city);
            return Result.Ok(address);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
        }
    }

}
