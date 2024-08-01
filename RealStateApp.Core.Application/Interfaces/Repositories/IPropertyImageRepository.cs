using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IPropertyImageRepository : IGenericRepositoryAsync<PropertyImage>
    {
        Task AddImagesToPropertyAsync(string propertyId, List<byte[]> images);
        Task RemoveImageFromPropertyAsync(string propertyId, string imageId);
    }
}
