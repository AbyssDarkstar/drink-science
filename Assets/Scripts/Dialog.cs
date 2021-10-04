using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class Dialog : MonoBehaviour
    {
        private static Dialog _instance;

        private TextMeshProUGUI _dialogTitle;
        private TextMeshProUGUI _dialogDetails;

        private void Awake()
        {
            _instance = this;
            _dialogTitle = transform.Find("Title").GetComponent<TextMeshProUGUI>();
            _dialogDetails = transform.Find("Details").GetComponent<TextMeshProUGUI>();

            HideDialog();
        }

        public void ShowDialog(string title, string details, TextAlignmentOptions alignment = TextAlignmentOptions.TopLeft)
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();

            _dialogTitle.text = title;
            _dialogDetails.text = details;
            _dialogDetails.alignment = alignment;
        }

        public void HideDialog()
        {
            gameObject.SetActive(false);
        }

        public static void ShowDialog_Static(string title, string details)
        {
            _instance.ShowDialog(title, details);
        }

        public static bool IsDialogOpen_Static()
        {
            return _instance.gameObject.activeSelf;
        }

        public static void HideDialog_Static()
        {
            _instance.HideDialog();
        }
    }
}
