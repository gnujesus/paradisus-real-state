using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.FavoritesModels
{
    public class FavoritesViewModel
    {
        public string Id { get; set; }
        public string User_Id { get; set; }
        public string Property_Id { get; set; }
        public Property Property { get; set; }
    }
}
