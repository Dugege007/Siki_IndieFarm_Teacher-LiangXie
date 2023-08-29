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
            Debug.Log("��Ϸ��ʼ����ң�");

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
                                // �л���Сֲ��״̬
                                plant.SetState(PlantState.Small);
                            }
                        }
                        else if (plant.State == PlantState.Small)
                        {
                            if (soilDatas[x, y].Watered)
                            {
                                // �л�������״̬
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

                SceneManager.GetActiveScene()   // ��ȡ��ǰ����
                    .GetRootGameObjects()   // ��ȡ��Ŀ¼������
                    .Where(gameObj => gameObj.name.StartsWith("Water")) // ��ȡ����ǰ�����ַ�Ϊ��Water��������
                    .ForEach(water => water.DestroySelf()); // �������ǣ�������������

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Global.Days.Value++;
            }

            // ���ݽ�ɫ��λ�ã��õ� Tilemap �ľ����
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
                TileSelectController.Instance.Hide();   // .Hide() ��װ�� gameObject.SetActive(false)
            }

            if (Input.GetMouseButtonDown(0))
            {

                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    // ���û����
                    if (grid[cellPos.x, cellPos.y] == null)
                    {
                        // ���ء�����
                        // ��������
                        Tilemap.SetTile(cellPos, FindObjectOfType<GridController>().Pen);
                        grid[cellPos.x, cellPos.y] = new SoilData();
                    }
                    // �Ѿ��и���
                    else if (grid[cellPos.x, cellPos.y].HasPlant != true)
                    {
                        // ������
                        GameObject plantObj = ResController.Instance.PlantPrefab
                            .Instantiate()
                            .Position(tileWorldPos);

                        Plant plant = plantObj.GetComponent<Plant>();
                        plant.XCell = cellPos.x;
                        plant.YCell = cellPos.y;
                        PlantController.Instance.Plants[cellPos.x, cellPos.y] = plantObj.GetComponent<Plant>();

                        grid[cellPos.x, cellPos.y].HasPlant = true;
                    }
                    // ժȡ����
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

            if (Input.GetMouseButtonDown(1))
            {
                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    if (grid[cellPos.x, cellPos.y] != null)
                    {
                        // ����
                        // ��������
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
                            // ��ˮ
                            ResController.Instance.WaterPrefab
                                .Instantiate()
                                .Position(tileWorldPos);

                            grid[cellPos.x, cellPos.y].Watered = true;
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("GamePass");
            }
        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(640, 360);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("������" + Global.Days.Value);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("���ӣ�" + Global.FruitCount.Value);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("��һ�죺F");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("��ˮ��E");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("�����ӣ�������");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("�����ؿ飺����Ҽ�");
            GUILayout.EndHorizontal();
        }
    }
}
