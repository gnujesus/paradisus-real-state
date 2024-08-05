using Microsoft.EntityFrameworkCore;
using RealStateApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Persistence.Services
{
    public class CodeGeneration
    {
        private readonly ApplicationContext _dbContext;

        public CodeGeneration(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateUniqueCodeAsync()
        {
            string code;
            bool exists;

            do
            {
                code = GenerateRandomCode();
                exists = await CodeExistsAsync(code);
            } while (exists);

            return code;
        }

        private string GenerateRandomCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private async Task<bool> CodeExistsAsync(string code)
        {
            bool existsInProperties = await _dbContext.Properties.AnyAsync(p => p.Id == code);
            bool existsInTypeProperties = await _dbContext.TypeProperties.AnyAsync(tp => tp.Id == code);
            bool existsInTypeSales = await _dbContext.TypeSales.AnyAsync(ts => ts.Id == code);
            bool existsInFavorites = await _dbContext.Favorites.AnyAsync(ts => ts.Id == code);
            bool existsInPropertyImage = await _dbContext.Favorites.AnyAsync(ts => ts.Id == code);

            return existsInProperties || existsInTypeProperties || existsInTypeSales || existsInFavorites || existsInPropertyImage;
        }
    }
}
