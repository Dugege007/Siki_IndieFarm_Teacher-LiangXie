
namespace ProjectIndieFarm
{
    public class ChallengeHarvestOneFruit : Challenge
    {
        public override string Name { get; } = "收获一个果子";

        public override void OnStart()
        {

        }

        public override bool CheckFinsh()
        {
            return Global.Days.Value != StartDate && Global.HarvestCountInCurrentDay.Value >= 1;
        }

        public override void OnFinish()
        {

        }

    }
}
