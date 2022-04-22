using System;

namespace WaracleTechnicalTest.Models
{
    public enum ProtocolVersion
    {
        Version1_5,
        Version1_6,
        Version1_7,
        Version2_0
    }

    public static class ProtocolVersionExtensions
    {
        public static string GetString(this ProtocolVersion pv)
        {
            switch (pv)
            {
                case ProtocolVersion.Version1_5:
                    return "1.5";
                case ProtocolVersion.Version1_6:
                    return "1.6";
                case ProtocolVersion.Version1_7:
                    return "1.7";
                case ProtocolVersion.Version2_0:
                    return "2.0";
                default:
                    throw new Exception("Value not supported");
            }
        }
    }
}
