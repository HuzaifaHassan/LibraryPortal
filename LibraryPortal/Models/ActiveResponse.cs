using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryPortal.Models
{
    public class ActiveResponse<T>
    {
      
       
        public ActiveResponse()
        {
            Code = ActiveErrorCode.Success;
            Message = "Sucess";
        
        }
        public ActiveErrorCode Code { get; set; }
        public string Message { get; set; }
        public T Content { get; set; }
        public string? ExceptionMessage { get; set; }
        public string Token { get; set; }
    }
    public enum ActiveErrorCode
    {

        UnAuthorise = 401,

        NotExist = 12,
        Success = 1,

        NotPicked = 36,
        Dropped = 37,
        Waiting = 38,
        Reject = 39,
        Retry = 40,
        ReferToBuisness = 41,
        Approved = 42,

        InvalidEmailFormat = 2,
        InvalidMobileNumberFormat = 3,
        NotFound = 4,
        AlreadyExist = 5,
        Expired = 6,
        Failed = 7,
        ScheduledExpire = 13,
        EmailNotVerified = 8,

        UserAppliedBefore = 9,
        InvalidOTP = 10,
        ExpireOTP = 11



    }
    public enum ReturnResponse
    {
        BadRequest = 404,
        Success = 200,
        Unauthorized = 401
    }
}
