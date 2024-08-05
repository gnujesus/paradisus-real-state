using RealStateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface ICodeGeneratorService
    {
        Task<string> GenerateUniqueCodeAsync();
    }
}
