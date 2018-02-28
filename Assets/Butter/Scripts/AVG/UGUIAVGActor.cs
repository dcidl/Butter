using System;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using DAFramework;
using DAFramework.Interfaces;

namespace Butter.StartMenu
{
    public class UGUIAVGActor : AVGActor
    {
        [SerializeField]
        AVGUGUI _ui;
        public AVGUGUI ui
        {
            get { return _ui; }
            set
            {
                _ui = value;
            }
        }
        [SerializeField]
        UGUIAVGAnchor _anchor;
        public override AVGAnchor anchor
        {
            get { return _anchor; }
            set
            {
                _anchor = value as UGUIAVGAnchor;
                if (_anchor != null)
                {
                    transform.SetParent(_anchor.transform);
                    transform.SetAsLastSibling();
                }
                else
                {
                    updatePosition();
                }
            }
        }
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
                if (_character != null)
                {
                    gameObject.name = _character.name;
                }
            }
        }
        [SerializeField]
        Image _image;
        public override Vector2 position
        {
            get { return (_image.rectTransform.anchorMin + _image.rectTransform.anchorMax) / 2; }
            set
            {
                _image.rectTransform.anchorMin = value;
                _image.rectTransform.anchorMax = value;
            }
        }
        IAVGExpression _expression;
        public override IAVGExpression expression
        {
            get { return _expression; }
            set
            {
                _expression = value;
                if (_expression != null)
                {
                    _image.sprite = _expression.sprite;
                    if (_image.sprite != null)
                    {
                        _image.enabled = true;
                        updateSize(value);
                        updatePosition();
                    }
                    else
                    {
                        _image.enabled = false;
                    }
                }
                else
                {
                    _image.enabled = false;
                }
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
        private void Update()
        {
            if (expression != null)
            {
                updateSize(expression);
                updatePosition();
            }
            else
                _image.enabled = false;
        }
        void updateSize(IAVGExpression standPic)
        {
            if (standPic == null || standPic.sprite == null)
                return;
            float maxWidth = ui.size.x * standPic.maxSize.x;
            float maxHeight = ui.size.y * standPic.maxSize.y;
            float picWidth = standPic.sprite.rect.width;
            float picHeight = standPic.sprite.rect.height;

            if (picWidth < maxWidth)
            {
                picHeight *= maxWidth / picWidth;
                picWidth = maxWidth;
            }
            if (picHeight < maxHeight)
            {
                picWidth *= maxHeight / picHeight;
                picHeight = maxHeight;
            }
            if (picWidth > maxWidth)
            {
                picHeight *= maxWidth / picWidth;
                picWidth = maxWidth;
            }
            if (picHeight > maxHeight)
            {
                picWidth *= maxHeight / picHeight;
                picHeight = maxHeight;
            }

            _image.rectTransform.sizeDelta = new Vector2(picWidth, picHeight);
        }
        void updatePosition()
        {
            if (anchor == null)
            {
                _image.rectTransform.anchoredPosition = new Vector3(0, _image.rectTransform.sizeDelta.y / 2, 0);
            }
            else
            {
                float x = anchor.offset.x * transform.GetSiblingIndex() * ui.size.x;
                float y = anchor.offset.y * transform.GetSiblingIndex() * ui.size.y;
                _image.rectTransform.anchoredPosition = new Vector3(x, _image.rectTransform.sizeDelta.y / 2 + y, 0);
            }
        }

        public override AVGActorSave save()
        {
            return new AVGActorSave()
            {
                characterName = character != null ? character.name : string.Empty,
                standPicName = expression != null ? expression.name : string.Empty,
                position = position,
                color = color,
                flipX = flipX,
                flipY = flipY,
                sortOrder = sortOrder,
                anchorIndex = anchor == null ? -1 : Array.IndexOf(ui.anchors, anchor)
            };
        }

        public override void load(AVGActorSave save)
        {
            for (int i = 0; i < ui.manager.script.characters.Length; i++)
            {
                if (ui.manager.script.characters[i].name == save.characterName)
                {
                    character = ui.manager.script.characters[i] as IAVGCharacter;
                }
            }
            if (character != null)
            {
                for (int i = 0; i < character.expressions.Length; i++)
                {
                    if (character.expressions[i].name == save.standPicName)
                    {
                        expression = character.expressions[i];
                    }
                }
            }
            else
                expression = null;
            position = save.position;
            color = save.color;
            flipX = save.flipX;
            flipY = save.flipY;
            sortOrder = sortOrder;
            anchor = save.anchorIndex < 0 ? null : ui.anchors[save.anchorIndex];
        }
    }
}