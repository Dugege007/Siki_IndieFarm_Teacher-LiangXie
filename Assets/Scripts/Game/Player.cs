using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Linq;

namespace ProjectIndieFarm
{
    public partial class Player : ViewController
    {
        public Grid Grid;
        public Tilemap Tilemap;

        public Font Font;
        private GUIStyle mLabelStyle;

        private void Awake()
        {
            Global.Player = this;
        }

        private void Start()
        {
            mLabelStyle = new GUIStyle("Label")
            {
                font = Font,
            };

            Debug.Log("游戏开始（玩家）");

            Global.Days.Register(day =>
            {
                // 天数变更开始时重置每天成熟的果子
                Global.RipeAndHarvestCountInCurrentDay.Value = 0;
                Global.RipeAndHarvestRadishCountInCurrentDay.Value = 0;
                Global.HarvestCountInCurrentDay.Value = 0;
                Global.HarvestRadishCountInCurrentDay.Value = 0;

                EasyGrid<SoilData> soilDatas = FindAnyObjectByType<GridController>().ShowGrid;

                PlantController.Instance.Plants.ForEach((x, y, plant) =>
                {
                    if (plant != null)
                    {
                        // 植物生长，传入地块数据
                        plant.Grow(soilDatas[x, y]);
                    }
                });

                soilDatas.ForEach(soilData =>
                {
                    if (soilData != null)
                        soilData.Watered = false;
                });

                SceneManager.GetActiveScene()   // 获取当前场景
                    .GetRootGameObjects()   // 获取根目录下物体
                    .Where(gameObj => gameObj.name.StartsWith("Water")) // 获取名称前几个字符为“Water”的物体
                    .ForEach(water => water.DestroySelf()); // 遍历他们，并将他们销毁

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            // 按下 F 键，跳过一天
            if (Input.GetKeyDown(KeyCode.F))
            {
                Global.Days.Value++;

                AudioController.Get.SFXNextDay.Play();
            }

            // 根据角色的位置，拿到 Tilemap 的具体块
            Vector3Int cellPos = Grid.WorldToCell(transform.position);

            EasyGrid<SoilData> grid = FindObjectOfType<GridController>().ShowGrid;

            Vector3 tileWorldPos = Grid.CellToWorld(cellPos);
            tileWorldPos.x += Grid.cellSize.x * 0.5f;
            tileWorldPos.y += Grid.cellSize.y * 0.5f;

            // 按下鼠标右键
            if (Input.GetMouseButtonDown(1))
            {
                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    if (grid[cellPos.x, cellPos.y] != null)
                    {
                        // 铲除
                        // 将其消除
                        Tilemap.SetTile(cellPos, null);
                        grid[cellPos.x, cellPos.y] = null;
                    }
                }
            }

            // 按下回车键
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // 切换到通关场景
                SceneManager.LoadScene("GamePass");
            }

        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(640, 360);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("天数：" + Global.Days.Value, mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("果子：" + Global.FruitCount.Value, mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("萝卜：" + Global.RadishCount.Value, mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("使用工具：鼠标左键", mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("铲除地块：鼠标右键", mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("下一天：F", mLabelStyle);
            GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            //GUILayout.Space(10);
            //GUILayout.Label($"当前工具：{Constant.DisplayName(Global.CurrentTool.Value)}");
            //GUILayout.EndHorizontal();

            //GUI.Label(new Rect(10, 360 - 24, 200, 24), "[1] 手   [2] 锄头  [3] 种子  [4] 花洒");
        }

        private void OnDestroy()
        {
            Global.Player = null;
        }
    }
}
