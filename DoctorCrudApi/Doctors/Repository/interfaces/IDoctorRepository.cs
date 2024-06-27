using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Dto;

namespace DoctorCrudApi.Doctors.Repository.interfaces
{
    public interface IDoctorRepository
    {
        Task<ListDoctorDto> GetAllAsync();
        Task<ListDoctorDto> GetByTypeAsync(string type);
        Task<DoctorDto> GetByIdAsync(int id);
        Task<DoctorDto> CreateDoctor(CreateDoctorRequest request);
        Task<DoctorDto> UpdateDoctor(int id, UpdateDoctorRequest request);
        Task<DoctorDto> DeleteDoctorById(int id);
        Task<ListDoctorDto> GetAllSortedByPatientsAscAsync();
        Task<ListDoctorDto> GetAllSortedByPatientsDescAsync();
        Task<DoctorDto> GetByNameAsync(string name);
        Task<ListDoctorDto> GetByNameStartingWithAsync(string prefix);
        Task<ListDoctorDto> GetByPatientIntervalAsync(int minPatients, int maxPatients);
        Task<ListDoctorDto> GetByTypeWithMinPatientsAsync(string type, int minPatients);
        Task<bool> DoctorExistsByIdAsync(int id);
        Task<bool> DoctorExistsByNameAsync(string name);
    } 
}
