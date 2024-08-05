using Microsoft.EntityFrameworkCore;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Services
{
    internal static class CodeGenerator
    {
        public static async Task<string> GenerateUniqueCodeAsync(ApplicationContext context)
        {
            string code;
            bool exists;

            do
            {
                code = GenerateRandomCode();
                exists = await CodeExistsAsync(context, code);
            } while (exists);

            return code;
        }

        private static string GenerateRandomCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private static async Task<bool> CodeExistsAsync(ApplicationContext context, string code)
        {
            bool existsInProperties = await context.Properties.AnyAsync(p => p.Id == code);
            bool existsInTypeProperties = await context.TypeProperties.AnyAsync(tp => tp.Id == code);
            bool existsInTypeSales = await context.TypeSales.AnyAsync(ts => ts.Id == code);
            bool existsInFavorites = await context.Favorites.AnyAsync(ts => ts.Id == code);
            bool existsInPropertyImage = await context.Favorites.AnyAsync(ts => ts.Id == code);

            return existsInProperties || existsInTypeProperties || existsInTypeSales || existsInFavorites || existsInPropertyImage;
        }
    }
}
