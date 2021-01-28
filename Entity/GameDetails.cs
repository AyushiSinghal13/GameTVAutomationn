using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTVAutomation.Entitiy
{
    public class GameDetails
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string GameUrl { get; set; }
        public string PageStatus { get; set; }
        public string TournamentsCount { get; set; }
    }
}
