using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Otter.Utility;

namespace PacMan.Utils
{
    public class GameUtils
    {
        private const string LevelPath = "level.oel";
        private const string ProjPath = "project.oep";

        public static OgmoProject OgmoProject { get; } = new OgmoProject(ProjPath);

        public static int[][] Map { get; } = LevelReader(LevelPath);

        private static int[][] LevelReader(string path)
        {
            var xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            string str = null;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);

            xmlnode = xmldoc.GetElementsByTagName("level");

            for (int i = 0; i <= xmlnode.Count - 1; i++)
            {
                xmlnode[i].ChildNodes.Item(0);
                str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
            }

            var res = Regex.Split(str, "\n");


            char[][] char2dArray = new char[res.Length][];

            for (int i = 0; i < res.Length; i++)
            {
                char2dArray[i] = res[i].Where(ch => ch != ',').ToArray();
            }

            int[][] levelMatrix = new int[char2dArray.Length][];

            for (int i = 0; i < char2dArray.Length; i++)
            {
                levelMatrix[i] = Array.ConvertAll(char2dArray[i], c => (int) Char.GetNumericValue(c) == 3 ? 1 : 0);
            }

            levelMatrix[1][1] = 2;
            levelMatrix[14][17] = 3;

            return levelMatrix;
        }
    }
}