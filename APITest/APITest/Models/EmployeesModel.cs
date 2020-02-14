namespace APITest.Models
{

    public class EmployeesModel
    {
        public string status { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public uint id { get; set; }
        public string employee_name { get; set; }
        public uint employee_salary { get; set; }
        public byte employee_age { get; set; }
        public object profile_image { get; set; }
    }

}
