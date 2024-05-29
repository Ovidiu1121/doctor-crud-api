using DoctorCrudApi.Doctors.Model;
using Microsoft.EntityFrameworkCore;

namespace DoctorCrudApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Doctor> Doctors { get; set; }
    }
}
