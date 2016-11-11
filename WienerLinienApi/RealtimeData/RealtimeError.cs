using System;
using System.Runtime.Serialization;

namespace WienerLinienApi.RealtimeData
{
    public class RealtimeError : Exception
    {
        public RealtimeError()
        {
        }

        public RealtimeError(RealtimeErrorCode message) : base(Enum.GetName(typeof(RealtimeErrorCode),message))
        {
        }

        public RealtimeError(RealtimeErrorCode message, Exception innerException) : base(Enum.GetName(typeof(RealtimeErrorCode), message), innerException)
        {
        }

        protected RealtimeError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public enum RealtimeErrorCode
    {
        Ok = 0,
        ServerDatabaseUnavailable = 311,            // Database not available
        ServerStopDoesNotExist = 312,               // Station does not exist
        ServerCallQuotaExceeded = 316,              // max. requests exceeded 
        ServerAuthenticationFailed = 317,           // API Key doesnt exist 
        ServerQueryStringParameterInvalid = 320,    // Parameter invalid
        ServerNoDataInDatabase = 322,               // no data in database
    }
}