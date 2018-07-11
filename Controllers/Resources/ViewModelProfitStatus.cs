using System.Collections.Generic;

namespace BetSystem.Controllers.Resources
{
    public class ViewModelProfitStatus
    {
        public IEnumerable<TeamStatus> TeamStatusList { get; set; }
        public IEnumerable<ProfitStatus> ProfitStatusList { get; set; }
    }
}