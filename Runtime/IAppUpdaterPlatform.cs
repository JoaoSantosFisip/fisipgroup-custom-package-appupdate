// Ignore Spelling: App Fisip

using System;
using System.Collections;

namespace FisipGroup.CustomPackage.AppUpdate
{
    /// <summary>
    /// Interface for platform updater methods.
    /// </summary>
    public interface IAppUpdaterPlatform
    {
        /// <summary>
        /// Checks for updates based on the platform.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IEnumerator CheckForUpdates(Action<bool, bool> callback);

        /// <summary>
        /// Update behaviour based on the platform.
        /// </summary>
        public void UpdateBehaviour();
    }
}