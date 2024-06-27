using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Dto;

namespace DoctorCrudApi.Doctors.Service.interfaces
{
    public interface IDoctorCommandService
    {
        Task<DoctorDto> CreateDoctor(CreateDoctorRequest request);
        Task<DoctorDto> UpdateDoctor(int id, UpdateDoctorRequest request);
        Task<DoctorDto> DeleteDoctor(int id);

    }
}
