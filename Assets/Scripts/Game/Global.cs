using QFramework;
using System.Collections.Generic;

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
        /// 当前工具名称
        /// </summary>
        public static BindableProperty<string> CurrentTool = new BindableProperty<string>(Constant.TOOL_HAND);

        /// <summary>
        /// 当天成熟并收割的数量
        /// </summary>
        public static BindableProperty<int> RipeAndHarvestCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 当天收获的数量
        /// </summary>
        public static BindableProperty<int> HarvestCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 挑战列表
        /// </summary>
        public static List<Challenge> Challenges = new List<Challenge>()
        {
            // 收获第一个果子
            new ChallengeHarvestOneFruit(),
            // 一天内成熟并收获两个果子
            new ChallengeRipeAndHarvestTwoFruitsInADay(),
            // 一天内成熟并收获五个果子
            new ChallengeRipeAndHarvestFiveFruitsInADay(),
        };

        /// <summary>
        /// 已激活的挑战列表
        /// </summary>
        public static List<Challenge> ActiveChallenges = new List<Challenge>()
        {

        };

        /// <summary>
        /// 已完成的挑战列表
        /// </summary>
        public static List<Challenge> FinishedChallenges = new List<Challenge>()
        {

        };

        /// <summary>
        /// 在植物收割时
        /// </summary>
        public static EasyEvent<Plant> OnPlantHarvest = new EasyEvent<Plant>();

        /// <summary>
        /// 在挑战结束时
        /// </summary>
        public static EasyEvent<Challenge> OnChallengeFinish = new EasyEvent<Challenge>();

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

                default:
                    return "空";
            }
        }
    }
}
