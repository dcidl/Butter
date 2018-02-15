using UnityEngine;

namespace Butter
{
    public abstract class LoadingUI : MonoBehaviour
    {
        public abstract float progress
        {
            get;
            set;
        }
    }
}
