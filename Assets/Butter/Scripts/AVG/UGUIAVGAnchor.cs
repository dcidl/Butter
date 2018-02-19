using System.Collections.Generic;

using UnityEngine;

using DAFramework;

namespace Butter.StartMenu
{
    /// <summary>
    /// Anchor的作用是保存锚点信息（通过RectTransform），通过SiblingIndex来实现Actor之间的显示顺序，并按照显示顺序来设置同一个Anchor下的Actor的偏移量。
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class UGUIAVGAnchor : MonoBehaviour
    {
        public void addActor(AVGActor actor)
        {
            if (actor is UGUIAVGActor)
            {
                actor.transform.parent = transform;
                actor.transform.SetAsLastSibling();
            }
        }
        [SerializeField]
        Vector2 _offset;
        public Vector2 offset
        {
            get { return _offset; }
        }
    }
}