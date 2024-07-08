using FluentResults;
using Priemka.Domain.Common;

namespace Priemka.Domain.ValueObjects
{
    public class Medication : ValueObject
    {
        private Medication(string name, string dosage)
        {
            Name = name;
            Dosage = dosage;
        }
        public string Name { get; private set; }
        public string Dosage { get; private set; }


        public static Result<Medication> Create(string name, string dosage)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Fail("Название не может быть пустым");
            if (string.IsNullOrWhiteSpace(dosage))
                return Result.Fail("Доза пустая");

            var medication = new Medication(name, dosage);
            return Result.Ok(medication);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Dosage;
        }
    }
}
    
