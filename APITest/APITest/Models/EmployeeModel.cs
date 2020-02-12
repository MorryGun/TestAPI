namespace APITest.Models
{

    public class Rootobject
    {
        public string status { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public string employee_name { get; set; }
        public int employee_salary { get; set; }
        public int employee_age { get; set; }
        public object profile_image { get; set; }
    }

}
