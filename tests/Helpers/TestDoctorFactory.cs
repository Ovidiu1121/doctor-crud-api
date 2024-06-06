using DoctorCrudApi.Doctors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Helpers
{
    public class TestDoctorFactory
    {

        public static Doctor CreateDoctor(int id)
        {
            return new Doctor
            {
                Id = id,
                Name="Alex"+id,
                Type="dentist"+id,
                Patients=30+id
            };
        }

        public static List<Doctor> CreateDoctors(int count)
        {
            List<Doctor> doctors=new List<Doctor>();

            for(int i = 0; i<count; i++)
            {
                doctors.Add(CreateDoctor(i));
            }
            return doctors;
        }

    }
}
