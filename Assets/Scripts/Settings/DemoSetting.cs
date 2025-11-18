using UnityEngine;

namespace Scripts.Settings
{
    [CreateAssetMenu(fileName = "DemoSetting", menuName = "Demo/Setting")]
    public sealed class DemoSetting : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public GameObject PlatformPrefab;

        public float PlayerInitialPosition;

        public PlayerMovementSettings PlayerMovementSettings;
        public PathSettings PathSettings;
        public Rect PlatformData;
    }
}