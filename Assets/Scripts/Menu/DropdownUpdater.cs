using TMPro;
using UnityEngine;

namespace Menu
{
    public class DropdownUpdater : MonoBehaviour
    {
        [SerializeField] public TMP_Dropdown[] resolutionDropdowns;
        [SerializeField] public TMP_Dropdown[] screenModeDropdowns;
        
        public void UpdateResolutionDropdowns(int newValue)
        {
            foreach (var dropdown in resolutionDropdowns)
            {
                dropdown.value = newValue;
                dropdown.RefreshShownValue();
            }
        }

        public void UpdateScreenModeDropdowns(int newValue)
        {
            foreach (var dropdown in screenModeDropdowns)
            {
                dropdown.value = newValue;
                dropdown.RefreshShownValue();
            }
        }
    }
}