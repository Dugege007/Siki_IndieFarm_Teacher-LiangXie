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
        public static BindableProperty<string> CurrentToolName = new BindableProperty<string>("��");
    }
}
