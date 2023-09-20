using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public interface IPlant
    {
        GameObject GameObject { get; }
        PlantState State { get; }
        int RipeDay { get; }

        void SetState(PlantState newState);
        void Grow(SoilData soilData);
    }

    public partial class Plant : ViewController, IPlant
    {
        public int XCell;
        public int YCell;

        private PlantState mState = PlantState.Seed;
        public PlantState State => mState;

        public GameObject GameObject => gameObject;

        /// <summary>
        /// 成熟的日期（第几天）
        /// </summary>
        public int RipeDay { get; private set; }

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
                        GetComponent<SpriteRenderer>().sprite = ResController.Instance.SeedSprite;
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
                    // 切换到成熟状态
                    SetState(PlantState.Ripe);
                }
            }
        }
    }
}
