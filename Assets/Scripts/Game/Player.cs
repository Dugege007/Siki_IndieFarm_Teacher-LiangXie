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

        private void Start()
        {
            Debug.Log("游戏开始（玩家）");

            Global.Days.Register(day =>
            {
                EasyGrid<SoilData> soilDatas = FindAnyObjectByType<GridController>().ShowGrid;

                PlantController.Instance.Plants.ForEach((x, y, plant) =>
                {
                    if (plant != null)
                    {
                        if (plant.State == PlantState.Seed)
                        {
                            if (soilDatas[x, y].Watered)
                            {
                                // 切换到小植物状态
                                plant.SetState(PlantState.Small);
                            }
                        }
                        else if (plant.State == PlantState.Small)
                        {
                            if (soilDatas[x, y].Watered)
                            {
                                // 切换到成熟状态
                                plant.SetState(PlantState.Ripe);
                            }
                        }
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
            }

            // 根据角色的位置，拿到 Tilemap 的具体块
            Vector3Int cellPos = Grid.WorldToCell(transform.position);

            EasyGrid<SoilData> grid = FindObjectOfType<GridController>().ShowGrid;

            Vector3 tileWorldPos = Grid.CellToWorld(cellPos);
            tileWorldPos.x += Grid.cellSize.x * 0.5f;
            tileWorldPos.y += Grid.cellSize.y * 0.5f;

            if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
            {
                TileSelectController.Instance.Position(tileWorldPos);
                TileSelectController.Instance.Show();
            }
            else
            {
                TileSelectController.Instance.Hide();   // .Hide() 封装了 gameObject.SetActive(false)
            }

            // 按下鼠标左键
            if (Input.GetMouseButtonDown(0))
            {

                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    // 如果没耕地
                    if (grid[cellPos.x, cellPos.y] == null && Global.CurrentToolName.Value == "锄头")
                    {
                        // 耕地、开垦
                        // 设置数据
                        Tilemap.SetTile(cellPos, FindObjectOfType<GridController>().Pen);
                        grid[cellPos.x, cellPos.y] = new SoilData();
                    }

                    return;

                    // 已经有耕地
                    if (grid[cellPos.x, cellPos.y].HasPlant != true)
                    {
                        // 放种子
                        GameObject plantObj = ResController.Instance.PlantPrefab
                            .Instantiate()
                            .Position(tileWorldPos);

                        Plant plant = plantObj.GetComponent<Plant>();
                        plant.XCell = cellPos.x;
                        plant.YCell = cellPos.y;
                        PlantController.Instance.Plants[cellPos.x, cellPos.y] = plantObj.GetComponent<Plant>();

                        grid[cellPos.x, cellPos.y].HasPlant = true;
                    }
                    // 摘取果子
                    else if (grid[cellPos.x, cellPos.y].HasPlant)
                    {
                        if (grid[cellPos.x, cellPos.y].PlantState == PlantState.Ripe)
                        {
                            Destroy(PlantController.Instance.Plants[cellPos.x, cellPos.y].gameObject);
                            grid[cellPos.x, cellPos.y].HasPlant = false;
                            Global.FruitCount.Value++;
                        }
                    }

                }
            }

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

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    if (grid[cellPos.x, cellPos.y] != null)
                    {
                        if (grid[cellPos.x, cellPos.y].Watered != true)
                        {
                            // 浇水
                            ResController.Instance.WaterPrefab
                                .Instantiate()
                                .Position(tileWorldPos);

                            grid[cellPos.x, cellPos.y].Watered = true;
                        }
                    }
                }
            }

            // 按下回车键
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // 切换到通关场景
                SceneManager.LoadScene("GamePass");
            }

            // 按下数字 1 键
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Global.CurrentToolName.Value = "手";
            }

            // 按下数字 2 键
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Global.CurrentToolName.Value = "锄头";
            }
        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(640, 360);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("天数：" + Global.Days.Value);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("果子：" + Global.FruitCount.Value);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("撒种子：鼠标左键");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("铲除地块：鼠标右键");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("浇水：E");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("下一天：F");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label($"当前工具：{Global.CurrentToolName.Value}");
            GUILayout.EndHorizontal();

            GUI.Label(new Rect(10, 360 - 24, 200, 24), "[1] 手   [2] 锄头");
        }
    }
}
