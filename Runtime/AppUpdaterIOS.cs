using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using FisipGroup.CustomPackage.AppUpdate.Helpers;
using FisipGroup.CustomPackage.Tools.Helpers;

namespace FisipGroup.CustomPackage.AppUpdate
{
    public class AppUpdaterIOS : IAppUpdaterPlatform
    {
        private const string APP_STORE_URL = "https://itunes.apple.com/lookup?bundleId=";

        public IEnumerator CheckForUpdates(Action<bool, bool> callback)
        {
            var request = UnityWebRequest.Get(APP_STORE_URL + Application.identifier);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                AppStoreResponse info = JsonUtility.FromJson<AppStoreResponse>(request.downloadHandler.text);

                if (info.results != null && info.results.Length > 0)
                {
                    var latestVersion = info.results[0].version;

                    if (IsDifferentVersion(latestVersion, Application.version)
                        && HelperAppUpdater.InstalledFromStore())
                    {
                        Debug.Log("AppUpdaterIOS.cs: Updates available");

                        callback?.Invoke(true, true);
                    }
                    else
                    {
                        Debug.Log("AppUpdaterIOS.cs: No Updates available");

                        callback?.Invoke(true, false);
                    }
                }
                else
                {
                    HandleVersionRequestFailure(callback);
                }
            }
            else
            {
                HandleVersionRequestFailure(callback);
            }
        }
        private void HandleVersionRequestFailure(Action<bool, bool> callback)
        {
            if (HelperAppUpdater.InstalledFromStore())
            {
                Debug.LogError("AppUpdaterIOS.cs: Error updating app");

                callback?.Invoke(false, false);
            }
            else
            {
                //Debug.LogError("VersionCheck.cs: Error getting APP update info... " +
                //"Normally occurs on building APKS that are not in development mode " +
                //"since builds in development mode surpass this check for updates." +
                //"This build should work without issues on release and internal release. (Test on internal release)");

                callback?.Invoke(true, false);
            }
        }

        private static bool IsDifferentVersion(string latestVersion, string currentVersion)
        {
            return latestVersion != currentVersion;
        }

        public void UpdateBehaviour()
        {
            var info = HelperCustomPackage.GetInfoFile<AppUpdaterInfoScriptableObject>("AppUpdate") as AppUpdaterInfoScriptableObject;

            Application.OpenURL($"itms-apps://itunes.apple.com/us/app/id{info.appleAppID}");
        }

        [Serializable]
        public class AppInfo
        {
            public string version;
        }

        [Serializable]
        public class AppStoreResponse
        {
            public AppInfo[] results;
        }
    }
}
