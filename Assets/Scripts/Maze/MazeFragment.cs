using UnityEngine;

namespace CM.Maze
{
    public class MazeFragment : MonoBehaviour
    {
        public const int SizeX = 7;
        public const int SizeZ = 7;
        
        public MazeConnectionData connectionData = new MazeConnectionData(SizeX, SizeZ);
    }
}