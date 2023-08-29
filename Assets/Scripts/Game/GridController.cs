using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace ProjectIndieFarm
{
    public partial class GridController : ViewController
    {
        private EasyGrid<SoilData> mShowGrid = new EasyGrid<SoilData>(10, 10);

        public EasyGrid<SoilData> ShowGrid => mShowGrid;
        public TileBase Pen;

        private void Start()
        {
            mShowGrid[0, 0] = new SoilData();
            mShowGrid[1, 1] = new SoilData();
            mShowGrid[2, 2] = new SoilData();
            mShowGrid[3, 3] = new SoilData();
            mShowGrid[4, 4] = new SoilData();
            mShowGrid[5, 5] = new SoilData();
            mShowGrid[6, 6] = new SoilData();
            mShowGrid[7, 7] = new SoilData();
            mShowGrid[8, 8] = new SoilData();
            mShowGrid[9, 9] = new SoilData();

            mShowGrid.ForEach((x, y, data) =>
            {
                if (data != null)
                {
                    Tilemap.SetTile(new Vector3Int(x, y), Pen);
                }
            });
        }

        private void Update()
        {
            var grid = FindObjectOfType<Grid>();
            mShowGrid.ForEach((x, y, _) =>
            {
                Vector3 tileWorldPos = grid.CellToWorld(new Vector3Int(x, y, 0));

                Vector3 leftBottomPos = tileWorldPos;
                Vector3 rightBottomPos = tileWorldPos + new Vector3(grid.cellSize.x, 0, 0);
                Vector3 leftTopPos = tileWorldPos + new Vector3(0, grid.cellSize.y, 0);
                Vector3 rightTopPos = tileWorldPos + new Vector3(grid.cellSize.x, grid.cellSize.y, 0);

                Debug.DrawLine(leftBottomPos, rightBottomPos, Color.red);
                Debug.DrawLine(leftBottomPos, leftTopPos, Color.red);
                Debug.DrawLine(leftTopPos, rightTopPos, Color.red);
                Debug.DrawLine(rightBottomPos, rightTopPos, Color.red);
            });
        }
    }
}
