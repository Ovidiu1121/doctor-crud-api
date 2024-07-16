using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DoctorCrudApi.Doctors.Model;
using DoctorCrudApi.Dto;
using Newtonsoft.Json;
using tests.Helpers;
using tests.Infrastructure;
using Xunit;

namespace tests.IntegrationTests;

public class DoctorIntegrationTests : IClassFixture<ApiWebApplicationFactory>, IDisposable
{
    private readonly HttpClient _client;
    private bool _disposed = false;

    public DoctorIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    public void Dispose()
    {
        CleanUpDatabase().GetAwaiter().GetResult();
        _disposed = true;
    }

    private async Task CleanUpDatabase()
    {
        var request = "/api/v1/Doctor/all";
        var response = await _client.GetAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListDoctorDto>(responseString);
            foreach (DoctorDto doc in result.doctorList)
            {
                request = "/api/v1/Doctor/delete/" + doc.Id;
                await _client.DeleteAsync(request);
            }
        }
    }

    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidDoctorContentResponse()
    {
        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "Dr. Smith", Type = "General", Patients = 50 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Doctor>(responseString);

        Assert.NotNull(result);
        Assert.Equal(doctor.Name, result.Name);
        Assert.Equal(doctor.Type, result.Type);
        Assert.Equal(doctor.Patients, result.Patients);
    }

    [Fact]
    public async Task Post_Create_DoctorAlreadyExists_ReturnsBadRequestStatusCode()
    {

        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "Dr. Smith", Type = "General", Patients = 50 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");
        
        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);
        
        Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);


    }

    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
    {

        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "Dr. Smith", Type = "General", Patients = 50 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Doctor>(responseString);

        request = "/api/v1/Doctor/update/" + result.Id;
        var updateDoctor = new UpdateDoctorRequest { Patients = 23 };
        content = new StringContent(JsonConvert.SerializeObject(updateDoctor), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<Doctor>(responseString);

        Assert.Equal(updateDoctor.Patients,result.Patients);
        
    }

    [Fact]
    public async Task Put_Upadte_DoctorDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Doctor/update/15";
        var updateDoctor = new UpdateDoctorRequest { Patients = 44 };
        var content = new StringContent(JsonConvert.SerializeObject(updateDoctor), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    [Fact]
    public async Task Delete_DoctorExists_ReturnsAcceptedStatusCode()
    {
        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "Dr. Johnson", Type = "Pediatrician", Patients = 30 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");

        var postResponse = await _client.PostAsync(request, content);
        var postResponseString = await postResponse.Content.ReadAsStringAsync();
        var createdDoctor = JsonConvert.DeserializeObject<Doctor>(postResponseString);

        request = "/api/v1/Doctor/delete/" + createdDoctor.Id;
        var deleteResponse = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_DoctorDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Doctor/delete/44";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }

    [Fact]
    public async Task Get_GetById_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "Dr. Smith", Type = "General", Patients = 50 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var stringResponse = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Doctor>(stringResponse);

        request = "/api/v1/Doctor/id/" + result.Id;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }

    [Fact]
    public async Task Get_GetById_DoctorDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Doctor/id/33";
        
        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    
}