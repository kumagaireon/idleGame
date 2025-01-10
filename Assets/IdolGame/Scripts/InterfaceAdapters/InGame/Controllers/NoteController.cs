using System.Collections.Generic;
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
        private NoteUseCase _noteUseCase;
        private MusicDataUseCase _musicDataUseCase;
        private List<CSVMusicData> _musicData;
        private int _generatedGroupsNum = 0;
        private float _timer = 0.0f;
        private int _bpm;
        private const int Fps = 60;

        public void InitializeWithMusicData(List<CSVMusicData> musicData)
        {
            _musicDataUseCase = new MusicDataUseCase(new MusicDataRepository());
            _noteUseCase = new NoteUseCase(new NoteRepository(notePrefab), new PositionService(), new AlphaService());
            _musicData = musicData;
            _bpm = _musicDataUseCase.GetBpm();
            Debug.Log(_bpm);
            MusicUpdate().Forget();
        }


        private async UniTask MusicUpdate()
        {
            while (_generatedGroupsNum < _musicData.Count)
            {
                _timer += Time.deltaTime * (_bpm / 60.0f);
                if (_timer > _musicData[_generatedGroupsNum].Time)
                {
                    Note note = new Note
                    {
                        Time = _musicData[_generatedGroupsNum].Time,
                        TypeOfGroup = _musicData[_generatedGroupsNum].TypeOfGroup,
                        InfoOfGroup = _musicData[_generatedGroupsNum].InfoOfGroup,
                        Position = _musicData[_generatedGroupsNum].Position
                    };
                    await _noteUseCase.GenerateNotes(note);
                    _generatedGroupsNum++;
                }

                CheckAndDestroyOldNotes();
                await UniTask.Yield();
            }

            Debug.Log($"Finish generatedGroupsNum: {_generatedGroupsNum} groupCount: {_musicData.Count}");
        }

        private void CheckAndDestroyOldNotes()
        {
            float currentTime = Time.time;
            List<int> groupsToRemove = new List<int>();
            foreach (var groupTime in _noteUseCase.GetGroupSpawnTimes())
            {
                if (currentTime - groupTime >= _noteUseCase.NoteLifeTime * (60.0f / _bpm))
                {
                    int groupNum = _noteUseCase.GetGroupSpawnTimes().IndexOf(groupTime);
                    if (groupNum < _noteUseCase.GetGroupList().Count)
                    {
                        _noteUseCase.DestroyNotes(groupNum);
                    }

                    groupsToRemove.Add(groupNum);
                }
            }

            foreach (var groupIndex in groupsToRemove)
            {
                _noteUseCase.RemoveGroupSpawnTime(groupIndex);
            }
        }
    }
}