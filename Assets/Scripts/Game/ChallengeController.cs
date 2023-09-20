using UnityEngine;
using QFramework;
using System.Linq;
using System.Collections.Generic;

namespace ProjectIndieFarm
{
    public partial class ChallengeController : ViewController
    {
        public Font Font;
        private GUIStyle mLabelStyle;

        /// <summary>
        /// 当天收获的数量
        /// </summary>
        public static BindableProperty<int> HarvestCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 当天成熟并收割的数量
        /// </summary>
        public static BindableProperty<int> RipeAndHarvestCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 当天收割萝卜的数量
        /// </summary>
        public static BindableProperty<int> HarvestRadishCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 当天成熟并收割萝卜的数量
        /// </summary>
        public static BindableProperty<int> RipeAndHarvestRadishCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 挑战列表
        /// </summary>
        public static List<Challenge> Challenges = new List<Challenge>()
        {
            // 收获一个果子
            new ChallengeHarvestOneFruit(),
            // 一天内成熟并收获两个果子
            new ChallengeRipeAndHarvestTwoFruitsInADay(),
            // 一天内成熟并收获五个果子
            new ChallengeRipeAndHarvestFiveFruitsInADay(),

            // 收获一个萝卜
            new ChallengeHarvestOneRadish(),
            // 一天内收获一个果子和萝卜
            new ChallengeRipeAndHarvestFruitAndRadishInOneDay(),
        };

        /// <summary>
        /// 已激活的挑战列表
        /// </summary>
        public static List<Challenge> ActiveChallenges = new List<Challenge>()
        {

        };

        /// <summary>
        /// 已完成的挑战列表
        /// </summary>
        public static List<Challenge> FinishedChallenges = new List<Challenge>()
        {

        };

        /// <summary>
        /// 在挑战结束时
        /// </summary>
        public static EasyEvent<Challenge> OnChallengeFinish = new EasyEvent<Challenge>();

        private void Start()
        {
            // 设置字体
            mLabelStyle = new GUIStyle("Label")
            {
                font = Font,
            };

            Challenge randomChallenge = Challenges.GetRandomItem();
            ActiveChallenges.Add(randomChallenge);

            // 监听植物是否是当天成熟当天收割的
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant is Plant)
                {
                    HarvestCountInCurrentDay.Value++;

                    if (plant.RipeDay == Global.Days.Value)
                    {
                        RipeAndHarvestCountInCurrentDay.Value++;
                    }
                }
                else if (plant is PlantRadish)
                {
                    HarvestRadishCountInCurrentDay.Value++;

                    if (plant.RipeDay == Global.Days.Value)
                    {
                        RipeAndHarvestRadishCountInCurrentDay.Value++;
                    }
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            bool hasFinishedChallenge = false;

            ActiveChallenges.ForEach(challenge =>
            {
                switch (challenge.State)
                {
                    case Challenge.States.NotStart:
                        challenge.StartDate = Global.Days.Value;
                        challenge.OnStart();
                        challenge.State = Challenge.States.Started;
                        break;

                    case Challenge.States.Started:
                        if (challenge.CheckFinsh())
                        {
                            challenge.OnFinish();
                            challenge.State = Challenge.States.Finished;
                            OnChallengeFinish.Trigger(challenge);
                            FinishedChallenges.Add(challenge);
                            hasFinishedChallenge = true;
                        }
                        break;

                    case Challenge.States.Finished:
                        break;

                    default:
                        break;
                }
            });

            if (hasFinishedChallenge)
            {
                ActiveChallenges.RemoveAll(challenge => challenge.State == Challenge.States.Finished);
            }

            if (ActiveChallenges.Count == 0 && FinishedChallenges.Count != Challenges.Count)
            {
                Challenge randomChallenge = Challenges.Where(c => c.State == Challenge.States.NotStart).ToList().GetRandomItem();
                ActiveChallenges.Add(randomChallenge);
            }
        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(960, 540);

            GUI.Label(new Rect(960 - 300, 0, 300, 24), "@@ 挑战 @@", mLabelStyle);

            for (int i = 0; i < ActiveChallenges.Count; i++)
            {
                Challenge challenge = ActiveChallenges[i];

                GUI.Label(new Rect(960 - 300, 20 + i * 20, 300, 24), (i + 1) + ". " + challenge.Name, mLabelStyle);

                if (challenge.State == Challenge.States.Finished)
                {
                    GUI.Label(new Rect(960 - 300, 20 + i * 20, 300, 24), "<color=green>" + i + ". " + challenge.Name + "</color>", mLabelStyle);
                }
            }

            for (int i = 0; i < FinishedChallenges.Count; i++)
            {
                Challenge challenge = FinishedChallenges[i];

                GUI.Label(new Rect(960 - 300, 20 + (i + ActiveChallenges.Count) * 20, 300, 24), "<color=green>" + (i + 1) + ". " + challenge.Name + "</color>", mLabelStyle);
            }
        }
    }
}
