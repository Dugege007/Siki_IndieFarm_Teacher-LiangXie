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
        /// 当前工具名称
        /// </summary>
        public static BindableProperty<string> CurrentToolName = new BindableProperty<string>("手");
    }
}
