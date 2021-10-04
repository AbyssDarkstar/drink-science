using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Apparatus")]
    public class ApparatusSO : ScriptableObject
    {
        public string ApparatusName;

        public Sprite Image;

        public InputOutputLevel Input;

        public InputOutputLevel Output;

        public StatMod AlcoholicStrength;

        public StatMod CaffineLevel;

        public StatMod EnergyLevel;

        public StatMod MentalStability;

        public StatMod Sourness;

        public StatMod Sweetness;

        public string ApparatusDescription;
    }

    public enum InputOutputLevel
    {
        None,
        High,
        Med,
        Low
    }
}