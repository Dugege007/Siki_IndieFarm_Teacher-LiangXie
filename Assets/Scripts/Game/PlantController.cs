using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public enum PlantState
    {
        Seed,
        Small,
        Ripe,
        Old
    }

    public partial class PlantController : ViewController, ISingleton
    {
        public static PlantController Instance => MonoSingletonProperty<PlantController>.Instance;

        public EasyGrid<IPlant> Plants = new EasyGrid<IPlant>(10, 10);

        public void OnSingletonInit()
        {

        }
    }
}
