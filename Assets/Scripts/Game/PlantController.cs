using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public enum PlantState
    {
        Seed,
        Small,
        Ripe
    }

    public partial class PlantController : ViewController, ISingleton
    {
        public static PlantController Instance => MonoSingletonProperty<PlantController>.Instance;

        public EasyGrid<Plant> Plants = new EasyGrid<Plant>(10, 10);

        public void OnSingletonInit()
        {

        }
    }
}
