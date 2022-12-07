using System.Collections.Generic;
using CM.Maze.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CM.Maze
{
    public class MazeController : MonoBehaviour
    {
        [SerializeField] private MazeFragment[] mazeFragments;
        [SerializeField] private MazeFragment playerSpawn;
        [SerializeField] private MazeFragment mazeFinish;

        [SerializeField] private Transform _roof;
        
        private MazeSegmentsConfig _segmentsConfig;

        private const float EndRoofHeight = 1.6f;
        private const float DangerousRoofHeight = 3f;
        private float _startRoofHeight;

        public bool IsRoofHeightDangerous => _roof.transform.position.y < DangerousRoofHeight;
        public bool IsRoofLessMinimalLevel => _roof.transform.position.y < EndRoofHeight;

        private void Awake()
        {
            _startRoofHeight = _roof.position.y;
        }

        [Inject]
        private void Construct(MazeSegmentsConfig segmentsConfig)
        {
            _segmentsConfig = segmentsConfig;
        }

        public void DoMoveRoof(float perSecond, float deltaTime)
        {
            var targetHeight = _roof.position.y - deltaTime * perSecond;
            _roof.DOMoveY(targetHeight, deltaTime);
        }

        public Vector3 GetPlayerSpawnPoint()
        {
            var countSpawnPoints = playerSpawn.connectionData.spawnPoints.Length;

            var spawnPoint = playerSpawn.connectionData.spawnPoints[Random.Range(0, countSpawnPoints)].position;
            spawnPoint.y = 0;
            return spawnPoint;
        }

        public Vector3 GetMonsterSpawnPoint()
        {
            var listTransforms = new List<Transform>();

            for (var i = 0; i < mazeFragments.Length; i++)
                listTransforms.AddRange(mazeFragments[i].connectionData.spawnPoints);

            var spawnPoint =  listTransforms[Random.Range(0, listTransforms.Count)].position;
            spawnPoint.y = 0;
            return spawnPoint;
        }

        public void ResetController()
        {
            var roofTransform = _roof.transform;
            var position = roofTransform.position;
            position += Vector3.up * (_startRoofHeight - position.y);
            roofTransform.position = position;
            
            
        }
    }
}
