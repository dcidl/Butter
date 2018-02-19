using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using DAFramework;
using DAFramework.Interfaces;

namespace Butter.StartMenu
{
    public class AVGUGUI : AVGUI
    {
        public override bool enableInteraction
        {
            get { return false; }
            set
            {
                //TODO:实现禁用交互
            }
        }
        [SerializeField]
        List<UGUIAVGActor> _actors;
        public override AVGActor[] actors
        {
            get
            {
                return _actors.ToArray();
            }
            set
            {
                _actors.Clear();
                _actors.AddRange(value.Where(e => { return e is UGUIAVGActor; }).Select(e => { return e as UGUIAVGActor; }));
            }
        }
        [SerializeField]
        OptionClickedEvent _onOptionClicked = new OptionClickedEvent();
        public override OptionClickedEvent optionClickedEvent
        {
            get { return _onOptionClicked; }
        }
        [SerializeField]
        Canvas _root;
        public Vector2 size
        {
            get
            {
                return (_root.transform as RectTransform).rect.size;
            }
        }
        public override bool isDisplaying
        {
            get { return _root.enabled; }
            set
            {
                _root.enabled = value;
            }
        }
        public override bool isFreezed
        {
            get
            {
                return dialog.pauseTyping;
            }
            set
            {
                dialog.pauseTyping = value;
            }
        }
        public override void clear()
        {
            clearActors();
            dialog.clearDialog();
            clearOptions();
        }
        public override void clearActors()
        {
            for (int i = 0; i < _actors.Count; i++)
            {
                if (_actors[i] != null)
                    Destroy(_actors[i].gameObject);
            }
            _actors.Clear();
        }
        [SerializeField]
        UGUIAVGActor _actorPrefab;
        [SerializeField]
        RectTransform _actorRoot;
        [SerializeField]
        UGUIAVGAnchor[] _anchors;
        public UGUIAVGAnchor[] anchors
        {
            get { return _anchors; }
        }
        public override AVGActor getOrCreateActor(IAVGCharacter character)
        {
            for (int i = 0; i < _actors.Count; i++)
            {
                if (_actors[i] != null && _actors[i].character == character)
                {
                    return _actors[i];
                }
            }
            if (_actorPrefab != null && _actorRoot != null)
            {
                UGUIAVGActor actor = Instantiate(_actorPrefab, _actorRoot);
                actor.ui = this;
                actor.character = character;
                return actor;
            }
            else
            {
                Debug.LogError("丢失对ActorPrefab或ActorRoot的引用！", this);
                return null;
            }
        }
        [SerializeField]
        UGUIAVGOption[] _options;
        private void Awake()
        {
            for (int i = 0; i < _options.Length; i++)
            {
                _options[i].onClick.AddListener(e =>
                {
                    Debug.Log(e, _options[i]);
                    _onOptionClicked.Invoke(e);
                });
            }
        }
        public override void setOptions(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                if (i < _options.Length)
                {
                    _options[i].display(options[i]);
                }
                else
                {
                    Debug.LogWarning("由于UI中的选项不够，选项" + options[i] + "无法被显示", this);
                }
            }
        }
        public override void clearOptions()
        {
            for (int i = 0; i < _options.Length; i++)
            {
                _options[i].hide();
            }
        }
    }
}