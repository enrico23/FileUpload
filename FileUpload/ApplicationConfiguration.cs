using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FileUpload
{
    public class ApplicationConfiguration
    {
        public string maxFileSize
        {
            get
            {
                return ConfigurationManager.AppSettings["maxFileSize"];
            }
        }
    }
}