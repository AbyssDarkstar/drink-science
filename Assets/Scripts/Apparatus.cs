using System;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Apparatus : MonoBehaviour, IComparable, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image _apparatusImage;

        private ApparatusSO _apparatusSo;
        private int _apparatusId = -1;

        [SerializeField]
        private ChemistrySet _chemistrySet;
    
        public int Order;

        public InputOutputLevel InputLevel => _apparatusSo.Input;

        public InputOutputLevel OutputLevel => _apparatusSo.Output;

        public void SetApparatusSO(ApparatusSO apparatusSo)
        {
            _apparatusSo = apparatusSo;
        }

        public ApparatusSO GetApparatusSo()
        {
            return _apparatusSo;
        }

        public FluidStats AdjustStats(FluidStats stats)
        {
            stats.AlcoholicStrength = AdjustStat(stats.AlcoholicStrength, _apparatusSo.AlcoholicStrength);
            stats.CaffeineLevel = AdjustStat(stats.CaffeineLevel, _apparatusSo.CaffineLevel);
            stats.EnergyLevel = AdjustStat(stats.EnergyLevel, _apparatusSo.EnergyLevel);
            stats.MentalStability = AdjustStat(stats.MentalStability, _apparatusSo.MentalStability);
            stats.Sourness = AdjustStat(stats.Sourness, _apparatusSo.Sourness);
            stats.Sweetness = AdjustStat(stats.Sweetness, _apparatusSo.Sweetness);

            return stats;
        }

        private int AdjustStat(int stat, StatMod statMod)
        {
            if (stat == 0)
            {
                return 0;
            }

            switch (statMod.MathmaticalOperator)
            {
                case MathmaticalOperator.Add:
                    return Mathf.Clamp(stat + statMod.Amount, -100, 100);
                case MathmaticalOperator.Subtract:
                    return Mathf.Clamp(stat - statMod.Amount, -100, 100);
                case MathmaticalOperator.Multiply:
                    return Mathf.Clamp(stat * statMod.Amount, -100, 100);
                case MathmaticalOperator.Divide:
                    return Mathf.Clamp(stat / statMod.Amount, -100, 100);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SelectApparatus()
        {
            var appList = Resources.Load<ApparatusListSO>("ApparatusList").ApparatusList;

            _apparatusId++;
            if (_apparatusId >= appList.Count)
            {
                _apparatusId = 0;
            }
            SetApparatusSO(appList[_apparatusId]);

            _apparatusImage.sprite = _apparatusSo.Image;

            _chemistrySet.AddApparatus(this);

            if (_mouseHover)
            {
                AdvancedTooltip.ShowTooltip_Static(_apparatusSo);
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var otherApp = obj as Apparatus;
            if (otherApp != null)
            {
                return Order.CompareTo(otherApp.Order);
            }

            throw new ArgumentException("Object is not an Apparatus.");
        }

        private bool _mouseHover;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _mouseHover = true;
            if (_apparatusSo != null)
            {
                AdvancedTooltip.ShowTooltip_Static(_apparatusSo);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mouseHover = false;

            AdvancedTooltip.HideTooltip_Static();
        }
    }
}