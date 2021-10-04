using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class Statistics : MonoBehaviour
    {
        public EventHandler<int> OnStabilityLevelChanged;

        public static Statistics Current;

        private int _alcoholLevel;

        private int _attemptCount;

        [SerializeField]
        private TextMeshProUGUI _attempts;

        [SerializeField]
        private TextMeshProUGUI _awakeness;

        private int _awakenessLevel = 100;

        [SerializeField]
        private TextMeshProUGUI _drunkeness;

        [SerializeField]
        private TextMeshProUGUI _energy;

        private int _energyLevel = 50;

        [SerializeField]
        private TextMeshProUGUI _mentalStability;

        private int _mentalStabilityLevel = 25;

        private int _successCount;

        [SerializeField]
        private TextMeshProUGUI _successes;

        public void AttemptMade()
        {
            _attemptCount++;
            SetAtteptsText();
        }

        public void Succeess()
        {
            _successCount++;
            SetSuccessesText();
            
            SetAtteptsText();
        }

        public void AlterMentalStability(int change)
        {
            _mentalStabilityLevel += change;
            _mentalStabilityLevel = Mathf.Clamp(_mentalStabilityLevel, -100, 100);
            
            OnStabilityLevelChanged?.Invoke(this, _mentalStabilityLevel);

            SetMentalStabilityText();
        }

        public void AlterDrunkeness(int change)
        {
            _alcoholLevel += change;
            _alcoholLevel = Mathf.Clamp(_alcoholLevel, 0, 100);
            SetDrunkenessText();
        }

        public void AlterAwakeness(int change)
        {
            _awakenessLevel += change;
            _awakenessLevel = Mathf.Clamp(_awakenessLevel, -100, 100);
            SetAwakenessText();
        }

        public void AlterEnergy(int change)
        {
            _energyLevel += change;
            _energyLevel = Mathf.Clamp(_energyLevel, 0, 100);
            SetEnergyText();
        }

        public int GetMentalStability()
        {
            return _mentalStabilityLevel;
        }

        private void Awake()
        {
            Current = this;
        }

        private void SetAtteptsText()
        {
            _attempts.text = "Failures: " + _attemptCount;
        }

        private void SetAwakenessText()
        {
            _awakeness.text = "Awakeness: " + _awakenessLevel;
        }

        private void SetDrunkenessText()
        {
            _drunkeness.text = "Drunkeness: " + _alcoholLevel;
        }

        private void SetEnergyText()
        {
            _energy.text = "Energy: " + _energyLevel;
        }

        private void SetMentalStabilityText()
        {
            _mentalStability.text = "Mental Stability: " + _mentalStabilityLevel;
        }

        private void SetSuccessesText()
        {
            _successes.text = "Successes: " + _successCount;
        }

        private void Start()
        {
            SetAtteptsText();
            SetSuccessesText();
            SetMentalStabilityText();
            SetDrunkenessText();
            SetAwakenessText();
            SetEnergyText();
        }
    }
}