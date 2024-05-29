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

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Doctor> GetByTypeAsync(string type)
        {
            return _context.Doctors.FirstOrDefault(x => x.Type.Equals(type));
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
