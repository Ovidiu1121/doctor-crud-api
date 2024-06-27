namespace DoctorCrudApi.System.Constant
{
    public class Constants
    {
        public const string NO_DOCTORS_EXIST = "There are no doctors.";
        public const string DOCTOR_ALREADY_EXIST = "This doctor already exist.";
        public const string DOCTOR_DOES_NOT_EXIST = "This doctor does not exist";

        public static IEnumerable<object> ItemDoesNotExist { get; set; }
    }
}
