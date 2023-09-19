using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace ProjectIndieFarm
{
	public partial class UIToolBar : ViewController
	{
        private void Start()
        {
            HideAllSelect();
            Btn1Select.Show();
            Global.Mouse.Icon.sprite = Btn1.GetComponentInChildren<Image>().sprite;

            Btn1.onClick.AddListener(() => ChangeTool(Constant.TOOL_HAND, Btn1Select, Btn1.GetComponent<Image>().sprite));
            Btn2.onClick.AddListener(() => ChangeTool(Constant.TOOL_HOE, Btn2Select, Btn2.GetComponent<Image>().sprite));
            Btn3.onClick.AddListener(() => ChangeTool(Constant.TOOL_SEED, Btn3Select, Btn3.GetComponent<Image>().sprite));
            Btn4.onClick.AddListener(() => ChangeTool(Constant.TOOL_WATERING_SCAN, Btn4Select, Btn4.GetComponent<Image>().sprite));
            Btn5.onClick.AddListener(() => ChangeTool(Constant.TOOL_SEED_RADISH, Btn5Select, Btn4.GetComponent<Image>().sprite));
        }

        private void Update()
        {
            // �������� 1 ������
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeTool(Constant.TOOL_HAND, Btn1Select, Btn1.GetComponent<Image>().sprite);
            }

            // �������� 2 ������ͷ
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeTool(Constant.TOOL_HOE, Btn2Select, Btn2.GetComponent<Image>().sprite);
            }

            // �������� 3 ��������
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeTool(Constant.TOOL_SEED, Btn3Select, Btn3.GetComponent<Image>().sprite);
            }

            // �������� 4 ��������
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeTool(Constant.TOOL_WATERING_SCAN, Btn4Select, Btn4.GetComponent<Image>().sprite);
            }

            // �������� 5 �������� �ܲ�
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ChangeTool(Constant.TOOL_SEED_RADISH, Btn5Select, Btn5.GetComponent<Image>().sprite);
            }
        }

        private void ChangeTool(string tool, Image selectImage,Sprite icon)
        {
            Global.CurrentTool.Value = tool;
            AudioController.Get.SFXTake.Play();

            HideAllSelect();
            selectImage.Show();
            Global.Mouse.Icon.sprite = icon;
        }

        private void HideAllSelect()
        {
            Btn1Select.Hide();
            Btn2Select.Hide();
            Btn3Select.Hide();
            Btn4Select.Hide();
            Btn5Select.Hide();
        }
    }
}
