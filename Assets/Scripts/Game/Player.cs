using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;
using HutongGames.PlayMaker.Actions;
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
                var seeds = SceneManager.GetActiveScene()   // ��ȡ��ǰ����
                    .GetRootGameObjects()   // ��ȡ��Ŀ¼������
                    .Where(gameObj => gameObj.name.StartsWith("Seed")); // ��ȡ����ǰ�����ַ�Ϊ��Seed��������

                seeds.ForEach(seed =>
                {
                    Vector3Int tilePos = Grid.WorldToCell(seed.transform.position);

                    SoilData tileData = FindObjectOfType<GridController>().ShowGrid[tilePos.x, tilePos.y];

                    if (tileData != null && tileData.Watered)
                    {
                        ResController.Instance.SmallPlantPrefab.Instantiate()   // ����Сֲ��
                            .Position(seed.transform.position); // ������λ��

                        seed.DestroySelf();
                    }
                });

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

            if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
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
                        ResController.Instance.SeedPrefab
                            .Instantiate()
                            .Position(tileWorldPos);

                        grid[cellPos.x, cellPos.y].HasPlant = true;
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
        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(640, 360);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("������" + Global.Days.Value);
            GUILayout.EndHorizontal();
        }
    }
}
