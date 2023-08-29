using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectIndieFarm
{
    public partial class GamePassController : ViewController
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("GameStart");
            }
        }
    }
}
