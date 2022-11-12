using CM.Maze;
using UnityEngine;

namespace CM.Configs
{
    [CreateAssetMenu(menuName = "Configs/MazeSegments", fileName = "MazeSegments")]
    public class MazeSegmentsConfig : ScriptableObject
    {
        public MazeFragment playerSpawn;
        public MazeFragment playerFinish;

        public MazeFragment[] segments;
    }
}
