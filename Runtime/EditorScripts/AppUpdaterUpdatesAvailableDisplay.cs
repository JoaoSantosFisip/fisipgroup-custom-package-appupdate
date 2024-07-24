using UnityEngine;
using FisipGroup.CustomPackage.Tools.Extensions;

namespace FisipGroup.CustomPackage.AppUpdate.EditorScript
{
    /// <summary>
    /// Makes a CanvasGroup visible when an update is available
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class AppUpdaterUpdatesAvailableDisplay : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.SetVisibility(false);
        }
        private void Start()
        {
            _canvasGroup.SetVisibility(AppUpdater.HasUpdates);
        }
    }
}