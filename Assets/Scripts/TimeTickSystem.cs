using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class TimeTickSystem
    {
        private const float TickTimerMax = 0.2f;

        private static int _tick;

        private static GameObject _timeTickSystemGameObject;

        public static event EventHandler<OnTickEventArgs> OnSecondElapsed;

        public static event EventHandler<OnTickEventArgs> OnTickElapsed;

        public static void Create()
        {
            if (_timeTickSystemGameObject == null)
            {
                _timeTickSystemGameObject = new GameObject("TimeTickSystem");
                _timeTickSystemGameObject.AddComponent<TimeTickSystemBehaviour>();
            }
        }

        public class OnTickEventArgs : EventArgs
        {
            public int Tick { get; set; }
        }

        private class TimeTickSystemBehaviour : MonoBehaviour
        {
            private float _tickTimer;

            private void Awake()
            {
                _tick = 0;
            }

            private void Update()
            {
                _tickTimer += Time.deltaTime;

                if (_tickTimer >= TickTimerMax)
                {
                    _tickTimer -= TickTimerMax;
                    _tick++;

                    OnTickElapsed?.Invoke(this, new OnTickEventArgs { Tick = _tick });

                    if (_tick % 5 == 0)
                    {
                        OnSecondElapsed?.Invoke(this, new OnTickEventArgs { Tick = _tick });
                    }
                }
            }
        }
    }
}