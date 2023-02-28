using UnityEngine;
using UnityEngine.UI;

namespace UI.Bars
{
    public class HpBarView : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        public void SetValue(float value)
        {
            fillImage.fillAmount = value;
        }
    }
}