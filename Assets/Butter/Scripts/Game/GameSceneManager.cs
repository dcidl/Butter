using System.Collections.Generic;

using UnityEngine;

using GameFramework;
using DAFramework.Behaviours;

namespace Butter.Game
{
    public class GameSceneManager : LocalMainManager
    {
        [SerializeField]
        AVGBehaviourManager _AVG;
        [Multiline]
        [SerializeField]
        List<string> _saves = new List<string>();
        private void OnGUI()
        {
            if (_AVG != null)
            {
                if (GUILayout.Button("保存"))
                {
                    _saves.Add(JsonUtility.ToJson(_AVG.save()));
                }
                for (int i = 0; i < _saves.Count; i++)
                {
                    if (GUILayout.Button("加载存档 " + i))
                    {
                        _AVG.load(JsonUtility.FromJson<AVGManagerSave>(_saves[i]));
                    }
                }
            }
        }
    }
}
