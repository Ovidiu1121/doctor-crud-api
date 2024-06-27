using AutoMapper;
using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Dto;

namespace DoctorCrudApi.Mappings
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateDoctorRequest, Doctor>();
            CreateMap<UpdateDoctorRequest, Doctor>();
            CreateMap<DoctorDto, Doctor>().ReverseMap();
        }
    }
}
