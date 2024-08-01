using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.UserModels
{
    public class UserStatisticsViewModel
    {
        public int ActiveClientsCount { get; set; }
        public int InactiveClientsCount { get; set; }
        public int ActiveAgentsCount { get; set; }
        public int InactiveAgentsCount { get; set; }
        public int ActiveDevelopersCount { get; set; }
        public int InactiveDevelopersCount { get; set; }
    }
}
