
using FluentMigrator.Runner.Initialization;

namespace DoctorCrudApi.Dto;

public class ListDoctorDto
{
    public ListDoctorDto()
    {
        doctorList = new List<DoctorDto>();
    }
    
    public List<DoctorDto> doctorList { get; set; }
}