using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DoctorCrudApi.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class DoctorApiController: ControllerBase
    {
        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListDoctorDto>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<DoctorDto>> CreateDoctor([FromBody] CreateDoctorRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<DoctorDto>> UpdateDoctor([FromRoute] int id, [FromBody] UpdateDoctorRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 201, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<DoctorDto>> DeleteDoctor([FromRoute] int id);

        [HttpGet("type/{type}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<ListDoctorDto>> GetByTypeRoute([FromRoute] string type);

        [HttpGet("name/{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<DoctorDto>> GetByNameRoute([FromRoute] string name);

        [HttpGet("id/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<DoctorDto>> GetByIdRoute([FromRoute] int id);

        [HttpGet("sortedByPatientsAsc")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListDoctorDto>> GetAllSortedByPatientsAsc();

        [HttpGet("sortedByPatientsDesc")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListDoctorDto>> GetAllSortedByPatientsDesc();

        [HttpGet("nameStartsWith/{prefix}")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListDoctorDto>> GetByNameStartingWith([FromRoute] string prefix);

        [HttpGet("allByPatientsInterval")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListDoctorDto>> GetByPatientInterval([FromQuery] int minPatients, [FromQuery] int maxPatients);

        [HttpGet("allByTypeWithMinPatients")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListDoctorDto>> GetByTypeWithMinPatients([FromQuery] string type, [FromQuery] int minPatients);

        [HttpGet("doctorExistsById/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(bool))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<bool>> DoctorExistsByIdAsync([FromRoute] int id);

        [HttpGet("doctorExistsByName/{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(bool))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<bool>> DoctorExistsByNameAsync([FromRoute] string name);
    }
}
