using QFramework;

namespace ProjectIndieFarm
{
    public class Global
    {
        /// <summary>
        /// Ĭ�ϴӵ�һ�쿪ʼ
        /// </summary>
        public static BindableProperty<int> Days = new BindableProperty<int>(1);

        /// <summary>
        /// ��������
        /// </summary>
        public static BindableProperty<int> FruitCount = new BindableProperty<int>(0);

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        public static BindableProperty<string> CurrentTool = new BindableProperty<string>(Constant.TOOL_HAND);

    }

    /// <summary>
    /// ����
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// ��
        /// </summary>
        public const string TOOL_HAND = "hand";
        /// <summary>
        /// ��ͷ
        /// </summary>
        public const string TOOL_HOE = "hoe";
        /// <summary>
        /// ����
        /// </summary>
        public const string TOOL_SHOVEL = "shovel";
        /// <summary>
        /// ����
        /// </summary>
        public const string TOOL_SEED = "seed";
        /// <summary>
        /// ����
        /// </summary>
        public const string TOOL_WATERING_SCAN = "watering_scan";

        public static string DisplayName(string toolName)
        {
            switch (toolName)
            {
                case TOOL_HAND:
                    return "��";

                case TOOL_HOE:
                    return "��ͷ";

                case TOOL_SHOVEL:
                    return "����";

                case TOOL_SEED:
                    return "����";

                case TOOL_WATERING_SCAN:
                    return "����";

                default:
                    return "��";
            }
        }
    }
}
