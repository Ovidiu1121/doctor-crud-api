using DoctorCrudApi.Controllers;
using DoctorCrudApi.Controllers.interfaces;
using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Doctors.Service.interfaces;
using DoctorCrudApi.Dto;
using DoctorCrudApi.System.Constant;
using DoctorCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;
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
    public class TestController
    {

        Mock<IDoctorCommandService> _command;
        Mock<IDoctorQueryService> _query;
        DoctorApiController _controller;

        public TestController()
        {
            _command = new Mock<IDoctorCommandService>();
            _query = new Mock<IDoctorQueryService>();
            _controller = new DoctorController(_command.Object, _query.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {

            _query.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST));
           
            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.DOCTOR_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {

            var doctors = TestDoctorFactory.CreateDoctors(5);

            _query.Setup(repo => repo.GetAll()).ReturnsAsync(doctors);

            var result = await _controller.GetAll();

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            var doctorsAll = Assert.IsType<ListDoctorDto>(okresult.Value);

            Assert.Equal(5, doctorsAll.doctorList.Count);
            Assert.Equal(200, okresult.StatusCode);


        }

        [Fact]
        public async Task Create_InvalidData()
        {

            var create = new CreateDoctorRequest
            {
                Name = "test",
                Type = "test",
                Patients = 0
            };

            _command.Setup(repo => repo.CreateDoctor(It.IsAny<CreateDoctorRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.DOCTOR_ALREADY_EXIST));

            var result = await _controller.CreateDoctor(create);

            var bad=Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400,bad.StatusCode);
            Assert.Equal(Constants.DOCTOR_ALREADY_EXIST, bad.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {

            var create = new CreateDoctorRequest
            {
                Name="test",
                Type="test",
                Patients=20
            };

            var doctor = TestDoctorFactory.CreateDoctor(5);

            doctor.Name=create.Name;
            doctor.Type=create.Type;
            doctor.Patients=create.Patients;

            _command.Setup(repo => repo.CreateDoctor(create)).ReturnsAsync(doctor);

            var result = await _controller.CreateDoctor(create);

            var okResult= Assert.IsType<CreatedResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 201);
            Assert.Equal(doctor, okResult.Value);

        }

        [Fact]
        public async Task Update_InvalidDate()
        {

            var update = new UpdateDoctorRequest
            {
                Patients=0
            };

            _command.Setup(repo => repo.UpdateDoctor(11, update)).ThrowsAsync(new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST));

            var result = await _controller.UpdateDoctor(11, update);

            var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 404);
            Assert.Equal(bad.Value, Constants.DOCTOR_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Update_ValidData()
        {

            var update = new UpdateDoctorRequest
            {
                Patients = 200
            };

            var doctor=TestDoctorFactory.CreateDoctor(5);
            doctor.Patients=update.Patients.Value;

            _command.Setup(repo=>repo.UpdateDoctor(5,update)).ReturnsAsync(doctor);

            var result = await _controller.UpdateDoctor(5, update);

            var okResult=Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, doctor);

        }


        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _command.Setup(repo=>repo.DeleteDoctor(2)).ThrowsAsync(new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST));

            var result= await _controller.DeleteDoctor(2);

            var notfound= Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notfound.StatusCode, 404);
            Assert.Equal(notfound.Value, Constants.DOCTOR_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var doctor = TestDoctorFactory.CreateDoctor(1);

            _command.Setup(repo => repo.DeleteDoctor(1)).ReturnsAsync(doctor);

            var result = await _controller.DeleteDoctor(1);

            var okResult=Assert.IsType<AcceptedResult>(result.Result);

            Assert.Equal(202, okResult.StatusCode);
            Assert.Equal(doctor, okResult.Value);

        }

    }
}
