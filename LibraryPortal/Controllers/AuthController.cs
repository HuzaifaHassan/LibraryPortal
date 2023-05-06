using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DbHandler.Data;
using DbHandler.Model;
using DbHandler.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using static LibraryPortal.DTO.DTO;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using LibraryPortal.Responses;
using LibraryPortal.Models;
using LibraryPortal.DTO;
using LibraryPortal.Helper;
using System.Transactions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;

namespace LibraryPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IConfiguration _config;
        private readonly IStudentRepositories _studentRepositories;
        private readonly IAddStudentRepository _addStudentRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ILibraryDuesRepository _libraryDuesRepository;
        private readonly IStudentDuesRepository _addStudentDuesRepository;
        private readonly APIHelper _helper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(ApplicationDbContext ctx, IConfiguration config, IStudentRepositories studentRepositories,IAddStudentRepository addStudentRepository, IAdminRepository adminRepository 
                                 ,ILibraryDuesRepository libraryDuesRepository, IStudentDuesRepository addstudentRepositories, UserManager<ApplicationUser> usermanager)
        {
            _ctx = ctx;
            _config = config;   
            _studentRepositories = studentRepositories; 
            _addStudentRepository = addStudentRepository;   
            _adminRepository = adminRepository;
            _libraryDuesRepository= libraryDuesRepository;
            _addStudentDuesRepository = addstudentRepositories;
            _userManager= usermanager;
            _helper = new APIHelper(studentRepositories, usermanager, config);


            
          
        
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO DTO)
        {
            DateTime _startTime = DateTime.Now;
            try
            {
                var user = _adminRepository.GetByNameandPassword(DTO.name, DTO.password);
                if (user == null || user.password != DTO.password)
                {
                    return await _helper.Response("err-001", Level.Error, "Invalid email or password", ActiveErrorCode.Failed, _startTime, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.Unauthorized, null, false);
                }
                return await _helper.Response("suc-001", Level.Success, user.name, ActiveErrorCode.Success, _startTime, HttpContext, _config, DTO.BaseClass, DTO, user.name, ReturnResponse.Success, null, true);

            }
            catch (Exception ex)
            {
                return await _helper.Response("err-001", Level.Error, ex.Message, ActiveErrorCode.Failed, _startTime, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.BadRequest, ex, false);
            }





        }
        [HttpPost]
        [Route("StudentLogin")]
        public async Task<IActionResult> StudentLogin([FromBody] LoginDTO DTO)
        {

            DateTime _startTime = DateTime.Now;
            try
            {
                var user = _studentRepositories.GetByNameandPassword(DTO.name, DTO.password);
                if (user == null || user.Password != DTO.password)
                {
                    return await _helper.Response("err-001", Level.Error, "Invalid email or password", ActiveErrorCode.Failed, _startTime, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.Unauthorized, null, false);
                }
                return await _helper.Response("suc-001", Level.Success, user.Name, ActiveErrorCode.Success, _startTime, HttpContext, _config, DTO.BaseClass, DTO, user.Name, ReturnResponse.Success, null, true);

            }
            catch (Exception ex)
            {
                return await _helper.Response("err-001", Level.Error, ex.Message, ActiveErrorCode.Failed, _startTime, HttpContext, _config, DTO.BaseClass, DTO, "", ReturnResponse.BadRequest, ex, false);
            }
        }
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(ActiveResponse<RegistraiontObject>), 200)]

        public async Task<IActionResult> AddStudent([FromBody] StudentDTO DTO)
        {
            DateTime _startTime = DateTime.Now;
            var name = "";
            bool exist = false;
            var id = "";
            try
            {
                var jso = JsonConvert.SerializeObject(DTO);

                var forLog = JsonConvert.DeserializeObject<StudentDTO>(jso);
                if (!TryValidateModel(DTO))
                {
                    //if (!ModelState.IsValid)
                    //{
                    return await _helper.Response("err-Model", Level.Success, _helper.GetErrors(ModelState), ActiveErrorCode.Failed, _startTime, HttpContext, _config, DTO.BaseClass, forLog, "", ReturnResponse.BadRequest, null, false);    //}

                }
                var addStudent = new StudentDetails
                {
                    Id = DTO.Id,
                    stId = DTO.stId,
                    cstID = DTO.cstID,
                    CreatedOn = DTO.CreatedOn,
                    IsActive = DTO.IsActive,
                    Name = DTO.Name,
                    LastName = DTO.LastName,
                    Email = DTO.Email,
                    Password = DTO.Password,
                    MobileNo = DTO.MobileNo,
                    IsGraduated = "No"
                };
                //var client = new HttpClient();
                //client.BaseAddress = new Uri("https://localhost:7120/"); // replace with the correct base URL of the finance portal
                //var requestUri = "api/Course/GettReference";
                //var requestBody = new StringContent(JsonConvert.SerializeObject(a), Encoding.UTF8, "application/json");
                //var response = await client.PostAsync(requestUri, requestBody);

                //// Check response status
                //var content = await response.Content.ReadAsStringAsync();
                //var financeResponse = JsonConvert.DeserializeObject<ActiveResponse<AddCourseDue>>(content);
                _studentRepositories.StudentDetails(addStudent);

                _studentRepositories.Save();
                return await _helper.Response("suc-001", Level.Success, addStudent, ActiveErrorCode.Success, _startTime, HttpContext, _config, DTO.BaseClass, forLog, id, ReturnResponse.Success, null, true);

            }
            catch (Exception ex)
            {

                var jso = JsonConvert.SerializeObject(DTO);

                var forLog = JsonConvert.DeserializeObject<StudentDTO>(jso);

                return await _helper.Response("ex-0001", Level.Error, null, ActiveErrorCode.Failed, _startTime, HttpContext, null, DTO.BaseClass, forLog, "", ReturnResponse.BadRequest, ex, false);

            }



        }
        [HttpPost]
        [Route("CreateStudentAccount")]
        [ProducesResponseType(typeof(ActiveResponse<AddStudent>), 200)]
        public async Task<IActionResult> ApproveStudentAccount([FromBody] StudentDTO DTO)
        {

            DateTime _startTime = DateTime.Now;
            var name = "";
            bool exist = false;
            var id = "";
            try
            {
                var jso = JsonConvert.SerializeObject(DTO);

                var forLog = JsonConvert.DeserializeObject<StudentDTO>(jso);
                if (!TryValidateModel(DTO))
                {
                    //if (!ModelState.IsValid)
                    //{
                    return await _helper.Response("err-Model", Level.Success, _helper.GetErrors(ModelState), ActiveErrorCode.Failed, _startTime, HttpContext, _config, DTO.BaseClass, forLog, "", ReturnResponse.BadRequest, null, false);    //}

                }
                var addStudentt = new AddStudent
                {
                    Id = DTO.Id,
                    stId = DTO.stId,
                    cstID = DTO.cstID,
                    CreatedOn = DTO.CreatedOn,
                    IsActive = DTO.IsActive,
                    Name = DTO.Name,
                    LastName = DTO.LastName,
                    Email = DTO.Email,
                    Password = DTO.Password,
                    MobileNo = DTO.MobileNo,
                    IsGraduated = DTO.IsGraduated
                };
                _addStudentRepository.AddStudentDets(addStudentt);
                _addStudentRepository.Save();
                return await _helper.Response("suc-001", Level.Success, addStudentt, ActiveErrorCode.Success, _startTime, HttpContext, _config, DTO.BaseClass, forLog, id, ReturnResponse.Success, null, true);

            }
            catch (Exception ex)
            {

                var jso = JsonConvert.SerializeObject(DTO);

                var forLog = JsonConvert.DeserializeObject<StudentDTO>(jso);

                return await _helper.Response("ex-0001", Level.Error, null, ActiveErrorCode.Failed, _startTime, HttpContext, null, DTO.BaseClass, forLog, "", ReturnResponse.BadRequest, ex, false);

            }
        }

        [HttpPost]
        [Route("GetBooks")]
        public async Task<IActionResult> GetBookByISBN(string isbn)
        {
            DateTime _startTime = DateTime.Now;
            var id = "";
           
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(json);
            var bookData = data["items"]?.FirstOrDefault();
            if (bookData == null)
            {
                return null;
            }
            var volumeInfo = bookData["volumeInfo"];
            var book = new LibraryDues
            {
                ISBN = isbn,
                Title = volumeInfo["title"]?.ToString(),
                Author = volumeInfo["authors"]?.FirstOrDefault()?.ToString(),
                // Add other properties as needed
            };
            return await _helper.Response("suc-001", Level.Success, book, ActiveErrorCode.Success, _startTime, HttpContext, _config, null, null, id, ReturnResponse.Success, null, true);

        }
        [HttpPost]
        [Route("GetBooksV2")]
        public async Task<IActionResult> GetBookByISBNV2(string isbn, [FromBody] LibraryDuesDTO DTO)
        {
            DateTime _startTime = DateTime.Now;
            var id = "";
            var _ref = "";
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(json);
            var bookData = data["items"]?.FirstOrDefault();
            var Reff = Guid.NewGuid().ToString();
            _ref = "#" + Reff;
            if (bookData == null)
            {
                return null;
            }
            var volumeInfo = bookData["volumeInfo"];
           
            var bookDueDate = DateTime.Now.AddDays(1);
            var daysOverDue = (_startTime - bookDueDate).TotalDays;
            var fine = daysOverDue > 0 ? (decimal)(daysOverDue * 0.25) : 0;

            var book = new LibraryDues
            {
                id = DTO.id,
                cstid = DTO.cstid,
                Reference = _ref,
                DueDate = bookDueDate,
                daysoverDue = Convert.ToDateTime(daysOverDue),
                LibraryDue = fine.ToString(),
                IsCleared=false,
                ISBN = isbn,
                Title = volumeInfo["title"]?.ToString(),
                Author = volumeInfo["authors"]?.FirstOrDefault()?.ToString(),
                
            };
            _libraryDuesRepository.AddLibraryDue(book);
            _libraryDuesRepository.Save();
            return await _helper.Response("suc-001", Level.Success, book, ActiveErrorCode.Success, _startTime, HttpContext, _config, null, null, id, ReturnResponse.Success, null, true);

        }
    }
}
