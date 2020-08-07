using System.Collections.Generic;

namespace HotelApp.BLL
{
    public interface IBLLActionResult
    {
        bool Succeeded { get; }
        IList<string> ErrorMessages { get; }
        void AddError(string err);
    }
}
