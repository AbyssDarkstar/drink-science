using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HelpDialogWizard : MonoBehaviour
    {
        private static HelpDialogWizard _instance;

        private int _currentPage = 1;

        private GameObject _page1;
        private GameObject _page2;
        private GameObject _page3;
        private GameObject _page4;

        private Button _next;
        private Button _previous;

        private void Awake()
        {
            _instance = this;

            _page1 = transform.Find("HelpContent").Find("Page 1").gameObject;
            _page2 = transform.Find("HelpContent").Find("Page 2").gameObject;
            _page3 = transform.Find("HelpContent").Find("Page 3").gameObject;
            _page4 = transform.Find("HelpContent").Find("Page 4").gameObject;

            _next = transform.Find("WizardControls").Find("Next").GetComponent<Button>();
            _previous = transform.Find("WizardControls").Find("Previous").GetComponent<Button>();

            SetPage();
        }

        public void ShowDialog()
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();

            _currentPage = 1;

            SetPage();
        }

        public void HideDialog()
        {
            gameObject.SetActive(false);
        }

        public static void ShowDialog_Static()
        {
            _instance.ShowDialog();
        }

        public static bool IsDialogOpen_Static()
        {
            return _instance.gameObject.activeSelf;
        }

        public static void HideDialog_Static()
        {
            _instance.HideDialog();
        }

        public void NextPage()
        {
            _currentPage++;

            SetPage();
        }

        public void PreviousPage()
        {
            _currentPage--;

            SetPage();
        }

        private void SetPage()
        {
            _page1.SetActive(false);
            _page2.SetActive(false);
            _page3.SetActive(false);
            _page4.SetActive(false);

            switch (_currentPage)
            {
                case 1:
                    _previous.interactable = false;
                    _next.interactable = true;
                    _page1.SetActive(true);
                    break;
                case 2:
                    _previous.interactable = true;
                    _next.interactable = true;
                    _page2.SetActive(true);
                    break;
                case 3:
                    _previous.interactable = true;
                    _next.interactable = true;
                    _page3.SetActive(true);
                    break;
                case 4:
                    _previous.interactable = true;
                    _next.interactable = false;
                    _page4.SetActive(true);
                    break;
            }
        }
    }
}