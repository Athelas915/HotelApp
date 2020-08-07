using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.Implementation
{
    public class BLLActionResult : IBLLActionResult
    {
        public BLLActionResult()
        {
            ErrorMessages = new List<string>();
        }
        public bool Succeeded 
        { 
            get
            {
                if (ErrorMessages.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }    
            } 
        }

        public IList<string> ErrorMessages { get; }

        public void AddError(string err)
        {
            ErrorMessages.Add(err);
        }
    }
}
