namespace Priemka.Domain.Common
{
    public class Permissions
    {
        public static class Doctors
        {
            public const string Read = "doctors.read";
            public const string Create = "doctors.create";
            public const string Update = "doctors.update";
            public const string Delete = "doctors.dalete";
        }
        public static class Patients
        {
            public const string Read = "patient.read";
            public const string Create = "patient.create";
            public const string Update = "patient.update";
            public const string Delete = "patient.dalete";
        }

    }
}
