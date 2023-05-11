using System;

namespace Application.Common;

public static class Constants
{
    public static class ErrorMessages
    {
        public const string GENERIC_ERROR = "Generic error";
        public const string SHIP_NOTFOUND = "Ship not found";
        public const string SHIP_LOCATION_NOTFOUND = "Cannot find closest port because missing ship location";
        public const string CLOSEST_PORT_NOTFOUND = "Cannot find any closest port";
        
    }
}