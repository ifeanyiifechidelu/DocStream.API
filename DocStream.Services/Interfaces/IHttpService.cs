using DocStream.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Services.Interfaces
{
    public interface IHttpService
    {
        Task<T> SendPostRequest<T, U>(JsonContentPostRequest<U> request);
    }
}
