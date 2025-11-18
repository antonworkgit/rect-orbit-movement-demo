using Scripts.Input;
using Scripts.Movement;
using Scripts.Path;
using Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Scripts.DI
{
    public sealed class DemoSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private DemoSetting _demoSetting;

        public override void InstallBindings()
        {
            // Singletons
            Container.Bind<IInputService>()
                .To<InputService>()
                .AsSingle();

            Container.Bind<PlayerMovementSettings>()
                .FromInstance(_demoSetting.PlayerMovementSettings)
                .AsSingle();

            Container.Bind<PathSettings>().FromInstance(_demoSetting.PathSettings)
                .AsSingle();

            // Transients
            Container.Bind<Rect>()
                .FromInstance(_demoSetting.PlatformData)
                .WhenInjectedInto<OrbPath>();

            Container.Bind<IOrbPath>().To<OrbPath>()
                .WhenInjectedInto<PlayerMovementController>();

            // Prefabs
            SpawnSceneObjects();
        }

        private void SpawnSceneObjects()
        {
            // Simple GOs for demo, no tag-components
            Container.InstantiatePrefab(_demoSetting.PlayerPrefab);

            GameObject platform = Container.InstantiatePrefab(_demoSetting.PlatformPrefab);
            platform.transform.position = _demoSetting.PlatformData.position;
            platform.transform.localScale = new Vector3(
                _demoSetting.PlatformData.size.x,
                _demoSetting.PlatformData.size.y,
                1f);
        }
    }
}