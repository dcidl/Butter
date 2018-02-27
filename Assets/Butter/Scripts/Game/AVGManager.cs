using System.IO;

using UnityEngine;
using UnityEngine.UI;

using GameFramework;
using DAFramework.Behaviours;

namespace Butter.Game
{
    public class AVGManager : LocalSubManager
    {
        [SerializeField]
        AVGBehaviourManager _AVG;
        [SerializeField]
        Button _quickSaveButton;
        [SerializeField]
        Button _quickLoadButton;
        protected override void onAwake()
        {
            base.onAwake();
            if (_quickSaveButton != null)
            {
                _quickSaveButton.onClick.AddListener(onSaveAVG);
            }
            else
                Debug.LogError(this + " 丢失对快速保存按钮的引用，快速保存按钮将失效", this);
            if (_quickLoadButton != null)
            {
                _quickLoadButton.onClick.AddListener(onLoadAVG);
            }
            else
                Debug.LogError(this + " 丢失对快速读取按钮的引用，快速读取按钮将失效", this);
        }
        private void onSaveAVG()
        {
            if (_AVG != null)
            {
                string path = Application.streamingAssetsPath + "/" + "AVGQuickSave.json";
                using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create)))
                {
                    var save = _AVG.save();
                    if (save != null)
                    {
                        string json = JsonUtility.ToJson(save);
                        if (!string.IsNullOrEmpty(json))
                        {
                            writer.Write(json);
                        }
                    }
                }
            }
            else
                Debug.LogError(this + " 丢失对AVG管理器的引用，无法快速保存存档", this);
        }
        private void onLoadAVG()
        {
            if (_AVG != null)
            {
                string path = Application.streamingAssetsPath + "/" + "AVGQuickSave.json";
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
                    {
                        string json = reader.ReadToEnd();
                        AVGManagerSave save = JsonUtility.FromJson<AVGManagerSave>(json);
                        if (save != null)
                        {
                            _AVG.load(save);
                        }
                        else
                            Debug.LogError("读取快速存档文件发生错误！");
                    }
                }
                else
                    Debug.LogWarning("快速存档文件不存在", this);
            }
            else
                Debug.LogError(this + " 丢失对AVG管理器的引用，无法快速读取存档", this);
        }
        [SerializeField]
        Sprite _sprite;
        [EditorEx.ResourcesPath("_sprite")]
        [SerializeField]
        string _spritePath;
    }
}
