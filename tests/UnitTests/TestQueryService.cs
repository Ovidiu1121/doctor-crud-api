using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Doctors.Repository.interfaces;
using DoctorCrudApi.Doctors.Service;
using DoctorCrudApi.Doctors.Service.interfaces;
using DoctorCrudApi.System.Constant;
using DoctorCrudApi.System.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorCrudApi.Dto;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests
{
    public class TestQueryService
    {

        Mock<IDoctorRepository> _mock;
        IDoctorQueryService _service;

        public TestQueryService()
        {
            _mock=new Mock<IDoctorRepository>();
            _service=new DoctorQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new ListDoctorDto());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAll());

            Assert.Equal(exception.Message, Constants.NO_DOCTORS_EXIST);       

        }

        [Fact]
        public async Task GetAll_ReturnAllDoctors()
        {

            var doctors = TestDoctorFactory.CreateDoctors(5);

            _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(doctors);

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Contains(doctors.doctorList[1], result.doctorList);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((DoctorDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetById(1));

            Assert.Equal(Constants.DOCTOR_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetById_ReturnDoctor()
        {

            var doctor = TestDoctorFactory.CreateDoctor(5);

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(doctor);

            var result = await _service.GetById(5);

            Assert.NotNull(result);
            Assert.Equal(doctor, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((DoctorDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByNameAsync(""));

            Assert.Equal(Constants.DOCTOR_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetByName_ReturnDoctor()
        {

            var doctor=TestDoctorFactory.CreateDoctor(3);

            doctor.Name="test";

            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(doctor);

            var result = await _service.GetByNameAsync("test");

            Assert.NotNull(result);
            Assert.Equal(doctor, result);

        }

        [Fact]
        public async Task GetByType_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByTypeAsync("")).ReturnsAsync(new ListDoctorDto());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByType(""));

            Assert.Equal(Constants.DOCTOR_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetByType_ReturnDoctor()
        {
            var doctor = TestDoctorFactory.CreateDoctors(3);

            _mock.Setup(repo => repo.GetByTypeAsync("test")).ReturnsAsync(doctor);

            var result = await _service.GetByType("test");

            Assert.NotNull(result);
            Assert.Equal(doctor, result);

        }

        [Fact]
        public async Task GetAllSortedByPatientsAscAsync_ItemsDoNotExist()
        {

            _mock.Setup(repo => repo.GetAllSortedByPatientsAscAsync()).ReturnsAsync(new ListDoctorDto());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetAllSortedByPatientsAscAsync());

            Assert.Equal(exception.Message,Constants.NO_DOCTORS_EXIST);

        }

        [Fact]
        public async Task GetAllSortedByPatientsAscAsync_ReturnDoctors()
        {

            var doctors = TestDoctorFactory.CreateDoctors(5);

            _mock.Setup(repo => repo.GetAllSortedByPatientsAscAsync()).ReturnsAsync(doctors);

            var result = await _service.GetAllSortedByPatientsAscAsync();

            Assert.NotNull(result);
            Assert.Contains(doctors.doctorList[1], result.doctorList);

        }

        [Fact]
        public async Task GetAllSortedByPatientsDescAsync_ItemsDoNotExist()
        {

            _mock.Setup(repo=>repo.GetAllSortedByPatientsDescAsync()).ReturnsAsync(new ListDoctorDto());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAllSortedByPatientsDescAsync());

            Assert.Equal(exception.Message, Constants.NO_DOCTORS_EXIST);

        }

        [Fact]
        public async Task GetAllSortedByPatientsDescAsync_ReturnDoctors()
        {

            var doctors=TestDoctorFactory.CreateDoctors(5);

            _mock.Setup(repo => repo.GetAllSortedByPatientsDescAsync()).ReturnsAsync(doctors);

            var result= await _service.GetAllSortedByPatientsDescAsync();

            Assert.NotNull(result);
            Assert.Contains(doctors.doctorList[1], result.doctorList);

        }

        [Fact]
        public async Task DoctorExistsByIdAsync_ReturnFalse()
        {

            _mock.Setup(repo => repo.DoctorExistsByIdAsync(11)).ReturnsAsync(false);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.DoctorExistsByIdAsync(47));  

            Assert.Equal(Constants.NO_DOCTORS_EXIST, exception.Message);


        }

        [Fact]
        public async Task DoctorExistsByIdAsync_ReturnTrue()
        {

            _mock.Setup(repo => repo.DoctorExistsByIdAsync(1)).ReturnsAsync(true);

            var result = await _service.DoctorExistsByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(true, result);

        }

        [Fact]
        public async Task DoctorExistsByNameAsync_ReturnFalse()
        {

            _mock.Setup(repo => repo.DoctorExistsByNameAsync("")).ReturnsAsync(false);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DoctorExistsByNameAsync(""));

            Assert.Equal(Constants.NO_DOCTORS_EXIST, exception.Message);


        }

        [Fact]
        public async Task DoctorExistsByNameAsync_ReturnTrue()
        {

            _mock.Setup(repo => repo.DoctorExistsByNameAsync("Cart")).ReturnsAsync(true);

            var result = await _service.DoctorExistsByNameAsync("Cart");

            Assert.NotNull(result);
            Assert.Equal(true, result);

        }

        [Fact]
        public async Task GetByNameStartingWithAsync_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByNameStartingWithAsync("")).ReturnsAsync(new ListDoctorDto());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByNameStartingWithAsync(""));

            Assert.Equal(exception.Message, Constants.NO_DOCTORS_EXIST);


        }

        [Fact]
        public async Task GetByNameStartingWithAsync_ReturnDoctors()
        {

            var doctors = TestDoctorFactory.CreateDoctors(9);

            _mock.Setup(repo => repo.GetByNameStartingWithAsync("Ca")).ReturnsAsync(doctors);

            var result = await _service.GetByNameStartingWithAsync("Ca");

            Assert.NotNull(result);
            Assert.Contains(doctors.doctorList[1], result.doctorList);
        }

        [Fact]
        public async Task GetByPatientIntervalAsync_ItemDoesNotExist()
        {

            _mock.Setup(repo=>repo.GetByPatientIntervalAsync(4,2)).ReturnsAsync(new ListDoctorDto());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetByPatientIntervalAsync(4,2));

            Assert.Equal(Constants.NO_DOCTORS_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetByPatientIntervalAsync_ReturnDoctors()
        {

            var doctors = TestDoctorFactory.CreateDoctors(5);

            _mock.Setup(repo => repo.GetByPatientIntervalAsync(1, 4)).ReturnsAsync(doctors);

            var result = await _service.GetByPatientIntervalAsync(1, 4);

            Assert.NotNull(result);
            Assert.Contains(doctors.doctorList[1], result.doctorList);

        }

        [Fact]
        public async Task GetByTypeWithMinPatientsAsync_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByTypeWithMinPatientsAsync("", 3)).ReturnsAsync(new ListDoctorDto());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByTypeWithMinPatientsAsync("", 3));

            Assert.Equal(Constants.NO_DOCTORS_EXIST,exception.Message);
        }

        [Fact]
        public async Task GetByTypeWithMinPatientsAsync_ReturnDoctors()
        {

            var doctors=TestDoctorFactory.CreateDoctors(9);

            _mock.Setup(repo => repo.GetByTypeWithMinPatientsAsync("oftalmolog", 2)).ReturnsAsync(doctors);

            var result = await _service.GetByTypeWithMinPatientsAsync("oftalmolog", 2);

            Assert.NotNull(result);
            Assert.Contains(doctors.doctorList[1], result.doctorList);

        }

    }
}
