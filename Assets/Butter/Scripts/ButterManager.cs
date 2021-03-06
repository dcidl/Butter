﻿using System.Collections;

using UnityEngine;

using GameFramework;

namespace Butter
{
    public class ButterManager : GlobalMainManager
    {
        [SerializeField]
        [EditorEx.ScenePathField]
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
        protected override IAsyncOperation onStartLoadingScene(string path)
        {
            if (path == firstScenePath && _startMenuLoadingUIPrefab != null)
            {
                _loadingUI = Instantiate(_startMenuLoadingUIPrefab);
            }
            else if (path == _gameScenePath && _startMenuLoadingUIPrefab != null)
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
        protected override void onEndLoadingScene(string path)
        {
            if (_loadingUI != null)
            {
                Destroy(_loadingUI.gameObject);
                _loadingUI = null;
            }
            base.onEndLoadingScene(path);
        }
        [SerializeField]
        [EditorEx.ScenePathField]
        string _gameScenePath;
        public void newGame()
        {
            loadScene(_gameScenePath);
        }
        public void quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
