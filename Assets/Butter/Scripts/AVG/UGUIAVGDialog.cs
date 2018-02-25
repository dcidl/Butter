using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using DAFramework;

namespace Butter.StartMenu
{
    public class UGUIAVGDialog : AVGDialog, IPointerDownHandler
    {
        [SerializeField]
        Text _dialogText;
        [SerializeField]
        Text _nameText;
        [SerializeField]
        string _typingString;
        public override bool isDialogTyping
        {
            get { return _typingString != null && _displayedLength < _typingString.Length; }
        }
        float _displayedLength;
        public override int dialogDisplayLength
        {
            get { return _typingString != null ? (int)_displayedLength : 0; }
            set
            {
                if (_typingString != null)
                {
                    _displayedLength = value;
                    _dialogText.text = _typingString.Substring(0, (int)_displayedLength);
                }
                else
                    _displayedLength = 0;
            }
        }
        [Tooltip("每秒显示多少个字符")]
        [SerializeField]
        float _displaySpeed;
        public override float dialogDisplaySpeed
        {
            get { return _displaySpeed; }
            set
            {
                _displaySpeed = value;
            }
        }
        [SerializeField]
        bool _isPaused;
        public override bool pauseTyping
        {
            get { return _isPaused; }
            set
            {
                _isPaused = value;
            }
        }
        public override void setDialog(string name, string content)
        {
            _nameText.text = name;
            _typingString = content;
            _displayedLength = 0;
        }
        private void Update()
        {
            if (!_isPaused)
            {
                if (_typingString != null)
                {
                    if (_displayedLength < _typingString.Length)
                    {
                        _displayedLength += Time.deltaTime * _displaySpeed;
                        if (_displayedLength > _typingString.Length)
                        {
                            _displayedLength = _typingString.Length;
                        }
                        _dialogText.text = _typingString.Substring(0, (int)_displayedLength);
                    }
                }
            }
        }
        [SerializeField]
        private UnityEvent _onClick = new UnityEvent();
        public override UnityEvent dialogClickedEvent
        {
            get { return _onClick; }
        }
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!eventData.used)
            {
                _onClick.Invoke();
                eventData.Use();
            }
        }
        public override void clearDialog()
        {
            _typingString = null;
            _displayedLength = 0;
            _nameText.text = null;
            _dialogText.text = null;
        }
    }
}
