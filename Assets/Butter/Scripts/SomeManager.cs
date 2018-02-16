using UnityEngine;

using GameFramework;

namespace Butter
{
    public class SomeManager : GlobalSubManager
    {
        protected override void onAwake()
        {
            base.onAwake();
            Debug.Log("一些和外界无关所以无所谓什么时候运行的初始化。", this);
        }
        protected override void onInit(GlobalManager manager)
        {
            base.onInit(manager);
            Debug.Log("一些必须要" + manager + "才能进行的初始化。", manager);
        }
        protected override void onStart()
        {
            base.onStart();
            Debug.Log("一些必须要其他管理器都初始化完了才能进行的逻辑", this);
        }
        protected override IAsyncOperation onStartLoadingScene(string path)
        {
            return new WaitAsyncOperation(5);
        }
    }
    public class WaitAsyncOperation : IAsyncOperation
    {
        float _startTime;
        float _duration;
        public WaitAsyncOperation(float duration)
        {
            _startTime = Time.time;
            _duration = duration;
        }
        public float progress
        {
            get
            {
                return (Time.time - _startTime) / _duration;
            }
        }
        public bool isDone
        {
            get
            {
                return (Time.time - _startTime) >= _duration;
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
