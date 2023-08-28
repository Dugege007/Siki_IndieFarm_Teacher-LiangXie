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
				Tilemap.SetTile(cellPos, null);

				// 然后将其消除
			}
		}
	}
}
