using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public struct MusicData
    {
        public float time;
        public float keepTime;
        public int type;
    }

    public static List<MusicData> data = new List<MusicData>();

    public List<MusicData> Music_CSV()
    {
        //ˆê“ü—Í—p‚Å–ˆ‰ñ‰Šú‰»‚·‚é
        MusicData dat = new MusicData();
        List<MusicData> dat_list = new List<MusicData>();

        //Resources‚©‚çcsv‚ğ“Ç‚İ‚Ş‚Ì‚É•K—v
        TextAsset csvFile;

        //“Ç‚İ‚ñ‚¾csv‚ğŠi”[
        List<string[]> csvData = new List<string[]>();

        //csv‚Ìs”‚ğŠi”[
        int height = 0;

        csvFile = Resources.Load("CSV/musicData") as TextAsset;   
        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();

            //,‚Å‹æØ‚Á‚Äcsv‚ÉŠi”[
            csvData.Add(line.Split(','));
            height++;
        }

        for(int i = 1; i < height; ++i)
        {
            dat.time = Convert.ToSingle(csvData[i][0]);
            dat.keepTime = Convert.ToSingle(csvData[i][1]);
            dat.type = int.Parse(csvData[i][2]);

            dat_list.Add(dat);
            Debug.Log(dat.time);
            Debug.Log(dat.keepTime);
            Debug.Log(dat.type);
        }
        return dat_list;
    }

    // Start is called before the first frame update
    void Start()
    {
        data = Music_CSV();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
