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
        public async Task<Doctor> CreateDoctor(CreateDoctorRequest request)
        {
            var doc = _mapper.Map<Doctor>(request);

            _context.Doctors.Add(doc);

            await _context.SaveChangesAsync();

            return doc;
        }

        public async Task<Doctor> DeleteDoctorById(int id)
        {
            var doc = await _context.Doctors.FindAsync(id);

            _context.Doctors.Remove(doc);

            await _context.SaveChangesAsync();

            return doc;
        }

        public async Task<bool> DoctorExistsByIdAsync(int id)
        {
            return await _context.Doctors.AnyAsync(d => d.Id == id);
        }

        public async Task<bool> DoctorExistsByNameAsync(string name)
        {
            return await _context.Doctors.AnyAsync(d => d.Name == name);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetAllSortedByPatientsAscAsync()
        {
            return await _context.Doctors.OrderBy(d => d.Patients).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetAllSortedByPatientsDescAsync()
        {
            return await _context.Doctors.OrderByDescending(d => d.Patients).ToListAsync();
        }

        public  async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Doctor> GetByNameAsync(string name)
        {
            return _context.Doctors.FirstOrDefault(x => x.Name.Equals(name));
        }

        public async Task<IEnumerable<Doctor>> GetByNameStartingWithAsync(string prefix)
        {
            return await _context.Doctors.Where(d => d.Name.StartsWith(prefix)).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetByPatientIntervalAsync(int minPatients, int maxPatients)
        {
            return await _context.Doctors.Where(d => d.Patients >= minPatients && d.Patients <= maxPatients).ToListAsync();
        }

        public  async Task<Doctor> GetByTypeAsync(string type)
        {
            return _context.Doctors.FirstOrDefault(x => x.Type.Equals(type));
        }

        public async Task<IEnumerable<Doctor>> GetByTypeWithMinPatientsAsync(string type, int minPatients)
        {
            return await _context.Doctors.Where(d => d.Type == type && d.Patients >= minPatients).ToListAsync();
        }

        public async Task<Doctor> UpdateDoctor(int id, UpdateDoctorRequest request)
        {
            var doc = await _context.Doctors.FindAsync(id);

            doc.Patients=request.Patients ?? doc.Patients;

            _context.Doctors.Update(doc);

            await _context.SaveChangesAsync();

            return doc;
        }
    }
}
