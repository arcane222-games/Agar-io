using System;
using System.Collections;
using System.Collections.Generic;
using Core.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Signin
{
    public class HUDSigninUIController : MonoBehaviour
    {
        private const string DestSceneName = "GameScene";

        #region Unity event methods

        private void Awake()
        {
            Application.runInBackground = true;
            Application.targetFrameRate = 60;
        }

        #endregion


        #region Callbacks

        public void OnPlayBtnClick()
        {
            NetWebSocketManager.Instance.Init();
            NetWebSocketManager.Instance.Connect();

            StartCoroutine(WaitForConnection());
        }

        #endregion


        #region Coroutines

        private IEnumerator WaitForConnection()
        {
            while (!NetWebSocketManager.Instance.IsConnected)
            {
                yield return null;
            }

            SceneManager.LoadSceneAsync(DestSceneName);
        }

        #endregion
    }
}