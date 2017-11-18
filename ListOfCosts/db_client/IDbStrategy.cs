using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.db_client
{
    public interface IDbStrategy<T>
    {
        T Create(T source);

        TResult Read<TParam, TResult>(TParam param) where TResult : class;

        IEnumerable<T> ReadAll();

        T Update(T source);
    }
}
