using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Doctors.Repository.interfaces;
using DoctorCrudApi.Doctors.Service.interfaces;
using DoctorCrudApi.System.Constant;
using DoctorCrudApi.System.Exceptions;

namespace DoctorCrudApi.Doctors.Service
{
    public class DoctorQueryService : IDoctorQueryService
    {
        private IDoctorRepository _repository;

        public DoctorQueryService(IDoctorRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Doctor>> GetAll()
        {
            IEnumerable<Doctor> animals = await _repository.GetAllAsync();

            if (animals.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }

            return animals;
        }

        public async Task<Doctor> GetById(int id)
        {
            Doctor doc = await _repository.GetByIdAsync(id);

            if (doc == null)
            {
                throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
            }

            return doc;
        }

        public async Task<Doctor> GetByType(string type)
        {
            Doctor doc = await _repository.GetByTypeAsync(type);

            if (doc == null)
            {
                throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
            }

            return doc;
        }
    }
}
