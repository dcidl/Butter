
using UnityEngine;
using UnityEngine.UI;

using DAFramework;

namespace Butter.StartMenu
{
    public class UGUIAVGOption : MonoBehaviour
    {
        [SerializeField]
        Button _button;
        [SerializeField]
        Image _image;
        [SerializeField]
        Text _text;
        private void Awake()
        {
            _button.onClick.AddListener(() =>
            {
                Debug.Log("选项被点击", this);
                _onClick.Invoke(_text.text);
            });
        }
        private void OnEnable()
        {
            _button.enabled = true;
            _text.enabled = true;
            _image.enabled = true;
        }
        private void OnDisable()
        {
            _button.enabled = false;
            _text.enabled = false;
            _image.enabled = false;
        }
        public void display(string text)
        {
            _image.enabled = true;
            _button.enabled = true;
            _text.enabled = true;
            _text.text = text;
        }
        public void hide()
        {
            _image.enabled = false;
            _button.enabled = false;
            _text.enabled = false;
            _text.text = null;
        }
        [SerializeField]
        OptionClickedEvent _onClick = new OptionClickedEvent();
        public OptionClickedEvent onClick
        {
            get { return _onClick; }
        }
    }
}