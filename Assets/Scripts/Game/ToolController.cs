using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace ProjectIndieFarm
{
    public partial class ToolController : ViewController
    {
        private Grid mGrid;
        private GridController mGridController;
        private EasyGrid<SoilData> mShowGrid;
        private Camera mMainCamera;
        private SpriteRenderer mSprite;
        private Tilemap mTilemap;

        private Vector3 worldMousePos;

        private void Awake()
        {
            Global.Mouse = this;
        }

        private void Start()
        {
            mGridController = FindObjectOfType<GridController>();
            mShowGrid = mGridController.ShowGrid;
            mGrid = mGridController.GetComponent<Grid>();
            mMainCamera = Camera.main;
            mTilemap = mGridController.Tilemap;

            mSprite = GetComponent<SpriteRenderer>();
            mSprite.enabled = false;
        }

        private void Update()
        {
            // 获取玩家所在单元格的坐标
            Vector3 playerCellPos = mGrid.WorldToCell(Global.Player.Position());
            // 获得鼠标在游戏场景中的坐标
            worldMousePos = mMainCamera.ScreenToWorldPoint(Input.mousePosition);
            // 获取当前单元格的坐标
            Vector3Int cellPos = mGrid.WorldToCell(worldMousePos);

            mSprite.enabled = false;

            // 判断相对玩家的位置
            if (// 左上
                cellPos.x - playerCellPos.x == -1 && cellPos.y - playerCellPos.y == 1 ||
                // 中上
                cellPos.x - playerCellPos.x == 0 && cellPos.y - playerCellPos.y == 1 ||
                // 右上
                cellPos.x - playerCellPos.x == 1 && cellPos.y - playerCellPos.y == 1 ||
                // 左
                cellPos.x - playerCellPos.x == -1 && cellPos.y - playerCellPos.y == 0 ||
                // 中
                cellPos.x - playerCellPos.x == 0 && cellPos.y - playerCellPos.y == 0 ||
                // 右
                cellPos.x - playerCellPos.x == 1 && cellPos.y - playerCellPos.y == 0 ||
                // 左下
                cellPos.x - playerCellPos.x == -1 && cellPos.y - playerCellPos.y == -1 ||
                // 中下
                cellPos.x - playerCellPos.x == 0 && cellPos.y - playerCellPos.y == -1 ||
                // 右下
                cellPos.x - playerCellPos.x == 1 && cellPos.y - playerCellPos.y == -1)
            {
                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    // 如果没耕地
                    if (mShowGrid[cellPos.x, cellPos.y] == null &&
                        Global.CurrentTool.Value == Constant.TOOL_HOE)
                    {
                        ShowSelect(cellPos);

                        // 耕地、开垦
                        if (Input.GetMouseButtonDown(0))
                        {
                            // 设置数据
                            mTilemap.SetTile(cellPos, mGridController.Pen);
                            mShowGrid[cellPos.x, cellPos.y] = new SoilData();

                            AudioController.Get.SFXShoveDig.Play();
                        }
                    }
                    else if (mShowGrid[cellPos.x, cellPos.y] != null &&
                        mShowGrid[cellPos.x, cellPos.y].HasPlant != true &&
                        Global.CurrentTool.Value == Constant.TOOL_SEED)
                    {
                        Vector3 gridCenterPos = ShowSelect(cellPos);

                        // 放种子
                        if (Input.GetMouseButtonDown(0))
                        {
                            GameObject plantObj = ResController.Instance.PlantPrefab
                                .Instantiate()
                                .Position(gridCenterPos);

                            Plant plant = plantObj.GetComponent<Plant>();
                            plant.XCell = cellPos.x;
                            plant.YCell = cellPos.y;
                            PlantController.Instance.Plants[cellPos.x, cellPos.y] = plantObj.GetComponent<Plant>();

                            mShowGrid[cellPos.x, cellPos.y].HasPlant = true;

                            AudioController.Get.SFXPutSeed.Play();
                        }
                    }
                    else if (mShowGrid[cellPos.x, cellPos.y] != null &&
                        mShowGrid[cellPos.x, cellPos.y].Watered != true &&
                        Global.CurrentTool.Value == Constant.TOOL_WATERING_SCAN)
                    {
                        Vector3 gridCenterPos = ShowSelect(cellPos);

                        // 浇水
                        if (Input.GetMouseButtonDown(0))
                        {
                            ResController.Instance.WaterPrefab
                                .Instantiate()
                                .Position(gridCenterPos);

                            mShowGrid[cellPos.x, cellPos.y].Watered = true;

                            AudioController.Get.SFXWater.Play();
                        }

                    }
                    // 已结果
                    else if (mShowGrid[cellPos.x, cellPos.y] != null &&
                        mShowGrid[cellPos.x, cellPos.y].HasPlant &&
                        mShowGrid[cellPos.x, cellPos.y].PlantState == PlantState.Ripe &&
                        Global.CurrentTool.Value == Constant.TOOL_HAND)
                    {
                        ShowSelect(cellPos);

                        // 摘取果子
                        if (Input.GetMouseButtonDown(0))
                        {
                            Global.OnPlantHarvest.Trigger(PlantController.Instance.Plants[cellPos.x, cellPos.y]);

                            Global.HarvestCountInCurrentDay.Value++;

                            // 摘取果子
                            Destroy(PlantController.Instance.Plants[cellPos.x, cellPos.y].gameObject);
                            mShowGrid[cellPos.x, cellPos.y].HasPlant = false;
                            Global.FruitCount.Value++;

                            AudioController.Get.SFXHarvest.Play();
                        }
                    }
                }
            }
            else
            {
                mSprite.enabled = false;
            }
        }

        private void LateUpdate()
        {
            Icon.Position(worldMousePos.x + 0.4f, worldMousePos.y - 0.4f);
        }

        private void OnDestroy()
        {
            Global.Mouse = null;
        }

        private Vector3 ShowSelect(Vector3Int cellPos)
        {
            // 拿到格子左下角坐标点
            Vector3 gridOriginPos = mGrid.CellToWorld(cellPos);
            // 拿到格子的中心点
            Vector3 gridCenterPos = gridOriginPos + mGrid.cellSize * 0.5f;
            transform.Position(gridCenterPos.x, gridCenterPos.y);
            mSprite.enabled = true;

            return gridCenterPos;
        }
    }
}
