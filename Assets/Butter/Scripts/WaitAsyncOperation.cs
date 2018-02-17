using UnityEngine;

using GameFramework;

namespace Butter
{
    public class WaitAsyncOperation : IAsyncOperation
    {
        float _startTime;
        float _duration;
        public WaitAsyncOperation(float duration)
        {
            _startTime = -1;
            _duration = duration;
        }
        public void start()
        {
            _startTime = Time.time;
        }
        public float progress
        {
            get
            {
                return _startTime < 0 ? 0 : ((Time.time - _startTime) / _duration);
            }
        }
        public bool isDone
        {
            get
            {
                return _startTime >= 0 && (Time.time - _startTime) >= _duration;
            }
        }

        public float estimatedTime
        {
            get
            {
                return _duration;
            }
        }
    }
}
