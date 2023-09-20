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
        /// �����ջ������
        /// </summary>
        public static BindableProperty<int> HarvestCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// ������첢�ո������
        /// </summary>
        public static BindableProperty<int> RipeAndHarvestCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// �����ո��ܲ�������
        /// </summary>
        public static BindableProperty<int> HarvestRadishCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// ������첢�ո��ܲ�������
        /// </summary>
        public static BindableProperty<int> RipeAndHarvestRadishCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// ��ս�б�
        /// </summary>
        public static List<Challenge> Challenges = new List<Challenge>()
        {
            // �ջ�һ������
            new ChallengeHarvestOneFruit(),
            // һ���ڳ��첢�ջ���������
            new ChallengeRipeAndHarvestTwoFruitsInADay(),
            // һ���ڳ��첢�ջ��������
            new ChallengeRipeAndHarvestFiveFruitsInADay(),

            // �ջ�һ���ܲ�
            new ChallengeHarvestOneRadish(),
            // һ�����ջ�һ�����Ӻ��ܲ�
            new ChallengeRipeAndHarvestFruitAndRadishInOneDay(),
        };

        /// <summary>
        /// �Ѽ������ս�б�
        /// </summary>
        public static List<Challenge> ActiveChallenges = new List<Challenge>()
        {

        };

        /// <summary>
        /// ����ɵ���ս�б�
        /// </summary>
        public static List<Challenge> FinishedChallenges = new List<Challenge>()
        {

        };

        /// <summary>
        /// ����ս����ʱ
        /// </summary>
        public static EasyEvent<Challenge> OnChallengeFinish = new EasyEvent<Challenge>();

        private void Start()
        {
            // ��������
            mLabelStyle = new GUIStyle("Label")
            {
                font = Font,
            };

            Challenge randomChallenge = Challenges.GetRandomItem();
            ActiveChallenges.Add(randomChallenge);

            // ����ֲ���Ƿ��ǵ�����쵱���ո��
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

            GUI.Label(new Rect(960 - 300, 0, 300, 24), "@@ ��ս @@", mLabelStyle);

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
