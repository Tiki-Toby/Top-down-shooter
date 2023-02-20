using Game.Ui.WindowManager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LoseWindow
{
    public class LoseWindowView : Window
    {
        [SerializeField] private Button _tryAgainButton;
        
        public override bool IsPoolable => true;

        protected override void OnOpenCompleted()
        {
        }
    }
}