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
				Tilemap.SetTile(cellPos, null);

				// Ȼ��������
			}
		}
	}
}
