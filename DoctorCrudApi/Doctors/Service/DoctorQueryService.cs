using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Doctors.Repository.interfaces;
using DoctorCrudApi.Doctors.Service.interfaces;
using DoctorCrudApi.Dto;
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

        public async Task<ListDoctorDto> GetAll()
        {
            ListDoctorDto doctors = await _repository.GetAllAsync();

            if (doctors.doctorList.Count==0) 
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }
            
            return doctors;
        }

        public async Task<ListDoctorDto> GetAllSortedByPatientsAscAsync()
        {
            ListDoctorDto doctors = await _repository.GetAllSortedByPatientsAscAsync();
            
            if (doctors.doctorList.Count==0)
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }
            return doctors;
        }

        public async Task<ListDoctorDto> GetAllSortedByPatientsDescAsync()
        {
            ListDoctorDto doctors = await _repository.GetAllSortedByPatientsDescAsync();
            
            if (doctors.doctorList.Count==0)
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }
            
            return doctors;
        }

        public async Task<DoctorDto> GetById(int id)
        {
            DoctorDto doc = await _repository.GetByIdAsync(id);

            if (doc == null)
            {
                throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
            }

            return doc;
        }

        public async Task<DoctorDto> GetByNameAsync(string name)
        {
            DoctorDto doc = await _repository.GetByNameAsync(name);

            if (doc == null)
            {
                throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
            }

            return doc;
        }

        public async Task<ListDoctorDto> GetByNameStartingWithAsync(string prefix)
        {
            ListDoctorDto doctors = await _repository.GetByNameStartingWithAsync(prefix);

            if (doctors.doctorList.Count==0)
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }
            
            return doctors;
        }

        public async Task<ListDoctorDto> GetByPatientIntervalAsync(int minPatients, int maxPatients)
        {
            ListDoctorDto doctors = await _repository.GetByPatientIntervalAsync(minPatients, maxPatients);

            if (doctors.doctorList.Count==0)
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }
            
            return doctors;

        }

        public async Task<ListDoctorDto> GetByType(string type)
        {
            ListDoctorDto doctors = await _repository.GetByTypeAsync(type);

            if (doctors.doctorList.Count==0)
            {
                throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
            }
            
            return doctors;
        }

        public async Task<ListDoctorDto> GetByTypeWithMinPatientsAsync(string type, int minPatients)
        {
            ListDoctorDto doctors = await _repository.GetByTypeWithMinPatientsAsync(type,minPatients);

            if (doctors.doctorList.Count==0)
            {
                throw new ItemDoesNotExist(Constants.NO_DOCTORS_EXIST);
            }
            
            return doctors;
        }
    }
}
