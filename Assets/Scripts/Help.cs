using UnityEngine;

namespace Assets.Scripts
{
    public class Help : MonoBehaviour
    {
        public void HelpMe()
        {
            HelpDialogWizard.ShowDialog_Static();
        }
    }
}
