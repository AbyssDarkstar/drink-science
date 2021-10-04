using System.Linq;
using Assets.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class EndBeaker : MonoBehaviour
    {
        public InputOutputLevel Input;

        private const int MaxFillState = 4;

        private Image _beakerImage;

        [SerializeField]
        private ChemistrySet _chemistrySet;

        private FluidStats _contentsStats;

        [SerializeField]
        private TextMeshProUGUI _dumpButtonText;

        private int _fillState = 0;

        [SerializeField]
        private Sprite[] _fillStates = new Sprite[5];

        private bool _flowState;

        private int _targetFillState = 0;

        public int RemainingSpace => MaxFillState - _fillState;

        public void EmptyContents()
        {
            if (!_flowState)
            {
                if (_fillState == MaxFillState)
                {
                    if (Objective.Current.ValidateObjective(_contentsStats))
                    {
                        Statistics.Current.Succeess();
                        Dialog.ShowDialog_Static("Success!", "You have successfully met the criteria set out in the objective!");
                    }
                    else
                    {
                        Statistics.Current.AttemptMade();
                        Dialog.ShowDialog_Static("Failure!", "Your creation does not meet the specified brief. Please try again...");
                    }

                    Statistics.Current.AlterMentalStability(_contentsStats.MentalStability);
                    Statistics.Current.AlterDrunkeness(_contentsStats.AlcoholicStrength);
                    Statistics.Current.AlterAwakeness(_contentsStats.CaffeineLevel);
                    Statistics.Current.AlterEnergy(_contentsStats.EnergyLevel);
                }

                _fillState = 0;
                _targetFillState = 0;
                UpdateFillState();

                _dumpButtonText.text = "Dump";
            }
        }

        public void SendIncomingFluid(FluidStats stats, int qty)
        {
            _targetFillState += Mathf.Clamp(qty, 0, MaxFillState);

            if (_fillState == 0)
            {
                _contentsStats = stats;
            }
            else
            {
                _contentsStats += stats;
            }

            _flowState = true;
        }

        private void Awake()
        {
            _beakerImage = GetComponentsInChildren<Image>().FirstOrDefault(i => i.name == "Beaker");

            TimeTickSystem.Create();
            TimeTickSystem.OnSecondElapsed += TimeTickSystem_OnSecondElapsed;
        }

        private void TimeTickSystem_OnSecondElapsed(object sender, TimeTickSystem.OnTickEventArgs e)
        {
            if (_flowState && _fillState < _targetFillState)
            {
                _fillState++;
                UpdateFillState();
            }
            else if (_flowState)
            {
                _flowState = !_flowState;
            }

            if (_fillState == MaxFillState)
            {
                _dumpButtonText.text = "Drink!";
            }
        }

        private void UpdateFillState()
        {
            _beakerImage.sprite = _fillStates[_fillState];
            _beakerImage.color = _contentsStats.FluidColour;
        }
    }
}