// Ignore Spelling: Fisip App

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Unity.Services.RemoteConfig;
using System.Linq.Expressions;
using System;

namespace FisipGroup.CustomPackage.AppUpdate
{
    public static class AppUpdater
    {
        public static readonly UnityEvent<bool> OnUpdatesCheck = new();

        private static IAppUpdaterPlatform Updater;

        public static bool MajorUpdateAvailable = false;
        public static bool HasUpdates = false;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AddListeners()
        {
            RemoteConfigService.Instance.FetchCompleted += Initialize;
        }

        private static void Initialize(ConfigResponse response)
        {
            Updater
#if DEVELOPMENT_BUILD || UNITY_EDITOR
                = new AppUpdaterDevelopment();
#elif UNITY_ANDROID
                = new AppUpdaterAndroid();
#elif UNITY_IOS
                = new AppUpdaterIOS();
#endif

            CoroutineRunner.instance.StartCoroutine(Updater.CheckForUpdates((success, hasUpdates) => 
            {
                HasUpdates = hasUpdates;

                // Check if a major update is available
                if (HasUpdates)
                {
                    try
                    {
                        if (int.TryParse(Application.version, out var currentVersions))
                        {
                            var updatesJSON = RemoteConfigService.Instance.appConfig.GetJson("MajorVersions");
                            var majorUpdates = JsonUtility.FromJson<AppVersionWrapper>(updatesJSON).versions;

                            foreach (var version in majorUpdates)
                            {
                                if (currentVersions < int.Parse(version))
                                {
                                    MajorUpdateAvailable = true;

                                    Debug.LogWarning("AppUpdater.cs: Major update available");

                                    break;
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("AppUpdater.cs: Invalid version number: " + Application.version);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("AppUpdater.cs: Error getting JSON data: " + ex.Message);
                    }
                }

                OnUpdatesCheck?.Invoke(success);
            }));
        }

        public static void RequestUpdateBehaviour()
        {
            Updater.UpdateBehaviour();
        }
    }
}