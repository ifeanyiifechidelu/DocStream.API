using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Core.Interfaces
{
    public interface IRadioButtonServiceRepository<T> where T : class
    {

        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);

        Task Insert(T entity);

        Task Delete(int id);
    }
}
