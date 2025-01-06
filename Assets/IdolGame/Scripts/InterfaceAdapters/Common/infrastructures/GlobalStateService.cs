using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnityEngine;
using ZLogger;

namespace IdolGame.Common.infrastructures;

public class GlobalStateService
{
    private readonly ILogger<GlobalStateService> logger;
    private const string SaveFilePath = "global_state.json";
    private const string IdolDataFilePath = "master_data/favorite_idol_data.json";

    public GlobalStateService(ILogger<GlobalStateService> logger)
    {
        this.logger = logger;
    }
/*
 仕様例
    async void Start()
    {
        await LoadGlobalStateAsync(ct);
    }

    void OnApplicationQuit()
    {
        await SaveGlobalStateAsync(ct);
    }
*/
    public async UniTask SaveGlobalStateAsync(CancellationToken ct)
    {
        var globalStateData = new GlobalStateData
        {
            GroupId = GlobalState.GroupId, IdolId = GlobalState.IdolId
        };
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(globalStateData, options);
        await File.WriteAllTextAsync(Path.Combine(Application.persistentDataPath, SaveFilePath), json, ct);

        logger.ZLogTrace($"グローバル状態が正常に保存されました");
    }

    public async UniTask LoadGlobalStateAsync(CancellationToken ct)
    {
        string path = Path.Combine(Application.persistentDataPath, SaveFilePath);
        if (File.Exists(path))
        {
            string json = await File.ReadAllTextAsync(path, ct);
            var globalStateData = JsonSerializer.Deserialize<GlobalStateData>(json);

            GlobalState.GroupId = globalStateData.GroupId;
            GlobalState.IdolId = globalStateData.IdolId;

            // favorite_idol_data.json から該当するデータを読み込む
            path = Path.Combine(Application.streamingAssetsPath, IdolDataFilePath);
            string idolJson = await File.ReadAllTextAsync(path, ct);
            var idolGroups = JsonSerializer.Deserialize<List<IdolGroupData>>(idolJson);

            if (idolGroups != null)
                foreach (var group in idolGroups)
                {
                    if (group.Members == null) continue;
                    foreach (var idol in group.Members)
                    {
                        if (idol.Id == GlobalState.IdolId)
                        {
                            GlobalState.GroupBackgroundImagePath = group.BackgroundImagePath;
                            GlobalState.GroupButtonUIPath = group.IdolButtonUIPath;
                            GlobalState.IdolImagePath = idol.ImagePath.ToString();
                            GlobalState.IdolColor = idol.CollarCode;
                            GlobalState.IdolSerifMenuText = idol.SerifMenuText.ToString();
                            GlobalState.IdolResultRankPoint = new List<float>
                            {
                                idol.ResultIdol.SRankPoint, idol.ResultIdol.ARankPoint, idol.ResultIdol.BRankPoint,
                                idol.ResultIdol.CRankPoint
                            };
                            GlobalState.IdolResultRankVoice = new List<string>
                            {
                                idol.ResultIdol.SRankVoice, idol.ResultIdol.ARankVoice, idol.ResultIdol.BRankVoice,
                                idol.ResultIdol.CRankVoice
                            };
                            GlobalState.IdolResultRankText = new List<string>
                            {
                                idol.ResultIdol.SRank.ToString(), idol.ResultIdol.ARank.ToString(),
                                idol.ResultIdol.BRank.ToString(), idol.ResultIdol.CRank.ToString()
                            };
                            GlobalState.IdolRewardPoint = idol.IdolReward.IdolPoint;
                        }
                    }
                }

            logger.ZLogTrace($"グローバル状態が正常に読み込まれました");
        }
    }
}