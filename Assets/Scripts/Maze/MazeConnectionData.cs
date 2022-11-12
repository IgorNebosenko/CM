using System;
using UnityEngine;

namespace CM.Maze
{
    [Serializable]
    public class MazeConnectionData
    {
        public bool[] upConnection;
        public bool[] rightConnection;
        public bool[] downConnection;
        public bool[] leftConnection;

        public Transform[] spawnPoints;
    }
}