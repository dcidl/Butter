
using UnityEngine;
using UnityEngine.UI;

using GameFramework;

namespace Butter
{
    public class StartMenuManager : LocalMainManager
    {
        [SerializeField]
        Button _newGameButton;
        [SerializeField]
        Button _exitButton;
        protected override void onInit(LocalManager manager)
        {
            _exitButton.onClick.AddListener((global as ButterManager).quit);
            base.onInit(manager);
        }
    }
}
