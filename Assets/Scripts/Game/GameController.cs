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
            Global.OnChallengeFinish.Register(challenge =>
            {
                Debug.Log("@@@@" + challenge.GetType().Name + "ÌôÕ½Íê³É");

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
