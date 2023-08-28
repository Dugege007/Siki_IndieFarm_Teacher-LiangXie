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
                // 根据角色的位置，拿到 Tilemap 的具体块
                Vector3Int cellPos = Grid.WorldToCell(transform.position);

                var grid = FindObjectOfType<GridController>().ShowGrid;

                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    // 如果没耕地
                    if (grid[cellPos.x, cellPos.y] == null)
                    {
                        // 耕地、开垦
                        // 设置数据
                        Tilemap.SetTile(cellPos, FindObjectOfType<GridController>().Pen);
                        grid[cellPos.x, cellPos.y] = new SoilData();
                    }
                    // 已经有耕地
                    else if (grid[cellPos.x, cellPos.y].HasPlant != true)
                    {
                        // 放种子
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
                // 根据角色的位置，拿到 Tilemap 的具体块
                Vector3Int cellPos = Grid.WorldToCell(transform.position);

                var grid = FindObjectOfType<GridController>().ShowGrid;

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
        }
    }
}
