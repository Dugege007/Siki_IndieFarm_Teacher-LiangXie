using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;
using System.Linq;

namespace ProjectIndieFarm
{
    public partial class GameController : ViewController
    {
        private void Start()
        {
            // 监听挑战是否完成
            ChallengeController.OnChallengeFinish.Register(challenge =>
            {
                // 如果全部挑战都完成了
                if (ChallengeController.Challenges.All(challenge => challenge.State == Challenge.States.Finished))
                {
                    // 在 0.5 秒后通关
                    ActionKit.Delay(0.5f, () =>
                    {
                        SceneManager.LoadScene("GamePass");

                    }).Start(this);
                }

                AudioController.Get.SFXChallengeFinish.Play();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {

        }
    }
}
