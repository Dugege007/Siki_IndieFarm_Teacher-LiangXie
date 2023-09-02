using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public partial class MouseController : ViewController
    {
        private Grid mGrid;
        private Camera mMainCamera;
        private SpriteRenderer mSprite;

        private void Start()
        {
            mGrid = FindObjectOfType<GridController>().GetComponent<Grid>();
            mMainCamera = Camera.main;
            mSprite = GetComponent<SpriteRenderer>();
            mSprite.enabled = false;
        }

        private void Update()
        {
            Vector3 playerCellPos = mGrid.WorldToCell(Global.Player.Position());

            // 获得鼠标在游戏场景中的坐标
            Vector3 worldMousePos = mMainCamera.ScreenToWorldPoint(Input.mousePosition);
            // 获取当前
            Vector3Int cellPos = mGrid.WorldToCell(worldMousePos);

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
                // 拿到格子左下角坐标点
                Vector3 gridOriginPos = mGrid.CellToWorld(cellPos);
                // 拿到格子的中心点
                gridOriginPos += mGrid.cellSize * 0.5f;
                transform.Position(gridOriginPos.x, gridOriginPos.y);
                mSprite.enabled = true;
            }
            else
            {
                mSprite.enabled = false;
            }
        }
    }
}
