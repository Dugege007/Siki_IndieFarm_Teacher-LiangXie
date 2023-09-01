using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public partial class AudioController : ViewController, ISingleton
    {
        public static AudioController Get => MonoSingletonProperty<AudioController>.Instance;

        public void OnSingletonInit()
        {
            
        }
    }
}
