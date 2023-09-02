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

            // ����������Ϸ�����е�����
            Vector3 worldMousePos = mMainCamera.ScreenToWorldPoint(Input.mousePosition);
            // ��ȡ��ǰ
            Vector3Int cellPos = mGrid.WorldToCell(worldMousePos);

            // �ж������ҵ�λ��
            if (// ����
                cellPos.x - playerCellPos.x == -1 && cellPos.y - playerCellPos.y == 1 ||
                // ����
                cellPos.x - playerCellPos.x == 0 && cellPos.y - playerCellPos.y == 1 ||
                // ����
                cellPos.x - playerCellPos.x == 1 && cellPos.y - playerCellPos.y == 1 ||
                // ��
                cellPos.x - playerCellPos.x == -1 && cellPos.y - playerCellPos.y == 0 ||
                // ��
                cellPos.x - playerCellPos.x == 0 && cellPos.y - playerCellPos.y == 0 ||
                // ��
                cellPos.x - playerCellPos.x == 1 && cellPos.y - playerCellPos.y == 0 ||
                // ����
                cellPos.x - playerCellPos.x == -1 && cellPos.y - playerCellPos.y == -1 ||
                // ����
                cellPos.x - playerCellPos.x == 0 && cellPos.y - playerCellPos.y == -1 ||
                // ����
                cellPos.x - playerCellPos.x == 1 && cellPos.y - playerCellPos.y == -1)
            {
                // �õ��������½������
                Vector3 gridOriginPos = mGrid.CellToWorld(cellPos);
                // �õ����ӵ����ĵ�
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
