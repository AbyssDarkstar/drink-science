using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FluidItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Canvas _canvas;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        private Vector2 _originalPosition;

        private FluidItemSO _fluidItemSo;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Statistics.Current.OnStabilityLevelChanged += OnStabilityLevelChanged;
        }

        private void OnStabilityLevelChanged(object sender, int e)
        {
            if (e < -50)
            {
                GetComponent<Image>().color = Color.white;
            }
        }

        public void SetFluidItemSO(FluidItemSO fluidItemSo)
        {
            _fluidItemSo = fluidItemSo;
            GetComponent<Image>().color = fluidItemSo.Stats.FluidColour;
        }

        public FluidItemSO GetFluidItemSO()
        {
            return _fluidItemSo;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _originalPosition = _rectTransform.anchoredPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.6f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor / 0.75f;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition = _originalPosition;
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            AdvancedTooltip.ShowTooltip_Static(_fluidItemSo);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            AdvancedTooltip.HideTooltip_Static();
        }
    }
}