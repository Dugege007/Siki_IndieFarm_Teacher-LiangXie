
using QFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarvestTwoFruitsInOneDay : Challenge, IUnRegisterList
    {
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();

        public override void OnStart()
        {

        }

        public override bool CheckFinsh()
        {
            return Global.RipeAndHarvestCountInCurrentDay >= 2;
        }

        public override void OnFinish()
        {
            this.UnRegisterAll();
        }
    }
}
