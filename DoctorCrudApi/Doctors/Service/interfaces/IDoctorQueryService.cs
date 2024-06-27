using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Dto;

namespace DoctorCrudApi.Doctors.Service.interfaces
{
    public interface IDoctorQueryService
    {
        Task<ListDoctorDto> GetAll();
        Task<DoctorDto> GetById(int id);
        Task<ListDoctorDto> GetByType(string type);
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
