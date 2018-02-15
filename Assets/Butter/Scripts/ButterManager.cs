using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using UnityEngine.SceneManagement;

namespace Butter
{
    public class ButterManager : GlobalMainManager
    {
        [HideInInspector]
        [SerializeField]
        string _firstScenePath;
        public override string firstScenePath
        {
            get
            {
                return _firstScenePath;
            }
        }
        [SerializeField]
        StartMenuLoadingUI _startMenuLoadingUIPrefab;
        public override IAsyncOperation onStartLoadingScene(string path)
        {
            if (path == firstScenePath && _startMenuLoadingUIPrefab != null)
            {
                _loadingUI = Instantiate(_startMenuLoadingUIPrefab);
            }
            return base.onStartLoadingScene(path);
        }
        [SerializeField]
        LoadingUI _loadingUI;
        protected override void onLoadingScene(string path, float progress)
        {
            base.onLoadingScene(path, progress);
            if (_loadingUI != null)
            {
                _loadingUI.progress = progress;
            }
        }
        public override void onEndLoadingScene(string path)
        {
            if (_loadingUI != null)
            {
                Destroy(_loadingUI.gameObject);
                _loadingUI = null;
            }
            base.onEndLoadingScene(path);
        }
    }
}
