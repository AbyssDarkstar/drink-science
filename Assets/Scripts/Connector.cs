using System;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Connector : MonoBehaviour, IComparable
    {
        [SerializeField]
        private Sprite _none;

        [SerializeField]
        private Sprite _h2H;
        [SerializeField]
        private Sprite _h2M;
        [SerializeField]
        private Sprite _h2L;

        [SerializeField]
        private Sprite _m2H;
        [SerializeField]
        private Sprite _m2M;
        [SerializeField]
        private Sprite _m2L;

        [SerializeField]
        private Sprite _l2H;
        [SerializeField]
        private Sprite _l2M;
        [SerializeField]
        private Sprite _l2L;

        private Sprite[,] _connectors;

        public int Order;

        private void Start()
        {
            _connectors = new Sprite[3, 3];
            _connectors[0, 0] = _h2H;
            _connectors[1, 0] = _h2M;
            _connectors[2, 0] = _h2L;

            _connectors[0, 1] = _m2H;
            _connectors[1, 1] = _m2M;
            _connectors[2, 1] = _m2L;

            _connectors[0, 2] = _l2H;
            _connectors[1, 2] = _l2M;
            _connectors[2, 2] = _l2L;
        }

        public void SetConnectorImage(InputOutputLevel left, InputOutputLevel right)
        {
            if (left == InputOutputLevel.None || right == InputOutputLevel.None)
            {
                GetComponent<Image>().sprite = _none;
            }

            var x = 0;
            var y = 0;

            switch (left)
            {
                case InputOutputLevel.Med:
                    x = 1;
                    break;
                case InputOutputLevel.Low:
                    x = 2;
                    break;
            }

            switch (right)
            {
                case InputOutputLevel.Med:
                    y = 1;
                    break;
                case InputOutputLevel.Low:
                    y = 2;
                    break;
            }

            GetComponent<Image>().sprite = _connectors[y, x];
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var otherConnector = obj as Connector;
            if (otherConnector != null)
            {
                return Order.CompareTo(otherConnector.Order);
            }

            throw new ArgumentException("Object is not a Connector.");
        }
    }
}