using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Word_Cloud.Models;

namespace Word_Cloud.Service
{
    class WordCloudUtility
    {
        public static SortedDictionary<string, int> GetMapFromString(string textBody)
        {
            SortedDictionary<string, int> NewMap = new SortedDictionary<string, int>();
            string[] words = textBody.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {

                if (String.IsNullOrWhiteSpace(words[i]))
                {
                    continue;
                }

                if (NewMap.ContainsKey(words[i]))
                {
                    NewMap[words[i]]++;
                }
                else
                {
                    NewMap[words[i]] = 1;
                }
            }
            return NewMap;
        }
        public static List<DisplayWordModel> FormatWordsForCanvas(SortedDictionary<string,int> UnformattedWords)
        {
            Random r = new Random();
            int maxWidth = 790;
            int maxHeight = 340;
            int midX = maxWidth / 2;
            int midY = maxHeight / 2;
            int distance = 1;
            double deg = 0;
            List<DisplayWordModel> FormattedWords = new List<DisplayWordModel>();
            foreach(string Word in UnformattedWords.Keys){
                deg += 10 % 360;
                int x = (int)(midX+distance*Math.Cos(deg*Math.PI/180.0));
                int y = (int)(midY+distance * Math.Sin(deg * Math.PI / 180.0));
                distance+=3;
                DisplayWordModel d = new DisplayWordModel(Word,UnformattedWords[Word],x,y,5);
                FormattedWords.Add(d);
                Debug.WriteLine(d.Text.ToString());
            }
            return FormattedWords;
        }
    }
}
