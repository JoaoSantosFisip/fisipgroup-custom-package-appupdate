// Ignore Spelling: Fisip App

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Unity.Services.RemoteConfig;

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
                    if(int.TryParse(Application.version, out var currentVersion))
                    {
                        var updatesJSON = RemoteConfigService.Instance.appConfig.GetJson("Versions");
                        var wrapper = JsonUtility.FromJson<AppVersionWrapper>(updatesJSON);

                        foreach (var version in wrapper.versions)
                        {
                            if(currentVersion < int.Parse(version.number))
                            {
                                MajorUpdateAvailable = true;
                                
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("AppUpdater.cs: Invalid version number: " + Application.version);
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