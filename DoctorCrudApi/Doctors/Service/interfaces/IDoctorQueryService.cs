using DoctorCrudApi.Doctors.Model;

namespace DoctorCrudApi.Doctors.Service.interfaces
{
    public interface IDoctorQueryService
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor> GetById(int id);
        Task<Doctor> GetByType(string type);
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
