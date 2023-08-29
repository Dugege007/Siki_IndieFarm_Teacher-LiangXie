using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public partial class Plant : ViewController
    {
        public int XCell;
        public int YCell;

        private PlantState mState = PlantState.Seed;
        public PlantState State => mState;

        public void SetState(PlantState newState)
        {
            if (newState != mState)
            {
                mState = newState;

                // 切换表现
                switch (newState)
                {
                    case PlantState.Seed:
                        GetComponent<SpriteRenderer>().sprite = ResController.Instance.SeedSprite;
                        break;

                    case PlantState.Small:
                        GetComponent<SpriteRenderer>().sprite = ResController.Instance.SmallPlantSprite;
                        break;

                    case PlantState.Ripe:
                        GetComponent<SpriteRenderer>().sprite = ResController.Instance.RipeSprite;
                        break;

                    default:
                        GetComponent<SpriteRenderer>().sprite = null;
                        break;
                }

                // 同步到 SoilData 中
                FindObjectOfType<GridController>().ShowGrid[XCell, YCell].PlantState = newState;
            }
        }
    }
}
