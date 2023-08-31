
namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarvestTwoFruitsInADay : Challenge
    {
        public override string Name { get; } = "一天内成熟并收获两个果子";

        public override void OnStart()
        {

        }

        public override bool CheckFinsh()
        {
            return Global.Days.Value != StartDate && Global.RipeAndHarvestCountInCurrentDay >= 2;
        }

        public override void OnFinish()
        {

        }
    }
}
