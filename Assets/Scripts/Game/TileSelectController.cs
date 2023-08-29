using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class TileSelectController : ViewController, ISingleton
	{
	    public static TileSelectController Instance => MonoSingletonProperty<TileSelectController>.Instance;

        public void OnSingletonInit()
        {
            
        }
    }
}
