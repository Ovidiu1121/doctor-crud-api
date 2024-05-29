using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Dto;

namespace DoctorCrudApi.Doctors.Service.interfaces
{
    public interface IDoctorCommandService
    {
        Task<Doctor> CreateDoctor(CreateDoctorRequest request);
        Task<Doctor> UpdateDoctor(int id, UpdateDoctorRequest request);
        Task<Doctor> DeleteDoctor(int id);

    }
}
