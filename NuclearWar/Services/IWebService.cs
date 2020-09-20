using NuclearWar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearWar.Services
{
    public interface IWebService
    {
        Task<IEnumerable<Capital>> GetCapitalsAsync();
    }
}
