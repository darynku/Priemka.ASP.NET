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

                 Common.Permissions.Patients.Delete,
                 Common.Permissions.Patients.Update,


            ]);
        public readonly static Role Doctor = new(
            "DOCTOR",
            [
                Common.Permissions.Doctors.Read,

                Common.Permissions.Patients.Create,
                Common.Permissions.Patients.Delete,
                Common.Permissions.Patients.Update,
                Common.Permissions.Patients.Read,
                ]);
        public readonly static Role ApplicationUser = new(
            "APPLICATION_USER",
            [
                Common.Permissions.Patients.Read,
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
