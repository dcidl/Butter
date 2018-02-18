using UnityEngine;
using UnityEngine.UI;

using DAFramework;

namespace Butter.StartMenu
{
    public class UGUIAVGBackground : AVGBackground
    {
        [SerializeField]
        Image _image;
        public override Sprite sprite
        {
            get
            {
                return _image.sprite;
            }

            set
            {
                _image.sprite = value;
            }
        }
    }
}
