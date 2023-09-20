
namespace ProjectIndieFarm
{
    public class ChallengeHarvestOneRadish : Challenge
    {
        public override string Name { get; } = "�ջ�һ���ܲ�";

        public override void OnStart()
        {

        }

        public override bool CheckFinsh()
        {
            return Global.Days.Value != StartDate && 
                Global.HarvestRadishCountInCurrentDay.Value >= 1;
        }

        public override void OnFinish()
        {

        }

    }
}
