using DoctorCrudApi.Doctors.Model;

namespace DoctorCrudApi.Doctors.Service.interfaces
{
    public interface IDoctorQueryService
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor> GetById(int id);
        Task<Doctor> GetByType(string type);
    }
}
