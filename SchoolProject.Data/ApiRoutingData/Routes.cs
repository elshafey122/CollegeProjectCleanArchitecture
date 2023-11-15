namespace SchoolProject.Data.ApiRoutingData
{
    public static class Routes
    {
        public const string root = "Api";
        public const string version = "v1";
        public const string Rule = root + "/" + version + "/";

        public static class StudentRouting
        {
            public const string Prefix = Rule + "Student";

            public const string List = Prefix + "/List";
            public const string GetById = Prefix + "/{id}";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete/{id}";
            public const string Paginated = Prefix + "/PaginatedList";
        }
        public static class DepartementRouting
        {
            public const string Prefix = Rule + "Departement";

            public const string List = Prefix + "/PaginatedList";
            public const string GetById = Prefix + "/Paginatedid";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete/{id}";
        }
        public static class InstructorRouting
        {
            public const string Prefix = Rule + "Instructor";

            public const string List = Prefix + "/PaginatedList";
            public const string GetById = Prefix + "/{id}";
            public const string Edit = Prefix + "/Edit";
            public const string Create = Prefix + "/Create";
            public const string Delete = Prefix + "/Delete/{id}";
        }

        public static class ApplicationUserRouting
        {
            public const string Prefix = Rule + "User";

            public const string Register = Prefix + "/Register";
            public const string GetUsers = Prefix + "/GetUsers";
            public const string GetbyId = Prefix + "/GetbyId/{id}";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete/{id}";
            public const string ChangeUserPassword = Prefix + "/ChangeUserPassword";
        }
        public static class Authentication
        {
            public const string Prefix = Rule + "Authentication";

            public const string SignIn = Prefix + "/SignIn";
            public const string GenerateRefreshToken = Prefix + "/GenerateRefreshToken";
            public const string ValidateToken = Prefix + "/ValidateToken";



        }
    }
}
