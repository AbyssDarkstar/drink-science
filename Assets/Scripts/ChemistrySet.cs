using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class ChemistrySet : MonoBehaviour
    {
        [SerializeField]
        private StartBeaker _startBeaker;

        [SerializeField]
        private List<Apparatus> _apparatus = new List<Apparatus>();

        [SerializeField]
        private List<Connector> _connectors = new List<Connector>();

        [SerializeField]
        private int _apparatusCount;
    
        [SerializeField]
        private EndBeaker _endBeaker;

        public int RemainingSpaceInEnd => _endBeaker.RemainingSpace;

        private void Start()
        {
            _connectors.Sort();
        }

        public bool IsFullyConnected()
        {
            return _apparatusCount == _apparatus.Count;
        }

        public void AddApparatus(Apparatus apparatus)
        {
            if (_apparatus.Contains(apparatus))
            {
                _apparatus.Remove(apparatus);
            }

            _apparatus.Add(apparatus);

            _apparatus.Sort();
        
            if (apparatus.Order == 0)
            {
                _connectors[apparatus.Order].SetConnectorImage(_startBeaker.Output, apparatus.InputLevel);

                var appToTheRight = _apparatus.FirstOrDefault(a => a.Order == 2);

                if (appToTheRight != null)
                {
                    _connectors[apparatus.Order].SetConnectorImage(apparatus.OutputLevel, appToTheRight.InputLevel);
                }
            }
            else if (apparatus.Order == _apparatusCount - 1)
            {
                _connectors[apparatus.Order + 1].SetConnectorImage(apparatus.OutputLevel, _endBeaker.Input);

                var appToTheLeft = _apparatus.FirstOrDefault(a => a.Order == apparatus.Order - 1);

                if (appToTheLeft != null)
                {
                    _connectors[apparatus.Order].SetConnectorImage(appToTheLeft.OutputLevel, apparatus.InputLevel);
                }
            }
            else
            {
                var appToTheRight = _apparatus.FirstOrDefault(a => a.Order == apparatus.Order + 1);

                if (appToTheRight != null)
                {
                    _connectors[apparatus.Order + 1].SetConnectorImage(apparatus.OutputLevel, appToTheRight.InputLevel);
                }

                var appToTheLeft = _apparatus.FirstOrDefault(a => a.Order == apparatus.Order - 1);

                if (appToTheLeft != null)
                {
                    _connectors[apparatus.Order].SetConnectorImage(appToTheLeft.OutputLevel, apparatus.InputLevel);
                }
            }
        }

        public void ApplyAdjustments(FluidStats stats, int qty)
        {
            if (_endBeaker.RemainingSpace >= qty)
            {
                foreach (var apparatus in _apparatus)
                {
                    stats = apparatus.AdjustStats(stats);
                }

                _endBeaker.SendIncomingFluid(stats, qty);
            }
        }
    }
}
