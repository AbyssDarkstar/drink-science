using Assets.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class AdvancedTooltip : MonoBehaviour
    {
        private static AdvancedTooltip _instance;

        [SerializeField]
        private Camera _uiCamera;

        [SerializeField]
        private RectTransform _canvasRectTransform;
    
        private TextMeshProUGUI _itemName;

        private Image _itemImage;

        private TextMeshProUGUI _itemSweet;
        private TextMeshProUGUI _itemSour;
        private TextMeshProUGUI _itemAlcohol;
        private TextMeshProUGUI _itemEnergy;
        private TextMeshProUGUI _itemCaffeine;

        private TextMeshProUGUI _itemDescription;

        private RectTransform _backgroundRectTransform;
    
        private void Awake()
        {
            _instance = this;

            _backgroundRectTransform = transform.Find("Border").GetComponent<RectTransform>();
            _itemName = transform.Find("Name").GetComponent<TextMeshProUGUI>();

            _itemImage = transform.Find("Image").GetComponent<Image>();

            _itemSweet = transform.Find("SweetLevel").GetComponent<TextMeshProUGUI>();
            _itemSour = transform.Find("SourLevel").GetComponent<TextMeshProUGUI>();
            _itemAlcohol = transform.Find("AlcoholLevel").GetComponent<TextMeshProUGUI>();
            _itemEnergy = transform.Find("EnergyLevel").GetComponent<TextMeshProUGUI>();
            _itemCaffeine = transform.Find("CaffeineLevel").GetComponent<TextMeshProUGUI>();

            _itemDescription = transform.Find("Description").GetComponent<TextMeshProUGUI>();

            HideTooltip();
        }

        private void Update()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, _uiCamera, out var localPoint);
            transform.localPosition = localPoint;
        
            var anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
            if (anchoredPosition.x + _backgroundRectTransform.rect.width > _canvasRectTransform.rect.width)
            {
                anchoredPosition.x = _canvasRectTransform.rect.width - _backgroundRectTransform.rect.width;
            }

            if (anchoredPosition.y + _backgroundRectTransform.rect.height > _canvasRectTransform.rect.height)
            {
                anchoredPosition.y = _canvasRectTransform.rect.height + _backgroundRectTransform.rect.height;
            }

            transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        }

        private void ShowTooltip(ApparatusSO apparatus)
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();

            _itemName.text = apparatus.ApparatusName;

            _itemImage.sprite = apparatus.Image;
            _itemImage.color = Color.white;

            _itemSweet.text = apparatus.Sweetness.MathmaticalSymbol() + " " + apparatus.Sweetness.Amount;
            _itemSour.text = apparatus.Sourness.MathmaticalSymbol() + " " + apparatus.Sourness.Amount;
            _itemAlcohol.text = apparatus.AlcoholicStrength.MathmaticalSymbol() + " " + apparatus.AlcoholicStrength.Amount;
            _itemEnergy.text = apparatus.EnergyLevel.MathmaticalSymbol() + " " + apparatus.EnergyLevel.Amount;
            _itemCaffeine.text = apparatus.CaffineLevel.MathmaticalSymbol() + " " + apparatus.CaffineLevel.Amount;

            _itemDescription.text = apparatus.ApparatusDescription;
            Update();
        }

        private void ShowTooltip(FluidItemSO item)
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();

            _itemName.text = item.ItemName;

            _itemImage.sprite = item.Prefab.GetComponent<Image>().sprite;

            _itemImage.color = item.Stats.FluidColour;

            _itemSweet.text = item.Stats.Sweetness.ToString();
            _itemSour.text = item.Stats.Sourness.ToString();
            _itemAlcohol.text = item.Stats.AlcoholicStrength.ToString();
            _itemEnergy.text = item.Stats.EnergyLevel.ToString();
            _itemCaffeine.text = item.Stats.CaffeineLevel.ToString();

            _itemDescription.text = item.ItemDescription;

            var mentalStability = Statistics.Current.GetMentalStability();

            if (mentalStability < -50)
            {
                _itemImage.color = Color.white;
            }

            if (mentalStability < 0)
            {
                _itemDescription.text = RandomizeText(item.ItemDescription);
            }

            if (mentalStability < 10)
            {
                _itemName.text = RandomizeText(item.ItemName);
            }

            if (mentalStability < 25)
            {
                _itemSweet.text = Random.Range(0, 50).ToString();
                _itemSour.text = Random.Range(0, 50).ToString();
                _itemAlcohol.text = Random.Range(0, 50).ToString();
                _itemEnergy.text = Random.Range(0, 50).ToString();
                _itemCaffeine.text = Random.Range(0, 50).ToString();
            }

            Update();
        }

        private string RandomizeText(string text)
        {
            var newText = string.Empty;
            for (var i = 0; i < text.Length-1; i++)
            {
                newText += (char)Random.Range(48, 127);
            }

            return newText;
        }

        private void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        public static void ShowTooltip_Static(ApparatusSO apparatus)
        {
            _instance.ShowTooltip(apparatus);
        }

        public static void ShowTooltip_Static(FluidItemSO item)
        {
            _instance.ShowTooltip(item);
        }

        public static void HideTooltip_Static()
        {
            _instance.HideTooltip();
        }
    }
}