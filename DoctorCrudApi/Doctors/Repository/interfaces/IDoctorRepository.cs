using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Dto;

namespace DoctorCrudApi.Doctors.Repository.interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor> GetByTypeAsync(string type);
        Task<Doctor> GetByIdAsync(int id);
        Task<Doctor> CreateDoctor(CreateDoctorRequest request);
        Task<Doctor> UpdateDoctor(int id, UpdateDoctorRequest request);
        Task<Doctor> DeleteDoctorById(int id);
    }
}
