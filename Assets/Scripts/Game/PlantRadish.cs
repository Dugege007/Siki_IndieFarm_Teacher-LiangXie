using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public partial class PlantRadish : ViewController, IPlant
    {
        public int XCell;
        public int YCell;

        private PlantState mState = PlantState.Seed;
        public PlantState State => mState;

        public GameObject GameObject => gameObject;

        /// <summary>
        /// 成熟的日期（第几天）
        /// </summary>
        public int RipeDay = -1;

        public void SetState(PlantState newState)
        {
            if (newState != mState)
            {
                if (mState == PlantState.Small && newState == PlantState.Ripe)
                {
                    RipeDay = Global.Days.Value;
                }

                mState = newState;

                // 切换表现
                switch (newState)
                {
                    case PlantState.Seed:
                        GetComponent<SpriteRenderer>().sprite = ResController.Instance.SeedRadishSprite;
                        break;

                    case PlantState.Small:
                        GetComponent<SpriteRenderer>().sprite = ResController.Instance.SmallSprite;
                        break;

                    case PlantState.Ripe:
                        GetComponent<SpriteRenderer>().sprite = ResController.Instance.RipeSprite;
                        break;

                    case PlantState.Old:
                        GetComponent<SpriteRenderer>().sprite = ResController.Instance.OldSprite;
                        break;

                    default:
                        GetComponent<SpriteRenderer>().sprite = null;
                        break;
                }

                // 同步到 SoilData 中
                FindObjectOfType<GridController>().ShowGrid[XCell, YCell].PlantState = newState;
            }
        }

        private int mSmallStateDay = 0;

        public void Grow(SoilData soilData)
        {
            if (State == PlantState.Seed)
            {
                if (soilData.Watered)
                {
                    // 切换到小植物状态
                    SetState(PlantState.Small);
                }
            }
            else if (State == PlantState.Small)
            {
                if (soilData.Watered)
                {
                    mSmallStateDay++;

                    if (mSmallStateDay == 2)
                    {
                        // 切换到成熟状态
                        SetState(PlantState.Ripe);
                    }
                }
            }
        }
    }
}
