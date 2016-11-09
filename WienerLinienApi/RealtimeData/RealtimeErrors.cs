namespace WienerLinienApi.RealtimeData
{
    internal class RealtimeErrors
    {

    }
    public enum RealtimeErrorCode
    {
        ServerDatabaseUnavailable = 311,            // Database not available
        ServerStopDoesNotExist = 312,               // Station does not exist
        ServerCallQuotaExceeded = 316,              // max. requests exceeded 
        ServerAuthenticationFailed = 317,           // API Key doesnt exist 
        ServerQueryStringParameterInvalid = 320,    // Parameter invalid
        ServerNoDataInDatabase = 322,               // no data in database
    }
}
