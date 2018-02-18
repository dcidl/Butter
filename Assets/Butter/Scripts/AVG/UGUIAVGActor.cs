
using UnityEngine;
using UnityEngine.UI;

using DAFramework;
using DAFramework.Interfaces;

namespace Butter.StartMenu
{
    public class UGUIAVGActor : AVGActor
    {
        IAVGCharacter _character;
        public override IAVGCharacter character
        {
            get
            {
                return _character;
            }
            set
            {
                _character = value;
            }
        }
        [SerializeField]
        Image _image;
        public override Vector2 position
        {
            get { return _image.rectTransform.anchoredPosition; }
            set
            {
                _image.rectTransform.anchoredPosition = value;
            }
        }
        IAVGExpression _expression;
        public override IAVGExpression expression
        {
            get { return _expression; }
            set
            {
                _expression = value;
            }
        }
        public override Color color
        {
            get { return _image.color; }
            set
            {
                _image.color = value;
            }
        }
        public override bool flipX
        {
            get { return _image.rectTransform.localEulerAngles.y != 0; }
            set
            {
                _image.rectTransform.localEulerAngles = new Vector3(0, value ? 180 : 0, _image.rectTransform.localEulerAngles.z);
            }
        }
        public override bool flipY
        {
            get { return _image.rectTransform.localEulerAngles.z != 0; }
            set
            {
                _image.rectTransform.localEulerAngles = new Vector3(0, _image.rectTransform.localEulerAngles.y, value ? 180 : 0);
            }
        }
        public override int sortOrder
        {
            get { return _image.rectTransform.GetSiblingIndex(); }
            set
            {
                _image.rectTransform.SetSiblingIndex(value);
            }
        }

        public override void setAnchor(int i)
        {
            //TODO:实现UGUIAVGActor的设置Anchor。
        }
    }
}