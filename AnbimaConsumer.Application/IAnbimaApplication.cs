using AnbimaConsumer.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnbimaConsumer.Application
{
    public interface IAnbimaApplication
    {
        Task<IEnumerable<Anbima>> GetByDate(DateTime date);
    }
}