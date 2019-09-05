using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [Serializable]
    public class LevelData
    {
        public bool[] accessToLevels { get; set; }
        public int[,] levelSRecords { get; set; }
        public bool gameStarted { get; set; }
        public int activeLevel { get; set; }

        public LevelData()
        {
            accessToLevels = new bool[2] { true, false };
            levelSRecords = new int[2, 10];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    levelSRecords[i, j] = 0;
                }
            }
            gameStarted = false;
            activeLevel = 1;
        }
    }
}
