using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace ProjectIndieFarm
{
    public partial class Player : ViewController
    {
        public Grid Grid;
        public Tilemap Tilemap;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
            {
                // ���ݽ�ɫ��λ�ã��õ� Tilemap �ľ����
                Vector3Int cellPos = Grid.WorldToCell(transform.position);

                var grid = FindObjectOfType<GridController>().ShowGrid;

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
                        Vector3 tileWorldPos = Grid.CellToWorld(cellPos);
                        tileWorldPos.x += Grid.cellSize.x * 0.5f;
                        tileWorldPos.y += Grid.cellSize.y * 0.5f;

                        ResController.Instance.SeedPrefab
                            .Instantiate()
                            .Position(tileWorldPos);

                        grid[cellPos.x, cellPos.y].HasPlant = true;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                // ���ݽ�ɫ��λ�ã��õ� Tilemap �ľ����
                Vector3Int cellPos = Grid.WorldToCell(transform.position);

                var grid = FindObjectOfType<GridController>().ShowGrid;

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
        }
    }
}
