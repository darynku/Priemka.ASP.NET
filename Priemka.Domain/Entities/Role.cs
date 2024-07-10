using Priemka.Domain.Common;

namespace Priemka.Domain.Entities
{
    public class Role : ValueObject
    {
        public readonly static Role Admin = new(
            "ADMIN",
            [    Common.Permissions.Doctors.Read,
                 Common.Permissions.Doctors.Update,
                 Common.Permissions.Doctors.Delete,
                 Common.Permissions.Doctors.Create,

                 Common.Permissions.Patient.Delete,
                 Common.Permissions.Patient.Update,


            ]);
        public readonly static Role Doctor = new(
            "DOCTOR",
            [
                Common.Permissions.Doctors.Read,

                Common.Permissions.Patient.Create,
                Common.Permissions.Patient.Delete,
                Common.Permissions.Patient.Update,
                Common.Permissions.Patient.Read,
                ]);
        public readonly static Role ApplicationUser = new(
            "PATIENT",
            [
                Common.Permissions.Patient.Read,
                Common.Permissions.Doctors.Read
                ]);

        private Role(string name, string[] permissions)
        {
            Name = name;
            Permissions = permissions;
        }
        public string Name { get; }
        public string[] Permissions { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
