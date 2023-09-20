
using QFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarvestFiveFruitsInADay : Challenge, IUnRegisterList
    {
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();

        public override string Name { get; } = "一天内成熟并收获五个果子";

        public override void OnStart()
        {

        }

        public override bool CheckFinsh()
        {
            return Global.Days.Value != StartDate && 
                Global.RipeAndHarvestCountInCurrentDay.Value >= 5;
        }

        public override void OnFinish()
        {
            this.UnRegisterAll();
        }
    }
}
