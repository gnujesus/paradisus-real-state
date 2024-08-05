using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public class CodeGeneratorService
    {
        private readonly ICodeGeneratorAsync _codeGeneratorAsync;

        public CodeGeneratorService(ICodeGeneratorAsync codeGeneratorAsync)
        {
            _codeGeneratorAsync = codeGeneratorAsync; 
        }

        public async Task<string> GenerateUniqueCodeAsync()
        {
            var code = await _codeGeneratorAsync.GenerateUniqueCodeAsync();

            return code;
        }

       
    }
}
