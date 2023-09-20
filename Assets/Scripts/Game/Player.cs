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

            Debug.Log("��Ϸ��ʼ����ң�");

            Global.Days.Register(day =>
            {
                // ���������ʼʱ����ÿ�����Ĺ���
                Global.RipeAndHarvestCountInCurrentDay.Value = 0;
                Global.RipeAndHarvestRadishCountInCurrentDay.Value = 0;
                Global.HarvestCountInCurrentDay.Value = 0;
                Global.HarvestRadishCountInCurrentDay.Value = 0;

                EasyGrid<SoilData> soilDatas = FindAnyObjectByType<GridController>().ShowGrid;

                PlantController.Instance.Plants.ForEach((x, y, plant) =>
                {
                    if (plant != null)
                    {
                        // ֲ������������ؿ�����
                        plant.Grow(soilDatas[x, y]);
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
            // ���� F ��������һ��
            if (Input.GetKeyDown(KeyCode.F))
            {
                Global.Days.Value++;

                AudioController.Get.SFXNextDay.Play();
            }

            // ���ݽ�ɫ��λ�ã��õ� Tilemap �ľ����
            Vector3Int cellPos = Grid.WorldToCell(transform.position);

            EasyGrid<SoilData> grid = FindObjectOfType<GridController>().ShowGrid;

            Vector3 tileWorldPos = Grid.CellToWorld(cellPos);
            tileWorldPos.x += Grid.cellSize.x * 0.5f;
            tileWorldPos.y += Grid.cellSize.y * 0.5f;

            // ��������Ҽ�
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

            // ���»س���
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // �л���ͨ�س���
                SceneManager.LoadScene("GamePass");
            }

        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(640, 360);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("������" + Global.Days.Value, mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("���ӣ�" + Global.FruitCount.Value, mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("�ܲ���" + Global.RadishCount.Value, mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("ʹ�ù��ߣ�������", mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("�����ؿ飺����Ҽ�", mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("��һ�죺F", mLabelStyle);
            GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            //GUILayout.Space(10);
            //GUILayout.Label($"��ǰ���ߣ�{Constant.DisplayName(Global.CurrentTool.Value)}");
            //GUILayout.EndHorizontal();

            //GUI.Label(new Rect(10, 360 - 24, 200, 24), "[1] ��   [2] ��ͷ  [3] ����  [4] ����");
        }

        private void OnDestroy()
        {
            Global.Player = null;
        }
    }
}
