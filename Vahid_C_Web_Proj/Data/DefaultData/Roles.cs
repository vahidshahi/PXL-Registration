namespace Vahid_C_Web_Proj.Data.DefaultData
{
    public class Roles
    {
        public const string Admin = "Admin";
        public const string Lector = "Lector";
        public const string Student = "Student";
        public const string Admin_Lector = Admin + "," + Lector;
        public const string Admin_Student = Admin + "," + Student;
        public const string Lector_Student = Lector + "," + Student;
    }
}
