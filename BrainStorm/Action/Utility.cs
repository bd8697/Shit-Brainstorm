using BrainStorm.Action;
using ChartJs.Blazor.Util;
using System;
using System.Collections.Generic;
using System.Xml;


namespace BrainStorm
{
    public static class Utility
    {
        public static Dictionary<string, string> strings = new Dictionary<string, string>();
        public static Tuple<int, int> ColorSeed { get; set; }
        public static Term CurrentTerm { get; set; }
        public static int ReportsThreshold { get; set; }
        public static int BranchesPerChart { get; set; }
        public static int CurrentBranchIdx { get; set; }
        public static string StartColor { get; set; }
        public static string EndColor { get; set; }


        public static void Init()
        {
            StartColor = "placeholder";
            EndColor = "placeholder";
            ReportsThreshold = 10;
            BranchesPerChart = 10;
            CurrentBranchIdx = 0;
            LoadXML();
        }
        public static void LoadXML()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("wwwroot/Brainstorm_Strings.xml");

            foreach (XmlElement elem in xml.SelectSingleNode("BrainStorm/Strings"))
            {
                strings.Add(elem.Name.ToString(), elem.InnerText);
            }
        }

        public static string GetString(string strName)
        {
            return strings[strName];
        }

        public static void NewColorSeed()
        {
            ColorSeed = new Tuple<int, int>(RandomInt(64, 255), RandomInt(1, 6));
        }

        public static int RandomInt(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max + 1);
        }

        public static string FirstWordOf(string str)
        {
            return str.Split(' ')[0];
        }

        public static List<int> GetBranchesWeights(List<int> scores)
        {
            List<int> weights = new List<int>();
            int totalScore = 0;
            foreach (int score in scores)
            {
                totalScore += score;
            }
            foreach (int score in scores)
            {
                int weight = (100 * score) / totalScore;
                if (weight > 25) //%
                {
                    weight = 25;
                }
                else if (weight < 5)
                {
                    weight = 5;
                }

                weights.Add(weight);
            }
            return weights;
        }

        public static List<string> GenerateColors(int gradientLength, Tuple<int, int> seed)
        {
            List<byte> channel1 = new List<byte>();
            List<byte> channel2 = new List<byte>();
            List<byte> channel3 = new List<byte>();
            float ch1 = 64, ch2 = seed.Item1, ch3 = 255;

            float decay = 191f * 2 / gradientLength;
            for (int i = 0; i < gradientLength; i++)
            {
                if (ch2 < 255)
                {
                    ch2 += decay;
                    if (ch2 > 255)
                    {
                        ch2 = 255;
                    }
                } else if (ch2 == 255 && ch3 > 64)
                {
                    ch3 -= decay;
                    if (ch3 < 64)
                    {
                        ch3 = 64;
                    }
                }
                if (ch3 == 64 && ch1 < 255)
                {
                    ch1 += decay;
                }

                channel1.Add((byte)ch1);
                channel2.Add((byte)ch2);
                channel3.Add((byte)ch3);
            }

            int rndCase = seed.Item2;

            switch (rndCase)
            {
                case 1:
                    {
                        return ChannelsToColors(channel1, channel2, channel3, gradientLength);
                    }
                case 2:
                    {
                        return ChannelsToColors(channel1, channel3, channel2, gradientLength);
                    }
                case 3:
                    {
                        return ChannelsToColors(channel2, channel1, channel3, gradientLength);
                    }
                case 4:
                    {
                        return ChannelsToColors(channel2, channel3, channel1, gradientLength);
                    }
                case 5:
                    {
                        return ChannelsToColors(channel3, channel1, channel2, gradientLength);
                    }
                case 6:
                    {
                        return ChannelsToColors(channel3, channel2, channel1, gradientLength);
                    }
                default: return null;
            }
        }

        private static List<string> ChannelsToColors(List<byte> ch1, List<byte> ch2, List<byte> ch3, int gradientLength)
        {
            List<string> colors = new List<string>();
            colors.Add(Utility.GetString("myBrown")); // this should be invisible

            float darkenFactor = 1f;

            if(ch1.Count > 0)
            {
                StartColor = ColorUtil.ColorHexString((byte)(ch1[0] / darkenFactor), (byte)(ch2[0] / darkenFactor), (byte)(ch3[0] / darkenFactor));
                EndColor = ColorUtil.ColorHexString((byte)(ch1[gradientLength - 1] / darkenFactor), (byte)(ch2[gradientLength - 1] / darkenFactor), (byte)(ch3[gradientLength - 1] / darkenFactor));
            }

            for (int i = 0; i < gradientLength; i++)
            {
                colors.Add(ColorUtil.ColorHexString(ch1[i], ch2[i], ch3[i]));
            }
            return colors;
        }

        public static void TermForChart(string searchedTerm)
        {
            Term foundTerm = TermRepo.Instance.GetTerm(searchedTerm);
            Term termForChart;

            if (foundTerm == null) // term not in "database"
            {
                Console.WriteLine("term not found");

                int newId = TermRepo.Instance.GetIdForNewTerm();
                termForChart = new Term(searchedTerm, newId);
                TermRepo.Instance.AddTerm(new Term(termForChart));
            }
            else
            {
                termForChart = new Term(foundTerm);
                termForChart.RemoveBannedBranches();
                TermRepo.Instance.UpdateVisitsInTerm(termForChart.Name);
                if (termForChart.Branches.Count > 0)
                    termForChart.Branches.Sort(new Comparison<Branch>((x, y) => -x.Score.CompareTo(y.Score)));
            }

            Utility.CurrentBranchIdx = 0;
            Utility.CurrentTerm = termForChart;
        }
    }
}
