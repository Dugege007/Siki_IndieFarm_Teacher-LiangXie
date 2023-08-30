
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
            // 监听植物是否是当天成熟当天收割的
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant.RipeDay == Global.Days.Value)
                {
                    Global.RipeAndHarvestCountInCurrentDay.Value++;
                }

            }).AddToUnregisterList(this);
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
