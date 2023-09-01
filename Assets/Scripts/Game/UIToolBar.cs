using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class UIToolBar : ViewController
	{
        private void Start()
        {
            Btn1.onClick.AddListener(() => ChangeTool(Constant.TOOL_HAND));
            Btn2.onClick.AddListener(() => ChangeTool(Constant.TOOL_HOE));
            Btn3.onClick.AddListener(() => ChangeTool(Constant.TOOL_SEED));
            Btn4.onClick.AddListener(() => ChangeTool(Constant.TOOL_WATERING_SCAN));
        }

        private void Update()
        {
            // �������� 1 ������
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeTool(Constant.TOOL_HAND);
            }

            // �������� 2 ������ͷ
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeTool(Constant.TOOL_HOE);
            }

            // �������� 3 ��������
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeTool(Constant.TOOL_SEED);
            }

            // �������� 4 ��������
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeTool(Constant.TOOL_WATERING_SCAN);
            }
        }

        private void ChangeTool(string tool)
        {
            Global.CurrentTool.Value = tool;
            AudioController.Get?.SFXTake.Play();
        }
    }
}
