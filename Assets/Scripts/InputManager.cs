using UnityEngine;

namespace Assets.Scripts
{
    public class InputManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (MenuManager.Current.MenusOpen())
                {
                    MenuManager.Current.HideAllMenus();
                }
                else if (HelpDialogWizard.IsDialogOpen_Static())
                {
                    HelpDialogWizard.HideDialog_Static();
                }
                else if (Dialog.IsDialogOpen_Static())
                {
                    Dialog.HideDialog_Static();
                }
                else
                {
                    MenuManager.Current.ShowPauseMenu();
                }
            }
        }
    }
}
