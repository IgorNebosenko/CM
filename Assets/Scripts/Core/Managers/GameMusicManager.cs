using CM.Core.Game;
using CM.Core.Management;
using CM.GameObjects.Configs;
using ElectrumGames.Core.Audio;
using UnityEngine;

namespace CM.Core.Managers
{
    public class GameMusicManager : IManager
    {
        private GameManager _gameManager;
        private SoundsConfig _soundsConfig;
        private GameAudioTokenProvider _audioTokenProvider;

        private AudioToken _musicToken;
        private AudioToken _roofLowLevelToken;
        
        public GameMusicManager(GameManager gameManager, SoundsConfig soundsConfig, 
            GameAudioTokenProvider audioTokenProvider)
        {
            _gameManager = gameManager;
            _soundsConfig = soundsConfig;
            _audioTokenProvider = audioTokenProvider;
            
            OnGameStarted();
            SubscribeEvents();
        }
        
        public void Destroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _gameManager.GameStarted += OnGameStarted;
            _gameManager.LowRoofHeight += OnLowRoofHeight;
            _gameManager.MonsterSeenPlayer += OnMonsterSeenPlayer;
            _gameManager.GameEnded += OnGameEnd;
        }

        private void UnsubscribeEvents()
        {
            _gameManager.GameStarted -= OnGameStarted;
            _gameManager.LowRoofHeight -= OnLowRoofHeight;
            _gameManager.MonsterSeenPlayer -= OnMonsterSeenPlayer;
            _gameManager.GameEnded -= OnGameEnd;
        }

        private void OnGameStarted()
        {
            _musicToken = _audioTokenProvider.GetToken((string) _soundsConfig.ambientMusic.RuntimeKey, 
                (string) _soundsConfig.globalAudioPreset.RuntimeKey);

            _musicToken.SetPosition(Vector3.zero).Loop().Play();
        }

        private void OnLowRoofHeight()
        {
            _roofLowLevelToken = _audioTokenProvider.GetToken((string) _soundsConfig.lowRoofMusic.RuntimeKey, 
                (string) _soundsConfig.globalAudioPreset.RuntimeKey);

            _roofLowLevelToken.SetPosition(Vector3.zero).Loop().Play();
        }

        private void OnMonsterSeenPlayer()
        {
            var token = _audioTokenProvider.GetToken((string) _soundsConfig.monsterSeenPlayerEffect.RuntimeKey,
                (string)_soundsConfig.globalAudioPreset.RuntimeKey);
            
            token.SetPosition(Vector3.zero).Play();
        }

        private void OnGameEnd(GameTerminationReason reason)
        {
            _musicToken?.Dispose();
            _roofLowLevelToken?.Dispose();
        }
        
    }
}