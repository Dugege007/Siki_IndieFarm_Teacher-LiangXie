
namespace ProjectIndieFarm
{
    public abstract class Challenge
    {
        public enum States
        {
            NotStart,
            Started,
            Finished
        }

        public States State = States.NotStart;

        public abstract string Name { get; }

        public abstract void OnStart();

        public abstract bool CheckFinsh();

        public abstract void OnFinish();
    }
}
