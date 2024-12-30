using System.Collections.Generic;
using IdolGame;
using Microsoft.Extensions.Logging;
using ZLogger;

public class IdolPointsUpdater
{ 
    private readonly ILogger<IdolPointsUpdater> logger;

    public IdolPointsUpdater(ILogger<IdolPointsUpdater> logger)
    {
        this.logger = logger;
    }

    public void UpdateIdolPoints(IEnumerable<IdolGroupData> idolGroups, int? idolId, float pointsToAdd)
    {
        foreach (var group in idolGroups)
        {
            if (group.Members == null) continue;
            foreach (var idol in group.Members)
            {
                if (idol.Id == idolId)
                {
                    logger.ZLogTrace($"{idol.Id}");
                    var idolMembersData = idol;
                    idolMembersData = idol with
                    {
                        IdolReward = idol.IdolReward with { IdolPoint = idol.IdolReward.IdolPoint + pointsToAdd }
                    };
                    logger.ZLogTrace($"Updated IdolPoint: {idol.IdolReward.IdolPoint}");
                }
            }
        }
    }
}