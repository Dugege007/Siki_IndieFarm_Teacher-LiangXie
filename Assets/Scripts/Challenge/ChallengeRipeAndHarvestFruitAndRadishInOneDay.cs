
namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarvestFruitAndRadishInOneDay : Challenge
    {
        public override string Name { get; } = "一天内收获一个果子和萝卜";

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
