using AutoMapper;
using DoctorCrudApi.Data;
using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Doctors.Repository.interfaces;
using DoctorCrudApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace DoctorCrudApi.Doctors.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DoctorRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DoctorDto> CreateDoctor(CreateDoctorRequest request)
        {
                var doc = _mapper.Map<Doctor>(request);

            _context.Doctors.Add(doc);

            await _context.SaveChangesAsync();

            return _mapper.Map<DoctorDto>(doc);
        }

        public async Task<DoctorDto> DeleteDoctorById(int id)
        {
            var doc = await _context.Doctors.FindAsync(id);

            _context.Doctors.Remove(doc);

            await _context.SaveChangesAsync();

            return _mapper.Map<DoctorDto>(doc);
        }

        public async Task<bool> DoctorExistsByIdAsync(int id)
        {
            return await _context.Doctors.AnyAsync(d => d.Id == id);
        }

        public async Task<bool> DoctorExistsByNameAsync(string name)
        {
            return await _context.Doctors.AnyAsync(d => d.Name == name);
        }

        public async Task<ListDoctorDto> GetAllAsync()
        {
            List<Doctor> result = await _context.Doctors.ToListAsync();
            
            ListDoctorDto listDoctorDto = new ListDoctorDto()
            {
                doctorList = _mapper.Map<List<DoctorDto>>(result)
            };

            return listDoctorDto;
        }

        public async Task<ListDoctorDto> GetAllSortedByPatientsAscAsync()
        {
            List<Doctor> result = await _context.Doctors.ToListAsync();

            ListDoctorDto listDoctorDto = new ListDoctorDto()
            {
                doctorList = _mapper.Map<List<DoctorDto>>(result).OrderBy(d=>d.Patients).ToList()
            };

            return listDoctorDto;
        }

        public async Task<ListDoctorDto> GetAllSortedByPatientsDescAsync()
        {
            List<Doctor> result = await _context.Doctors.ToListAsync();

            ListDoctorDto listDoctorDto = new ListDoctorDto()
            {
                doctorList = _mapper.Map<List<DoctorDto>>(result).OrderByDescending(d => d.Patients).ToList()
            };

            return listDoctorDto;
        }

        public  async Task<DoctorDto> GetByIdAsync(int id)
        {
            var doctor = await _context.Doctors.Where(dc => dc.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<DoctorDto> GetByNameAsync(string name)
        {
            var doctor = await _context.Doctors.Where(dc => dc.Name.Equals(name)).FirstOrDefaultAsync();
            
            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<ListDoctorDto> GetByNameStartingWithAsync(string prefix)
        {
            List<Doctor> result = await _context.Doctors.ToListAsync();

            ListDoctorDto listDoctorDto = new ListDoctorDto()
            {
                doctorList = _mapper.Map<List<DoctorDto>>(result).Where(d => d.Name.StartsWith(prefix)).ToList()
            };

            return listDoctorDto;

        }

        public async Task<ListDoctorDto> GetByPatientIntervalAsync(int minPatients, int maxPatients)
        {
            List<Doctor> result = await _context.Doctors.ToListAsync();

            ListDoctorDto listDoctorDto = new ListDoctorDto()
            {
                doctorList = _mapper.Map<List<DoctorDto>>(result)
                    .Where(d => d.Patients >= minPatients && d.Patients <= maxPatients).ToList()
            };

            return listDoctorDto;
        }

        public  async Task<ListDoctorDto> GetByTypeAsync(string type)
        {
            List<Doctor> result = await _context.Doctors.ToListAsync();

            ListDoctorDto listDoctorDto = new ListDoctorDto()
            {
                doctorList = _mapper.Map<List<DoctorDto>>(result).Where(d => d.Type.Equals(type)).ToList()
            };

            return listDoctorDto;

        }

        public async Task<ListDoctorDto> GetByTypeWithMinPatientsAsync(string type, int minPatients)
        {
            List<Doctor> result = await _context.Doctors.ToListAsync();

            ListDoctorDto listDoctorDto = new ListDoctorDto()
            {
                doctorList = _mapper.Map<List<DoctorDto>>(result)
                    .Where(d => d.Type.Equals(type) && d.Patients >= minPatients).ToList()
            };

            return listDoctorDto;

        }

        public async Task<DoctorDto> UpdateDoctor(int id, UpdateDoctorRequest request)
        {
            var doc = await _context.Doctors.FindAsync(id);

            doc.Patients=request.Patients ?? doc.Patients;

            _context.Doctors.Update(doc);

            await _context.SaveChangesAsync();

            return _mapper.Map<DoctorDto>(doc);
        }
    }
}
