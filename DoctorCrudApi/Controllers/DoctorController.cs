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

        public override async Task<ActionResult<bool>> DoctorExistsByIdAsync([FromRoute] int id)
        {
            try
            {
                var result = await _doctorQueryService.DoctorExistsByIdAsync(id);

                return Accepted("", result);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<bool>> DoctorExistsByNameAsync([FromRoute] string name)
        {
            try
            {
                var result = await _doctorQueryService.DoctorExistsByNameAsync(name);

                return Accepted("", result);
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

        public override async Task<ActionResult<IEnumerable<Doctor>>> GetAllSortedByPatientsAsc()
        {
            try
            {
                var doctors = await _doctorQueryService.GetAllSortedByPatientsAscAsync();
                return Ok(doctors);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Doctor>>> GetAllSortedByPatientsDesc()
        {
            try
            {
                var doctors = await _doctorQueryService.GetAllSortedByPatientsDescAsync();
                return Ok(doctors);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Doctor>> GetByNameRoute([FromRoute] string name)
        {
            try
            {
                var doc = await _doctorQueryService.GetByNameAsync(name);
                return Ok(doc);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Doctor>>> GetByNameStartingWith(string prefix)
        {
            try
            {
                var doctors = await _doctorQueryService.GetByNameStartingWithAsync(prefix);
                return Ok(doctors);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Doctor>>> GetByPatientInterval(int minPatients, int maxPatients)
        {
            try
            {
                var doctors = await _doctorQueryService.GetByPatientIntervalAsync(minPatients, maxPatients);
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
                var doc = await _doctorQueryService.GetByType(type);
                return Ok(doc);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Doctor>>> GetByTypeWithMinPatients(string type, int minPatients)
        {
            try
            {
                var doctors = await _doctorQueryService.GetByTypeWithMinPatientsAsync(type, minPatients);
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
