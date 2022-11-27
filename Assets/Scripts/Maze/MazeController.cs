using System.Collections.Generic;
using CM.Maze.Configs;
using UnityEngine;
using Zenject;

namespace CM.Maze
{
    public class MazeController : MonoBehaviour
    {
        [SerializeField] private MazeFragment[] mazeFragments;
        [SerializeField] private MazeFragment playerSpawn;
        [SerializeField] private MazeFragment mazeFinish;
        
        private MazeSegmentsConfig _segmentsConfig;
        
        [Inject]
        private void Construct(MazeSegmentsConfig segmentsConfig)
        {
            _segmentsConfig = segmentsConfig;
        }

        public Vector3 GetPlayerSpawnPoint()
        {
            var countSpawnPoints = playerSpawn.connectionData.spawnPoints.Length;

            return playerSpawn.connectionData.spawnPoints[Random.Range(0, countSpawnPoints)].position;
        }

        public Vector3 GetMonsterSpawnPoint()
        {
            var listTransforms = new List<Transform>();

            for (var i = 0; i < mazeFragments.Length; i++)
                listTransforms.AddRange(mazeFragments[i].connectionData.spawnPoints);

            return listTransforms[Random.Range(0, listTransforms.Count)].position;

        }
    }
}
