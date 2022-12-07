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
        
        public GameMusicManager(GameManager gameManager, SoundsConfig soundsConfig, 
            GameAudioTokenProvider audioTokenProvider)
        {
            _gameManager = gameManager;
            _soundsConfig = soundsConfig;
            _audioTokenProvider = audioTokenProvider;
            
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
            PlayMusic(_soundsConfig.ambientMusic.RuntimeKey);
        }

        private void OnLowRoofHeight()
        {
            PlayMusic(_soundsConfig.lowRoofMusic);
        }

        private void OnMonsterSeenPlayer()
        {
            var token = _audioTokenProvider.GetToken((string) _soundsConfig.monsterSeenPlayerEffect.RuntimeKey,
                (string)_soundsConfig.globalAudioPreset.RuntimeKey);
        }

        private void OnGameEnd(GameTerminationReason reason)
        {
            _musicToken?.Dispose();
        }

        private void PlayMusic(object runtimeKey)
        {
            _musicToken?.Dispose();
            
            _musicToken = _audioTokenProvider
                .GetToken((string) runtimeKey, (string) _soundsConfig.globalAudioPreset.RuntimeKey);

            _musicToken.SetPosition(Vector3.zero).Loop().Play();
        }
    }
}