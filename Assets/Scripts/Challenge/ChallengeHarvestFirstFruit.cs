
namespace ProjectIndieFarm
{
    public class ChallengeHarvestFirstFruit : Challenge
    {
        public override string Name { get; } = "�ջ��һ������";

        public override void OnStart()
        {
            
        }

        public override bool CheckFinsh()
        {
            return Global.FruitCount.Value > 0;
        }

        public override void OnFinish()
        {
            
        }

    }
}
