using Assets.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

//using Random = System.Random;

namespace Assets.Scripts
{
    public class Objective : MonoBehaviour
    {
        public static Objective Current;

        [SerializeField]
        private TextMeshProUGUI _alcohol;

        private bool _alcoholAny;

        private int _alcoholLevelHigh;

        private int _alcoholLevelLow;

        [SerializeField]
        private TextMeshProUGUI _caffeine;

        private bool _caffeineAny;

        private int _caffeineLevelHigh;

        private int _caffeineLevelLow;

        [SerializeField]
        private TextMeshProUGUI _energy;

        private bool _energyAny;

        private int _energyLevelHigh;

        private int _energyLevelLow;

        [SerializeField]
        private TextMeshProUGUI _sour;

        private bool _sourAny;

        private int _sourLevelHigh;

        private int _sourLevelLow;

        [SerializeField]
        private TextMeshProUGUI _sweet;

        private bool _sweetAny;

        private int _sweetLevelHigh;

        private int _sweetLevelLow;

        public void SetObjective(ObjectiveSO newObjective)
        {
            _alcoholAny = newObjective.AlcoholAny;
            _alcoholLevelHigh = newObjective.AlcoholLevelHigh;
            _alcoholLevelLow = newObjective.AlcoholLevelLow;
            _caffeineAny = newObjective.CaffeineAny;
            _caffeineLevelHigh = newObjective.CaffeineLevelHigh;
            _caffeineLevelLow = newObjective.CaffeineLevelLow;
            _energyAny = newObjective.EnergyAny;
            _energyLevelHigh = newObjective.EnergyLevelHigh;
            _energyLevelLow = newObjective.EnergyLevelLow;
            _sourAny = newObjective.SourAny;
            _sourLevelHigh = newObjective.SourLevelHigh;
            _sourLevelLow = newObjective.SourLevelLow;
            _sweetAny = newObjective.SweetAny;
            _sweetLevelHigh = newObjective.SweetLevelHigh;
            _sweetLevelLow = newObjective.SweetLevelLow;

            if (_alcoholAny)
            {
                _alcohol.text = "Alcohol: --";
            }
            else
            {
                _alcohol.text = "Alcohol: " + _alcoholLevelLow;
                if (_alcoholLevelHigh > _alcoholLevelLow)
                {
                    _alcohol.text += " - " + _alcoholLevelHigh;
                }
            }

            if (_caffeineAny)
            {
                _caffeine.text = "Caffeine: --";
            }
            else
            {
                _caffeine.text = "Caffeine: " + _caffeineLevelLow;
                if (_caffeineLevelHigh > _caffeineLevelLow)
                {
                    _caffeine.text += " - " + _caffeineLevelHigh;
                }
            }

            if (_energyAny)
            {
                _energy.text = "Energy: --";
            }
            else
            {
                _energy.text = "Energy: " + _energyLevelLow;
                if (_energyLevelHigh > _energyLevelLow)
                {
                    _energy.text += " - " + _energyLevelHigh;
                }
            }

            if (_sourAny)
            {
                _sour.text = "Sour: --";
            }
            else
            {
                _sour.text = "Sour: " + _sourLevelLow;
                if (_sourLevelHigh > _sourLevelLow)
                {
                    _sour.text += " - " + _sourLevelHigh;
                }
            }

            if (_sweetAny)
            {
                _sweet.text = "Sweet: --";
            }
            else
            {
                _sweet.text = "Sweet: " + _sweetLevelLow;
                if (_sweetLevelHigh > _sweetLevelLow)
                {
                    _sweet.text += " - " + _sweetLevelHigh;
                }
            }
        }

        private void Awake()
        {
            Current = this;
        }

        private void Start()
        {
            SelectRandomObjective();
        }

        public void SelectRandomObjective()
        {
            var objectiveList = Resources.Load<ObjectiveListSO>("ObjectiveList").ObjectiveList;

            var next = Random.Range(0, objectiveList.Count);
        
            SetObjective(objectiveList[next]);
        }

        public bool ValidateObjective(FluidStats stats)
        {
            if ((stats.AlcoholicStrength < _alcoholLevelLow || stats.AlcoholicStrength > _alcoholLevelHigh) && !_alcoholAny)
            {
                return false;
            }

            if ((stats.EnergyLevel < _energyLevelLow || stats.EnergyLevel > _energyLevelHigh) && !_energyAny)
            {
                return false;
            }

            if ((stats.CaffeineLevel < _caffeineLevelLow || stats.CaffeineLevel > _caffeineLevelHigh) && !_caffeineAny)
            {
                return false;
            }

            if ((stats.Sourness < _sourLevelLow || stats.Sourness > _sourLevelHigh) && !_sourAny)
            {
                return false;
            }

            if ((stats.Sweetness < _sweetLevelLow || stats.Sweetness > _sweetLevelHigh) && !_sweetAny)
            {
                return false;
            }

            return true;
        }
    }
}
