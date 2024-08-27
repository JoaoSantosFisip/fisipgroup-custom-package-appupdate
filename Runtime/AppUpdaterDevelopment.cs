// Ignore Spelling: Fisip App

using FisipGroup.CustomPackage.Tools.Helpers;
using System;
using System.Collections;
using UnityEngine;

namespace FisipGroup.CustomPackage.AppUpdate
{
    public class AppUpdaterDevelopment : IAppUpdaterPlatform
    {
        public IEnumerator CheckForUpdates(Action<bool, bool> callback)
        {
            yield return null;

            Debug.Log("AppUpdaterDevelopment.cs: Skipping updates since it's a development build or editor");

            callback.Invoke(true, (HelperCustomPackage.GetInfoFile<AppUpdaterInfoScriptableObject>("AppUpdate") as AppUpdaterInfoScriptableObject).setUpdateAvailable);
        }

        public void UpdateBehaviour()
        {
            // No update behaviour. 
            Debug.LogWarning("AppUpdaterDevelopment.cs: Button click");
        }
    }
}
