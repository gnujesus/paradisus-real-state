﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface ICodeGeneratorAsync
    {
        Task<string> GenerateUniqueCodeAsync();
    }
}
