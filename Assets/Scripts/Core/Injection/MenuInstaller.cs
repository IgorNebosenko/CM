using ElectrumGames.Core.Audio;
using Zenject;

namespace CM.Core
{
    public class MenuInstaller : MonoInstaller<MenuInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<string, string, AudioToken, AudioToken.Factory>()
                .WithId("MenuSoundFactory").FromPoolableMemoryPool(x =>
                    x.WithFixedSize(10));
            Container.Bind<IAudioTokenResourceProvider>().To<GameAudioTokenProvider>().FromResolve()
                .WhenInjectedIntoWithId<AudioToken.Factory>("MenuSoundFactory");
        }
    }
}