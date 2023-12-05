namespace SchoolProject.Data.ApiRoutingData
{
    public static class Routes
    {
        public const string root = "Api";
        public const string version = "V1";
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
            public const string ConfirmEmail = "/Api/Authentication/ConfirmEmail";
            public const string SendResetPassword = Prefix + "/SendResetPassword";
            public const string ConfirmResetpassword = Prefix + "/ConfirmResetpassword";
            public const string ResetPassword = Prefix + "/ResetPassword";


        }
        public static class Authorization
        {
            public const string Prefix = Rule + "Authorization";
            public const string role = Prefix + "/Role";
            public const string claim = Prefix + "/claim";


            public const string Create = role + "/Create";
            public const string Edit = role + "/Edit";
            public const string Delete = role + "/Delete/{id}";
            public const string List = role + "/List";
            public const string GetById = role + "/GetById/{id}";

            public const string ManageUserRoles = role + "/ManageUserRoles/{id}";
            public const string UpdateUserRoles = role + "/UpdateUserRoles";

            public const string ManageUserClaims = claim + "/ManageUserClaims/{id}";
            public const string UpdateUserClaims = claim + "/UpdateUserClaims";

        }

        public static class Email
        {
            public const string Prefix = Rule + "Email";
            public const string SendEmail = Prefix + "/SendEmail";

        }
    }
}
