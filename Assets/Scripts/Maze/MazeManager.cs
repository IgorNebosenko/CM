using CM.Core.Management;
using UnityEngine;
using Zenject;

namespace CM.Maze
{
    public class MazeManager : IFixedUpdateManager
    {
        [Inject] private MazeController _mazeController;
        
        public void Simulate(float deltaTime)
        {
            throw new System.NotImplementedException();
        }
        
        public void Destroy()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            
        }

        public Vector3 GetPlayerSpawnPoint()
        {
            return _mazeController.GetPlayerSpawnPoint();
        }

        public Quaternion GetPlayerRotation()
        {
            return Quaternion.identity;
        }

        public Vector3 GetMonsterSpawnPoint()
        {
            return _mazeController.GetMonsterSpawnPoint();
        }
    }
}