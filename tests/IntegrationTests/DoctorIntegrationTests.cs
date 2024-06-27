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

public class DoctorIntegrationTests : IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DoctorIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "new doctor", Type = "new type", Patients = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Doctor>(responseString);

        Assert.NotNull(result);
        Assert.Equal(doctor.Name, result.Name);
        Assert.Equal(doctor.Type, result.Type);
        Assert.Equal(doctor.Patients, result.Patients);
        
        EraseAll_CleanUpDB();

    }
    
    [Fact]
    public async Task Post_Create_DoctorAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "new doctor", Type = "new type", Patients = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
        EraseAll_CleanUpDB();
    }

    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "new doctor", Type = "new type", Patients = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Doctor>(responseString)!;

        request = "/api/v1/Doctor/update/"+result.Id;
        var updateDoctor = new UpdateDoctorRequest { Patients = 99 };
        content = new StringContent(JsonConvert.SerializeObject(updateDoctor), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<Doctor>(responseString)!;

        Assert.Equal(updateDoctor.Patients, result.Patients);
        
        EraseAll_CleanUpDB();
    }

    [Fact]
    public async Task Put_Update_DoctorDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Doctor/update/1";
        var updateDoctor = new UpdateDoctorRequest { Patients = 77 };
        var content = new StringContent(JsonConvert.SerializeObject(updateDoctor), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
        EraseAll_CleanUpDB();
    }

    [Fact]
    public async Task Delete_Delete_DoctorExists_ReturnsDeletedDoctor()
    {

        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "new doctor", Type = "new type", Patients = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Doctor>(responseString)!;

        request = "/api/v1/Doctor/delete/" + result.Id;
        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
        
        EraseAll_CleanUpDB();
    }

    [Fact]
    public async Task Delete_Delete_DoctorDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Doctor/delete/66";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
        EraseAll_CleanUpDB();
        
    }

    [Fact]
    public async Task Get_GetByName_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "new doctor", Type = "new type", Patients = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Doctor>(responseString)!;

        request = "/api/v1/Doctor/name/" + result.Name;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);

        EraseAll_CleanUpDB();
    }

    [Fact]
    public async Task Get_GetByName_DoctorDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Doctor/name/test";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }

    [Fact]
    public async Task Get_GetByType_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "new doctor", Type = "new type", Patients = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Doctor>(responseString);

        request = "/api/v1/Doctor/type/" + result.Type;

        response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);

        EraseAll_CleanUpDB();
    }

    [Fact]
    public async Task Get_GetByType_NoDoctorExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Doctor/type/test";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
        EraseAll_CleanUpDB();
    }

    [Fact]
    public async Task Get_GetById_ValidRequest_ReturnsOKStatusCode()
    {
        var request = "/api/v1/Doctor/create";
        var doctor = new CreateDoctorRequest { Name = "new doctor", Type = "new type", Patients = 2088 };
        var content = new StringContent(JsonConvert.SerializeObject(doctor), Encoding.UTF8, "application/json");
        await _client.PostAsync(request, content);
        
        var doctor2 = new CreateDoctorRequest { Name = "new doctor2", Type = "new type2", Patients = 33 };
        content = new StringContent(JsonConvert.SerializeObject(doctor2), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(request, content);
       
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Doctor>(responseString)!;
       
        request = "/api/v1/Doctor/id/" + result.Id;
        response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        
        EraseAll_CleanUpDB();
        
    }
    
    [Fact]
    public async Task Get_GetById_DoctorDoesExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Doctor/id/1";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
        EraseAll_CleanUpDB();
    }
    
    private async void EraseAll_CleanUpDB()
    {
        var request = "/api/v1/Doctor/all";
        var response = await _client.GetAsync(request);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ListDoctorDto>(responseString);
        
        foreach (DoctorDto doc in result.doctorList)
        {
            request = "/api/v1/Doctor/delete/" + doc.Id;
        
            await _client.DeleteAsync(request);
        
        }
        
        Console.WriteLine(result.doctorList);

    }
    
    
    
    
    
}