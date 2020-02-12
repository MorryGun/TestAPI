using APITest.Managers;

namespace APITest.Constants
{
    public class ConfigConstants : ConfigManager
    {
        public const string BaseUrl = "BaseUrl";

        protected static string FirstPartUrl => Config[BaseUrl];

        public static string GetAllEmployeesURL = string.Concat(FirstPartUrl, "/employees");

        public static string GetEmployeeURL = string.Concat(FirstPartUrl, "/employee/");

        public static string CreateEmployeeURL = string.Concat(FirstPartUrl, "/create");
    }
}
