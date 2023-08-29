using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{

	public partial class ResController : ViewController, ISingleton
	{
        public GameObject WaterPrefab;
        public GameObject PlantPrefab;

        public Sprite SeedSprite;
        public Sprite SmallSprite;
        public Sprite RipeSprite;
        public Sprite OldSprite;

        public static ResController Instance => MonoSingletonProperty<ResController>.Instance;

        public void OnSingletonInit()
        {
            
        }
    }
}
