// Ignore Spelling: Fisip App
#if UNITY_ANDROID

using System.Collections;
using UnityEngine;
using System;
using Google.Play.AppUpdate;
using Unity.VisualScripting;
using FisipGroup.CustomPackage.AppUpdate.Helpers;

namespace FisipGroup.CustomPackage.AppUpdate
{
    public class AppUpdaterAndroid : IAppUpdaterPlatform
    {
        private AppUpdateManager _updateManager;
        private AppUpdateInfo _info;

        public IEnumerator CheckForUpdates(Action<bool, bool> callback)
        {
            _updateManager = new AppUpdateManager();

            var operation = _updateManager.GetAppUpdateInfo();

            while (!operation.IsDone)
            {
                yield return null;
            }

            if (operation.IsSuccessful)
            {
                _info = operation.GetResult();

                if (_info.UpdateAvailability == UpdateAvailability.UpdateAvailable)
                {
                    Debug.Log("AppUpdaterAndroid.cs: Updates available");

                    callback?.Invoke(true, true);
                }
                else
                {
                    Debug.Log("AppUpdaterAndroid.cs: No Updates available: " + _info.UpdateAvailability.ToString());

                    callback?.Invoke(true, false);
                }
            }
            else
            {
                if (HelperAppUpdater.InstalledFromStore())
                {
                    Debug.LogError("AppUpdaterAndroid.cs: Error updating app");

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
        }

        public void UpdateBehaviour()
        {
            CoroutineRunner.instance.StartCoroutine(StartImmediateUpdate());
        }

        private IEnumerator StartImmediateUpdate()
        {
            Debug.Log("AppUpdaterAndroid.cs: Forcing APP update");

            var immediateUpdateRequest = _updateManager.StartUpdate(_info, AppUpdateOptions.ImmediateAppUpdateOptions());

            yield return immediateUpdateRequest;

            // If this line is reacheded there was an issue with the app update
            Debug.LogError("AppUpdaterAndroid.cs: Error trying to update APP: " + immediateUpdateRequest.Error);
        }
    }
}
#endif