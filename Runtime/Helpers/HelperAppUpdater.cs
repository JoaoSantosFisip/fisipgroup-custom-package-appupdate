using UnityEngine;

namespace FisipGroup.CustomPackage.AppUpdate.Helpers
{
    public class HelperAppUpdater : MonoBehaviour
    {
        /// <summary>
        /// Checks if the app was installed via app store.
        /// </summary>
        /// <returns></returns>
        public static bool InstalledFromStore()
        {
            return Application.identifier.Contains("com.android.vending")
                || Application.identifier.Contains("com.apple.appstore");
        }
    }
}