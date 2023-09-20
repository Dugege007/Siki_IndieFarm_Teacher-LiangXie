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
            // ������ս�Ƿ����
            ChallengeController.OnChallengeFinish.Register(challenge =>
            {
                // ���ȫ����ս�������
                if (ChallengeController.Challenges.All(challenge => challenge.State == Challenge.States.Finished))
                {
                    // �� 0.5 ���ͨ��
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
