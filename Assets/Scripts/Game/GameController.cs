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
            // 监听植物是否是当天成熟当天收割的
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant.RipeDay == Global.Days.Value)
                {
                    Global.RipeAndHarvestCountInCurrentDay.Value++;
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 监听挑战是否完成
            Global.OnChallengeFinish.Register(challenge =>
            {
                Debug.Log("@@@@" + challenge.GetType().Name + "挑战完成");

                // 如果全部挑战都完成
                if (Global.Challenges.All(challenge => challenge.State == Challenge.States.Finished))
                {
                    // 在 0.5 秒后通关
                    ActionKit.Delay(0.5f, () =>
                    {
                        SceneManager.LoadScene("GamePass");

                    }).Start(this);
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            Global.Challenges
                .Where(challenge => challenge.State != Challenge.States.Finished)
                .ForEach(challenge =>
            {
                switch (challenge.State)
                {
                    case Challenge.States.NotStart:
                        challenge.OnStart();
                        challenge.State = Challenge.States.Started;
                        break;

                    case Challenge.States.Started:
                        if (challenge.CheckFinsh())
                        {
                            challenge.OnFinish();
                            challenge.State = Challenge.States.Finished;
                            Global.OnChallengeFinish.Trigger(challenge);
                        }
                        break;

                    case Challenge.States.Finished:
                        break;

                    default:
                        break;
                }
            });
        }
    }
}
