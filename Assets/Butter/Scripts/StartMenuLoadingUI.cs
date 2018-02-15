using UnityEngine;
using UnityEngine.UI;

using GameFramework;

namespace Butter
{
    public class StartMenuLoadingUI : LoadingUI
    {
        [SerializeField]
        Slider _progressBar;
        public override float progress
        {
            get
            {
                return _progressBar.normalizedValue;
            }

            set
            {
                _progressBar.normalizedValue = value;
            }
        }
    }
}
