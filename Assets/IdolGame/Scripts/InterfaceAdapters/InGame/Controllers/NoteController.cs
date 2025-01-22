using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Data;
using IdolGame.InGame.Entities;
using IdolGame.InGame.Services;
using IdolGame.InGame.UseCases;
using UnityEngine;

namespace IdolGame.InGame.Controllers
{
    public class NoteController : MonoBehaviour
    {
        [SerializeField] GameObject notePrefab;
        private NoteUseCase noteUseCase;
        private MusicDataUseCase? musicDataUseCase;
        private List<CSVMusicData> musicData;
        private int generatedGroupsNum = 0;
        private float timer = 0.0f;
        public int bpm;
        private const int Fps = 60;

        public void InitializeWithMusicData(List<CSVMusicData> musicData,int bpm)
        {
            var musicCsvDataRepository = new MusicCsvDataRepository();
            musicDataUseCase = new MusicDataUseCase(musicCsvDataRepository);
            noteUseCase = new NoteUseCase(new NoteRepository(notePrefab), new PositionService(), new AlphaService());
            this.musicData = musicData;
            Debug.Log(this.musicData.Count);
            MusicUpdate().Forget();
        }

        // 音楽更新メソッドを非同期で実行するメソッド
        private async UniTask MusicUpdate()
        {
            while (generatedGroupsNum < musicData.Count)
            {
                timer += Time.deltaTime * (bpm / 60.0f);
                if (timer > musicData[generatedGroupsNum].Time)
                {
                    Note note = new Note
                    {
                        Time = musicData[generatedGroupsNum].Time,
                        TypeOfGroup = musicData[generatedGroupsNum].TypeOfGroup,
                        GroupInfo = musicData[generatedGroupsNum].InfoOfGroup,
                        Position = musicData[generatedGroupsNum].Position
                    };
                    await noteUseCase.GenerateNotes(note);
                    generatedGroupsNum++;
                }

                CheckAndDestroyOldNotes();
                await UniTask.Yield();
            }

            Debug.Log($"Finish generatedGroupsNum: {generatedGroupsNum} groupCount: {musicData.Count}");
        }

        // 古いノートをチェックして削除するメソッド
        private void CheckAndDestroyOldNotes()
        {
            float currentTime = Time.time;
            List<int> groupsToRemove = noteUseCase.GetGroupSpawnTimes()
                .Where(groupTime => currentTime - groupTime >= noteUseCase.NoteLifeTime * (60.0f / bpm))
                .Select(groupTime => noteUseCase.GetGroupSpawnTimes().IndexOf(groupTime)).ToList();
            foreach (var groupIndex in groupsToRemove)
            {
                noteUseCase.DestroyNotes(groupIndex);
                noteUseCase.RemoveGroupSpawnTime(groupIndex);
            }
        }
    }
}