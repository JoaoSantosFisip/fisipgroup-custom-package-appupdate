using UnityEngine;

namespace FisipGroup.CustomPackage.AppUpdate
{
    public class AppUpdaterInfoScriptableObject : ScriptableObject
    {
        [Header("INFO")]
        public string appleAppID;
        [Header("Can be found on https://appstoreconnect.apple.com on General -> App Information section.")]
        [Space(10)]
        [Header("TEST TOOLS - These only work on editor mode")]
        public bool setUpdateAvailable;
        public bool setMajorUpdateAvailable;
    }
}
