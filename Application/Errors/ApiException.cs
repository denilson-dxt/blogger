using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Errors;

public class ApiException:Exception
{
    public int StatusCode { get; set; } = 400;
    public object Errors { get; set; } = null;

    public ApiException(object errors)
    {
        Errors = errors;
    }
    public ApiException(int statusCode, object errors=null)
    {
        StatusCode = statusCode;
        Errors = errors;
    }
}
