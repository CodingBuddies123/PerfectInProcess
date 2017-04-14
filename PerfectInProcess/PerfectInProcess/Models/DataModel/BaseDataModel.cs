using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectInProcess.Models.DataModel
{
    public class BaseDataModel
    {
        List<String> Errors = new List<string>();

        protected void SetError(string error)
        {
            Errors.Add(error);
        }

        public List<string> GetError()
        {
            return Errors;
        }

        public void ClearError()
        {
            Errors.Clear();
        }

    }
}