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

                // �л�����
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

                // ͬ���� SoilData ��
                FindObjectOfType<GridController>().ShowGrid[XCell, YCell].PlantState = newState;
            }
        }
    }
}
