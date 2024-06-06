using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Doctors.Repository.interfaces;
using DoctorCrudApi.Doctors.Service;
using DoctorCrudApi.Doctors.Service.interfaces;
using DoctorCrudApi.Dto;
using DoctorCrudApi.System.Constant;
using DoctorCrudApi.System.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests
{
    public class TestCommandService
    {
        Mock<IDoctorRepository> _mock;
        IDoctorCommandService _service;

        public TestCommandService()
        {
            _mock = new Mock<IDoctorRepository>();
            _service = new DoctorCommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidData()
        {
            var create = new CreateDoctorRequest
            {
                Name="Test",
                Type="test",
                Patients=0
            };

            var doctor = TestDoctorFactory.CreateDoctor(5);

            _mock.Setup(repo => repo.GetByTypeAsync("test")).ReturnsAsync(doctor);
                
           var exception=  await Assert.ThrowsAsync<ItemAlreadyExists>(()=>_service.CreateDoctor(create));

            Assert.Equal(Constants.DOCTOR_ALREADY_EXIST, exception.Message);



        }

        [Fact]
        public async Task Create_ReturnDoctor()
        {

            var create = new CreateDoctorRequest
            {
                Name="Test",
                Type="test",
                Patients=27
            };

            var doctor= TestDoctorFactory.CreateDoctor(5);

            doctor.Name=create.Name;
            doctor.Type=create.Type;
            doctor.Patients=create.Patients;

            _mock.Setup(repo => repo.CreateDoctor(It.IsAny<CreateDoctorRequest>())).ReturnsAsync(doctor);

            var result = await _service.CreateDoctor(create);

            Assert.NotNull(result);
            Assert.Equal(result, doctor);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateDoctorRequest
            {
                Patients = 2000
            };

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Doctor)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateDoctor(1, update));

            Assert.Equal(Constants.DOCTOR_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task Update_InvalidData()
        {
            var update = new UpdateDoctorRequest
            {
                Patients = 0
            };

            _mock.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync((Doctor)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateDoctor(5, update));

            Assert.Equal(Constants.DOCTOR_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateDoctorRequest
            {
                Patients = 24
            };

            var doctor = TestDoctorFactory.CreateDoctor(5);

            doctor.Patients=update.Patients.Value;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(doctor);
            _mock.Setup(repoo => repoo.UpdateDoctor(It.IsAny<int>(), It.IsAny<UpdateDoctorRequest>())).ReturnsAsync(doctor);

            var result = await _service.UpdateDoctor(5, update);

            Assert.NotNull(result);
            Assert.Equal(doctor, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.DeleteDoctorById(It.IsAny<int>())).ReturnsAsync((Doctor)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteDoctor(5));

            Assert.Equal(exception.Message, Constants.DOCTOR_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var doctor = TestDoctorFactory.CreateDoctor(1);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(doctor);

            var result= await _service.DeleteDoctor(1);

            Assert.NotNull(result);
            Assert.Equal(doctor, result);


        }
    }
}
