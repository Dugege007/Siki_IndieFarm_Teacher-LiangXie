
namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarvestFiveFruitsInOneDay : Challenge
    {
        public override string Name { get; } = "一天内成熟并收获两个果子";

        public override void OnStart()
        {
            
        }

        public override bool CheckFinsh()
        {
            return Global.RipeAndHarvestCountInCurrentDay >= 5;
        }

        public override void OnFinish()
        {
            
        }
    }
}
