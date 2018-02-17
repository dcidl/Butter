
using UnityEngine;
using UnityEngine.UI;

using GameFramework;

namespace Butter.StartMenu
{
    public class UIManager : LocalSubManager
    {
        [SerializeField]
        Canvas _canvas;
        [SerializeField]
        Button _newGameButton;
        [SerializeField]
        Button _exitButton;
        protected override void onInit(LocalManager manager)
        {
            base.onInit(manager);
            _newGameButton.onClick.AddListener((global as ButterManager).newGame);
            _exitButton.onClick.AddListener((global as ButterManager).quit);
        }
        public override IAsyncOperation unload()
        {
            _canvas.enabled = false;
            return base.unload();
        }
    }
}
