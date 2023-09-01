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
            // 按下数字 1 键，手
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeTool(Constant.TOOL_HAND);
            }

            // 按下数字 2 键，锄头
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeTool(Constant.TOOL_HOE);
            }

            // 按下数字 3 键，种子
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeTool(Constant.TOOL_SEED);
            }

            // 按下数字 4 键，花洒
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
