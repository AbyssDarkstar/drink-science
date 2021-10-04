using System.Linq;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StartBeaker : MonoBehaviour, IDropHandler
    {
        public InputOutputLevel Output;

        private Image _beakerImage;

        [SerializeField]
        private ChemistrySet _chemistrySet;

        [SerializeField]
        private Sprite _closedSprite;

        private FluidStats _contentsStats;

        private int _fillState = 0;

        [SerializeField]
        private Sprite[] _fillStates = new Sprite[5];

        private bool _flowState;

        [SerializeField]
        private Sprite _openSprite;

        private Image _tripodImage;

        public void OnDrop(PointerEventData eventData)
        {
            if (_fillState < 4 && !_flowState)
            {
                if (_fillState == 0)
                {
                    _contentsStats = eventData.pointerDrag.GetComponent<FluidItem>().GetFluidItemSO().Stats;
                }
                else
                {
                    _contentsStats += eventData.pointerDrag.GetComponent<FluidItem>().GetFluidItemSO().Stats;
                }

                _fillState++;
                UpdateFillState();

                _beakerImage.color = _contentsStats.FluidColour;
            }
        }

        public void OnTapClick()
        {
            if (!_flowState && _chemistrySet.RemainingSpaceInEnd >= _fillState && _chemistrySet.IsFullyConnected())
            {
                _chemistrySet.ApplyAdjustments(_contentsStats, _fillState);
                ToggleFlowState();

                SfxManager.Current.PlayClip();
            }
        }

        public void ToggleFlowState()
        {
            _flowState = !_flowState;

            _tripodImage.sprite = _flowState ? _openSprite : _closedSprite;
        }

        private void Awake()
        {
            _flowState = false;
            _beakerImage = GetComponentsInChildren<Image>().FirstOrDefault(i => i.name == "Beaker");
            _tripodImage = GetComponentsInChildren<Image>().FirstOrDefault(i => i.name == "Tripod");

            TimeTickSystem.Create();
            TimeTickSystem.OnSecondElapsed += TimeTickSystem_OnSecondElapsed;
        }

        private void TimeTickSystem_OnSecondElapsed(object sender, TimeTickSystem.OnTickEventArgs e)
        {
            if (_flowState && _fillState > 0)
            {
                _fillState--;
                UpdateFillState();
            }
            else if (_flowState)
            {
                ToggleFlowState();

                SfxManager.Current.StopClip();
            }
        }
        private void UpdateFillState()
        {
            _beakerImage.sprite = _fillStates[_fillState];
        }
    }
}
