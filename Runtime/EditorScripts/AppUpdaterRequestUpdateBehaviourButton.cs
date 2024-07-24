using UnityEngine;
using UnityEngine.UI;

namespace FisipGroup.CustomPackage.AppUpdate.EditorScript
{
    /// <summary>
    /// This button requests the app update.
    /// </summary>
    public class AppUpdaterRequestUpdateBehaviourButton : MonoBehaviour
    {
        private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(RequestUpdateBehaviour);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(RequestUpdateBehaviour);
        }
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void RequestUpdateBehaviour()
        {
            AppUpdater.RequestUpdateBehaviour();
        }
    }
}