using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Kumagai.Entities;
using UnityEngine;

namespace Kumagai.UseCase
{

    public class MusicDataLoader : IMusicDataLoader
    {
        public async UniTask<List<MusicDataKari>> LoadMusicDataAsync(string csvFileName)
        {
            List<MusicDataKari> dat_list = new List<MusicDataKari>();
            TextAsset csvFile = await Resources.LoadAsync<TextAsset>($"CSV/{csvFileName}") as TextAsset;
            if (csvFile == null) return dat_list;
            StringReader reader = new StringReader(csvFile.text);
            List<string[]> csvData = new List<string[]>();
            int height = 0;
            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                csvData.Add(line.Split(','));
                height++;
            }

            for (int i = 3; i < height; ++i)
            {
                var dat = new MusicDataKari
                {
                    time = float.Parse(csvData[i][0]), keepTime = float.Parse(csvData[i][1]),
                    direction = int.Parse(csvData[i][2]), type = bool.Parse(csvData[i][3])
                };
                dat_list.Add(dat);
            }

            return dat_list;
        }
    }
}