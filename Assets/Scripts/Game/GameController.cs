using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectIndieFarm
{
	public partial class GameController : ViewController
	{
		private void Start()
		{
			Global.FruitCount.Register(fruitCount =>
			{
				if (fruitCount == 1)
				{
					// ��Ϸͨ��
					// ��ʱ��ת����
					ActionKit.Delay(1f, () =>
					{
						SceneManager.LoadScene("GamePass");

					}).Start(this);
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
	}
}
