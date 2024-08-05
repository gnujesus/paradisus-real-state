namespace RealStateApp.Core.Application.DataTransferObjects.UserDTOs
{
    public class UserStatisticsDTO
    {
        public int ActiveClientsCount { get; set; }
        public int InactiveClientsCount { get; set; }
        public int ActiveAgentsCount { get; set; }
        public int InactiveAgentsCount { get; set; }
        public int ActiveDevelopersCount { get; set; }
        public int InactiveDevelopersCount { get; set; }
    }
}
