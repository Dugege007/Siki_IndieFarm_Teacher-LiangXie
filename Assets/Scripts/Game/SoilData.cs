
namespace ProjectIndieFarm
{
    public class SoilData
    {
        /// <summary>
        /// 有植物
        /// </summary>
        public bool HasPlant { get; set; } = false;

        /// <summary>
        /// 有水
        /// </summary>
        public bool Watered { get; set; } = false;

        /// <summary>
        /// 植物状态
        /// </summary>
        public PlantState PlantState { get; set; } = PlantState.Seed;
    }
}
