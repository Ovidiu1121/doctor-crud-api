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

        public async Task<bool> DoctorExistsByIdAsync(int id)
        {
            bool result = await _repository.DoctorExistsByIdAsync(id);

            if (result.Equals(false)){
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }

            return result;
        }

        public async Task<bool> DoctorExistsByNameAsync(string name)
        {
            bool result = await _repository.DoctorExistsByNameAsync(name);

            if (result.Equals(false))
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }

            return result;
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            IEnumerable<Doctor> doctors = await _repository.GetAllAsync();

            if (doctors.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }

            return doctors;
        }

        public async Task<IEnumerable<Doctor>> GetAllSortedByPatientsAscAsync()
        {
            IEnumerable<Doctor> doctors = await _repository.GetAllSortedByPatientsAscAsync();

            if (doctors.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }

            return doctors;
        }

        public async Task<IEnumerable<Doctor>> GetAllSortedByPatientsDescAsync()
        {
            IEnumerable<Doctor> doctors = await _repository.GetAllSortedByPatientsDescAsync();

            if (doctors.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }

            return doctors;
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

        public async Task<Doctor> GetByNameAsync(string name)
        {
            Doctor doc = await _repository.GetByNameAsync(name);

            if (doc == null)
            {
                throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
            }

            return doc;
        }

        public async Task<IEnumerable<Doctor>> GetByNameStartingWithAsync(string prefix)
        {
            IEnumerable<Doctor> doctors = await _repository.GetByNameStartingWithAsync(prefix);

            if (doctors.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }

            return doctors;
        }

        public async Task<IEnumerable<Doctor>> GetByPatientIntervalAsync(int minPatients, int maxPatients)
        {
            IEnumerable<Doctor> doctors = await _repository.GetByPatientIntervalAsync(minPatients, maxPatients);

            if (doctors.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }

            return doctors;

        }

        public async Task<Doctor> GetByType(string type)
        {
            Doctor doctors = await _repository.GetByTypeAsync(type);

            if (doctors == null)
            {
                throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
            }

            return doctors;
        }

        public async Task<IEnumerable<Doctor>> GetByTypeWithMinPatientsAsync(string type, int minPatients)
        {
            IEnumerable<Doctor> doctors = await _repository.GetByTypeWithMinPatientsAsync(type,minPatients);

            if (doctors.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }

            return doctors;
        }
    }
}
