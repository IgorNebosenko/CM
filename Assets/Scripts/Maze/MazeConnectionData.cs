using System;
using UnityEngine;

namespace CM.Maze
{
    [Serializable]
    public class MazeConnectionData
    {
        public MazeLineData[] data;

        public Transform[] spawnPoints;
        public Transform[] monsterMovementTargets;

        public MazeConnectionData(int sizeX, int sizeZ)
        {
            data = new MazeLineData[sizeX];

            for (var i = 0; i < sizeX; i++)
                data[i] = new MazeLineData(sizeZ);
        }
    }

    [Serializable]
    public class MazeLineData
    {
        public bool[] lineData;

        public MazeLineData(int size)
        {
            lineData = new bool[size];
        }
    }
}