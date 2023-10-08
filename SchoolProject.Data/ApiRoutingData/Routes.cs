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
            public const string Paginated = Prefix + "/Paginated";
        }
        public static class DepartementRouting
        {
            public const string Prefix = Rule + "Departement";
            public const string GetById = Prefix + "/id";
        }
    }
}
