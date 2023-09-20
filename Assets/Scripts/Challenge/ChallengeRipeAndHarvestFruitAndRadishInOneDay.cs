
namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarvestFruitAndRadishInOneDay : Challenge
    {
        public override string Name { get; } = "һ�����ջ�һ�����Ӻ��ܲ�";

        public override void OnStart()
        {

        }

        public override bool CheckFinsh()
        {
            return Global.Days.Value != StartDate &&
                ChallengeController.RipeAndHarvestCountInCurrentDay.Value >= 1 &&
                ChallengeController.RipeAndHarvestRadishCountInCurrentDay.Value >= 1;
        }

        public override void OnFinish()
        {

        }
    }
}
