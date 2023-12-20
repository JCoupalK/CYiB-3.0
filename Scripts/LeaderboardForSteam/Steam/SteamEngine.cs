using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.Serialization;

namespace LeastSquares
{
    /// <summary>
    /// Core class that loads Steam into the game
    /// </summary>
    public class SteamEngine : MonoBehaviour
    {
        private static bool _initialized;
        private static uint _initializedId;
        public uint appId = 480;

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException">Will throw if multiple scripts of SteamEngine exist and have different AppIds</exception>
        private void Awake()
        {
            if (_initialized && _initializedId != appId)
                throw new ArgumentException("Only 1 instance of SteamEngine can exist at the same time");
            if (_initialized) return;
            try
            {
                SteamClient.Init(appId);
                Debug.Log($"Steam started for app {appId}");
            }
            catch (Exception e)
            {
                Debug.Log(e);
                Debug.Log(
                    $"Failed to initialize steam for app {appId}. Check the troubleshooting section in the docs.");
            }

            _initialized = true;
            _initializedId = appId;
        }

        /// <summary>
        /// Run steam callbacks every frame
        /// </summary>
        void Update()
        {
            SteamClient.RunCallbacks();
        }

        /// <summary>
        /// Shutdown steam on destroy
        /// </summary>
        // private void OnDestroy()
        // {
        //     SteamClient.Shutdown();
        // }
    }
}