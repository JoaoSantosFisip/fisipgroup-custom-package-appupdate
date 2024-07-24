using TMPro;
using UnityEngine;

namespace FisipGroup.CustomPackage.AppUpdate.EditorScript
{
    /// <summary>
    /// Displays the current app version.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]  
    public class AppUpdaterVersionDisplay : MonoBehaviour
    {
        private void Awake()
        {
            if(TryGetComponent<TextMeshProUGUI>(out var component))
            {
                component.text = Application.version;
            }
            else
            {
                Debug.LogError("AppUpdaterVersionDisplay.cs: No component to display the version.");
            }
        }
    }
}

