using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility
{
    public static string Getstring(object value)
    {
            return Convert.ToString(value);
    }

    public static DateTime GetDatatime(object value)
    {
        return Convert.ToDateTime(value);
    }
    public static decimal GetDecimal(object value)
    {
        return Convert.ToDecimal(value);
    }
}