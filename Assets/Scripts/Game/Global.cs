using QFramework;

namespace ProjectIndieFarm
{
    public class Global
    {
        /// <summary>
        /// 默认从第一天开始
        /// </summary>
        public static BindableProperty<int> Days = new BindableProperty<int>(1);

        /// <summary>
        /// 果子数量
        /// </summary>
        public static BindableProperty<int> FruitCount = new BindableProperty<int>(0);

        /// <summary>
        /// 萝卜数量
        /// </summary>
        public static BindableProperty<int> RadishCount = new BindableProperty<int>(0);

        /// <summary>
        /// 当前工具名称
        /// </summary>
        public static BindableProperty<string> CurrentTool = new BindableProperty<string>(Constant.TOOL_HAND);

        /// <summary>
        /// 在植物收割时
        /// </summary>
        public static EasyEvent<IPlant> OnPlantHarvest = new EasyEvent<IPlant>();

        public static Player Player = null;
        public static ToolController Mouse = null;
    }

    /// <summary>
    /// 常量
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// 手
        /// </summary>
        public const string TOOL_HAND = "hand";
        /// <summary>
        /// 锄头
        /// </summary>
        public const string TOOL_HOE = "hoe";
        /// <summary>
        /// 铁锹
        /// </summary>
        public const string TOOL_SHOVEL = "shovel";
        /// <summary>
        /// 种子
        /// </summary>
        public const string TOOL_SEED = "seed";
        /// <summary>
        /// 花洒
        /// </summary>
        public const string TOOL_WATERING_SCAN = "watering_scan";
        /// <summary>
        /// 萝卜
        /// </summary>
        public const string TOOL_SEED_RADISH = "seed_radish";

        public static string DisplayName(string toolName)
        {
            switch (toolName)
            {
                case TOOL_HAND:
                    return "手";

                case TOOL_HOE:
                    return "锄头";

                case TOOL_SHOVEL:
                    return "铁锹";

                case TOOL_SEED:
                    return "种子";

                case TOOL_WATERING_SCAN:
                    return "花洒";

                case TOOL_SEED_RADISH:
                    return "萝卜";

                default:
                    return "空";
            }
        }
    }
}
