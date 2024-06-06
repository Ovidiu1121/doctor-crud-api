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
        Task<IEnumerable<Doctor>> GetAllSortedByPatientsAscAsync();
        Task<IEnumerable<Doctor>> GetAllSortedByPatientsDescAsync();
        Task<Doctor> GetByNameAsync(string name);
        Task<IEnumerable<Doctor>> GetByNameStartingWithAsync(string prefix);
        Task<IEnumerable<Doctor>> GetByPatientIntervalAsync(int minPatients, int maxPatients);
        Task<IEnumerable<Doctor>> GetByTypeWithMinPatientsAsync(string type, int minPatients);
        Task<bool> DoctorExistsByIdAsync(int id);
        Task<bool> DoctorExistsByNameAsync(string name);
    } 
}
