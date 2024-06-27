using DoctorCrudApi.Doctors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorCrudApi.Dto;

namespace tests.Helpers
{
    public class TestDoctorFactory
    {

        public static DoctorDto CreateDoctor(int id)
        {
            return new DoctorDto
            {
                Id = id,
                Name="Alex"+id,
                Type="dentist"+id,
                Patients=30+id
            };
        }

        public static ListDoctorDto CreateDoctors(int count)
        {
            ListDoctorDto doctors=new ListDoctorDto();
            
            for(int i = 0; i<count; i++)
            {
                doctors.doctorList.Add(CreateDoctor(i));
            }
            return doctors;
        }

    }
}
