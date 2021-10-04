using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Objective")]
    public class ObjectiveSO : ScriptableObject
    {
        public bool AlcoholAny = false;

        public int AlcoholLevelHigh = 0;

        public int AlcoholLevelLow = 0;

        public bool CaffeineAny = false;

        public int CaffeineLevelHigh = 0;

        public int CaffeineLevelLow = 0;

        public bool EnergyAny = false;

        public int EnergyLevelHigh = 0;

        public int EnergyLevelLow = 0;

        public bool SourAny = false;

        public int SourLevelHigh = 0;

        public int SourLevelLow = 0;

        public bool SweetAny = false;

        public int SweetLevelHigh = 0;

        public int SweetLevelLow = 0;
    }
}