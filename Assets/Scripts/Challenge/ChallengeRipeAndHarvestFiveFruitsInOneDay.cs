
namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarvestFiveFruitsInOneDay : Challenge
    {
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
