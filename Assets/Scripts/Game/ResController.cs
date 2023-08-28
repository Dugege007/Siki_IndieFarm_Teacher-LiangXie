using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{

	public partial class ResController : ViewController, ISingleton
	{
        public GameObject SeedPrefab;

        public static ResController Instance => MonoSingletonProperty<ResController>.Instance;

        public void OnSingletonInit()
        {
            
        }
    }
}
