using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Doctors.Repository.interfaces;
using DoctorCrudApi.Doctors.Service.interfaces;
using DoctorCrudApi.Dto;
using DoctorCrudApi.System.Constant;
using DoctorCrudApi.System.Exceptions;

namespace DoctorCrudApi.Doctors.Service
{
    public class DoctorCommandService : IDoctorCommandService
    {
        private IDoctorRepository _repository;

        public DoctorCommandService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Doctor> CreateDoctor(CreateDoctorRequest request)
        {
            Doctor doc = await _repository.GetByTypeAsync(request.Type);

            if (doc!=null)
            {
                throw new ItemAlreadyExists(Constants.DOCTOR_ALREADY_EXIST);
            }

            doc=await _repository.CreateDoctor(request);
            return doc;
        }

        public async Task<Doctor> DeleteDoctor(int id)
        {
            Doctor doc = await _repository.GetByIdAsync(id);

            if (doc==null)
            {
                throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
            }

            await _repository.DeleteDoctorById(id);
            return doc;
        }

        public async Task<Doctor> UpdateDoctor(int id, UpdateDoctorRequest request)
        {
            Doctor doc = await _repository.GetByIdAsync(id);

            if (doc==null)
            {
                throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
            }

            doc = await _repository.UpdateDoctor(id, request);
            return doc;
        }

    }
}
