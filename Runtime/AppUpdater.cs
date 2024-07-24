// Ignore Spelling: Fisip App

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace FisipGroup.CustomPackage.AppUpdate
{
    public static class AppUpdater
    {
        public static readonly UnityEvent<bool> OnUpdatesCheck = new();

        private static IAppUpdaterPlatform Updater;

        public static bool HasUpdates = false;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
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

                OnUpdatesCheck?.Invoke(success);
            }));
        }

        public static void RequestUpdateBehaviour()
        {
            Updater.UpdateBehaviour();
        }
    }
}