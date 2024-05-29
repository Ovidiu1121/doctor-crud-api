namespace DoctorCrudApi.Dto
{
    public class CreateDoctorRequest
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Patients { get; set; }
    }
}
