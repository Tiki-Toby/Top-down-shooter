using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Loading
{
    public class LoadingWindowView : MonoBehaviour
    {
        [SerializeField] private Image _fillLoadingImage;
        [SerializeField] private TMP_Text _percentInfoText;

        public Image FillLoadingImage => _fillLoadingImage;
        public TMP_Text PercentInfoText => _percentInfoText;
    }
}
