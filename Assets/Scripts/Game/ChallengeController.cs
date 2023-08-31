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

            for (int i = 0; i < Global.ActiveChallenges.Count; i++)
            {
                Challenge challenge = Global.ActiveChallenges[i];

                GUI.Label(new Rect(960 - 300, 20 + i * 20, 300, 24), (i + 1) + ". " + challenge.Name);

                if (challenge.State == Challenge.States.Finished)
                {
                    GUI.Label(new Rect(960 - 300, 20 + i * 20, 300, 24), "<color=green>" + i + ". " + challenge.Name + "</color>");
                }
            }

            for (int i = 0; i < Global.FinishedChallenges.Count; i++)
            {
                Challenge challenge = Global.FinishedChallenges[i];

                GUI.Label(new Rect(960 - 300, 20 + (i + Global.ActiveChallenges.Count) * 20, 300, 24), "<color=green>" + (i + 1) + ". " + challenge.Name + "</color>");
            }
        }
    }
}
