using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Services
{
    public class ServiceResponseModel
    {
        public object Data { get; set; }
        public string ErrorString { get; set; }

        public ServiceResponseModel(object data, string errorString)
        {
            Data = data;
            ErrorString = errorString;
        }

        public ServiceResponseModel()
        {
            Data = null;
            ErrorString = null;
        }
    }
}