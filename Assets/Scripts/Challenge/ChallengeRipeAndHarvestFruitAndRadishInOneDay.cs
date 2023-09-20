
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
                Global.RipeAndHarvestCountInCurrentDay.Value >= 1 &&
                Global.RipeAndHarvestRadishCountInCurrentDay.Value >= 1;
        }

        public override void OnFinish()
        {

        }
    }
}
