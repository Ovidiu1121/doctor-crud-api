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
        public abstract Task<ActionResult<IEnumerable<Doctor>>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Doctor>> CreateDoctor([FromBody] CreateDoctorRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Doctor>> UpdateDoctor([FromRoute] int id, [FromBody] UpdateDoctorRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 201, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Doctor>> DeleteDoctor([FromRoute] int id);

        [HttpGet("{type}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Doctor>> GetByTypeRoute([FromRoute] string type);

        [HttpGet("name/{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Doctor))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Doctor>> GetByNameRoute([FromRoute] string name);

        [HttpGet("sortedByPatientsAsc")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<IEnumerable<Doctor>>> GetAllSortedByPatientsAsc();

        [HttpGet("sortedByPatientsDesc")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<IEnumerable<Doctor>>> GetAllSortedByPatientsDesc();

        [HttpGet("nameStartsWith/{prefix}")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<IEnumerable<Doctor>>> GetByNameStartingWith([FromRoute] string prefix);

        [HttpGet("allByPatientsInterval")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<IEnumerable<Doctor>>> GetByPatientInterval([FromQuery] int minPatients, [FromQuery] int maxPatients);

        [HttpGet("allByTypeWithMinPatients")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<IEnumerable<Doctor>>> GetByTypeWithMinPatients([FromQuery] string type, [FromQuery] int minPatients);

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
