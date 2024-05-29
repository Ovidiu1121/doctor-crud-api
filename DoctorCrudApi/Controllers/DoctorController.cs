using DoctorCrudApi.Controllers.interfaces;
using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Doctors.Service.interfaces;
using DoctorCrudApi.Dto;
using DoctorCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DoctorCrudApi.Controllers
{
    public class DoctorController:DoctorApiController
    {
        private IDoctorCommandService _doctorCommandService;
        private IDoctorQueryService _doctorQueryService;

        public DoctorController(IDoctorCommandService doctorCommandService, IDoctorQueryService doctorQueryService)
        {
           _doctorCommandService = doctorCommandService;
           _doctorQueryService = doctorQueryService;
        }

        public override async Task<ActionResult<Doctor>> CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            try
            {
                var doctors = await _doctorCommandService.CreateDoctor(request);

                return Ok(doctors);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<Doctor>> DeleteDoctor([FromRoute] int id)
        {
            try
            {
                var doctors = await _doctorCommandService.DeleteDoctor(id);

                return Accepted("", doctors);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Doctor>>> GetAll()
        {
            try
            {
                var doctors = await _doctorQueryService.GetAll();
                return Ok(doctors);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Doctor>> GetByTypeRoute([FromRoute] string type)
        {
            try
            {
                var doctors = await _doctorQueryService.GetByType(type);
                return Ok(doctors);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Doctor>> UpdateDoctor([FromRoute] int id, [FromBody] UpdateDoctorRequest request)
        {
            try
            {
                var doctors = await _doctorCommandService.UpdateDoctor(id, request);

                return Ok(doctors);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
