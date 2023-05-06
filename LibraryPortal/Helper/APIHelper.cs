using DbHandler.Model;
using DbHandler.Repositories;
using LibraryPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static LibraryPortal.DTO.DTO;
using static LibraryPortal.Responses.ResponseDTO;
using System.IO;
using System.Net;
using LibraryPortal.DTO;

namespace   LibraryPortal.Helper
{
    public class APIHelper : ControllerBase
    {
        private readonly IStudentRepositories _student;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;



        public APIHelper(IStudentRepositories student, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _student = student;
            _userManager = userManager;
            _config = config;
        }
        public DateTime roundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
        }
        public async new Task<IActionResult> Response(string message, Level levelpara, object objcontent, ActiveErrorCode code, DateTime Startime,  HttpContext contextobj, IConfiguration configuration, BaseClass baseclass, Object RequestParameter, string id, ReturnResponse responsecode, Exception ex, bool token)
        {
            var msg = message;
            if (ex != null)
            {
                if (ex.Message != null)
                {
                    var error = ex.Message.Split(new string[] { "*-" }, StringSplitOptions.None);
                    if (!string.IsNullOrEmpty(error[0]))
                    {
                        if (error[0] == "INNER")
                        {
                            if (error[1] != null)
                            {
                                var innerError = error[1].Split(new string[] { "--|--" }, StringSplitOptions.None);
                                msg = innerError[0];
                            }
                        }
                    }
                }
            }
            var routeInfo = contextobj.GetRouteData().Values;
            string currentController = routeInfo["controller"].ToString();
            string currentAction = routeInfo["action"].ToString();

            if (currentController.Length < 4)
            {
                var actualLenght = 4 - currentController.Length;
                string addString = "";
                for (int i = 1; i <= actualLenght; i++)
                {
                    addString = addString + "C";
                }

                currentController = currentController + addString;
            }
            if (currentAction.Length < 3)
            {
                var actualLenght = 3 - currentAction.Length;
                string addString = "";
                for (int i = 1; i <= actualLenght; i++)
                {
                    addString = addString + "A";
                }
                currentAction = currentAction + addString;
            }

            var ratingCode = currentController.Substring(0, 4).ToUpper() + currentAction.Substring(0, 3).ToUpper() + "R";

            IActionResult res = null;
            ActiveResponse<object> response = new ActiveResponse<object>();
            Level obj = new Level();

            obj = levelpara;
            response.Code = code;
            Console.WriteLine("Token=" + token);
            if (token == true)
            {
               // response.Token = await Token(id, token);

            }
            else
            {
                response.Token = "";
            }
            response.Content = objcontent;
            response.Message = msg;
            if (!string.IsNullOrEmpty(id))
            {

            }
            if (ex != null)
            {
                response.ExceptionMessage = ex.InnerException == null ? ex.Message : ex.Message + " Inner Exception " + ex.InnerException.ToString();
            }

            int returnCode = Convert.ToInt32(responsecode);
            if (returnCode == 404)
            {
                res = BadRequest(new { response });
            }
            if (returnCode == 401)
            {
                res = Unauthorized(new { response });
            }
            else if (returnCode == 200)
            {
                res = Ok(new { response });
            }

            string requestResponse = JsonConvert.SerializeObject(res);
            string _request = JsonConvert.SerializeObject(RequestParameter);
           // Logs( levelpara, response.Code.ToString(), contextobj, configuration, ex, Startime, DateTime.Now, baseclass, _request, requestResponse, id);
            return res;
        }
        //public static void Logs(Level Level, string response, HttpContext contaxt, IConfiguration configuration, Exception ex, DateTime Starttime, DateTime Endtime, BaseClass baseClass, string RequestParameter, string RequestResponse, string userid)
        //{
        //    try
        //    {
        //        var exception = JsonConvert.SerializeObject(ex);
        //        //ex = null;
        //        var routeInfo = contaxt.GetRouteData().Values;
        //        string currentController = routeInfo["controller"].ToString();
        //        string currentAction = routeInfo["action"].ToString();
        //        var id = userid;
        //        var clientIp = GetIP(contaxt);
        //        string message = $"User Access this function with Id: {id}";
        //        // level level = level.Info;
        //        string except = exception;
        //        string status = response;
        //        //if (ex != null)
        //        //{
        //        //    except = ex.Message;
        //        //    level = level.Error;
        //        //}
        //        if (response == "Success")
        //            message = $"User with id:{id} successfully Get Result";
        //        if (response == "Failed")
        //            message = $"User with id:{id} was unsuccessful to Get Result";


        //        if (Level == Level.Error && Convert.ToBoolean(configuration["ErrorLog"]) == true || Level == Level.Success && Convert.ToBoolean(configuration["SuccessLog"]) == true)
        //        {
        //            WriteLogs( configuration, message, Level, id, clientIp, currentAction, exception, currentController, status, Starttime, Endtime, baseClass, RequestParameter, RequestResponse);
        //        }
        //        else
        //        {
        //            if (Convert.ToBoolean(configuration["InfoLog"]) == true)
        //            {
        //                message = $"User with id:{id} Access Function:{currentAction} and Controller:{currentController}";

        //                //except = null;
        //                WriteLogs( configuration, message, Level, id, clientIp, currentAction, exception, currentController, status, Starttime, Endtime, baseClass, RequestParameter, RequestResponse);
        //            }

        //        }
        //    }
        //    catch (Exception ex12)
        //    {

        //    }
        //}
        public static string GetIP(HttpContext contaxt)
        {
            return contaxt.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();
        }
        //public static void WriteLogs( IConfiguration configuration, string message, Level Level, string userid, string ip, string function, string Exception, string ControllerName, string status, DateTime Starttime, DateTime Endtime, BaseClass baseClass, string RequestParameter, string RequestResponse)
        //{
        //    try
        //    {
        //        configuration = null;
        //        Log model = new Log();
        //        model.ID = Guid.NewGuid().ToString();
               
        //        model.FunctionName = function;
        //        model.ControllerName = ControllerName;
        //        model.DeviceID = baseClass == null ? "" : baseClass.DeviceID;
        //        model.Browser = baseClass == null ? "" : baseClass.Browser;
        //        model.IP = ip;
        //        model.StartTime = Starttime;
        //        model.EndTime = Endtime;
        //        model.Exception = Exception;
        //        model.Message = message;
        //        model.Level = Level.ToString();
        //        model.Status = status;
        //        model.UserID = userid;
        //        model.RequestParameters = RequestParameter;
        //        model.RequestResponse =  RequestResponse;
            
        //        //change this when log updates
        //        _log.Add(model);
        //        _log.Save();


        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
        public List<string> GetErrors(ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

        }

      
    }
}
