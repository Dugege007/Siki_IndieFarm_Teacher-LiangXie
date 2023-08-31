using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public partial class ChallengeController : ViewController
    {
        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(960, 540);

            GUI.Label(new Rect(960 - 300, 0, 300, 24), "@@ ÃÙ’Ω @@");

            for (int i = 0; i < Global.Challenges.Count; i++)
            {
                Challenge challenge = Global.Challenges[i];

                if(challenge.State == Challenge.States.Finished)
                {
                    GUI.Label(new Rect(960 - 300, 20 + i * 26, 300, 24),
                        "<color=green>" + i + ". " + challenge.Name + "</color>");
                }
                else
                {
                    GUI.Label(new Rect(960 - 300, 20 + i * 26, 300, 24),
                        i + ". " + challenge.Name);
                }
            }

        }
    }
}
