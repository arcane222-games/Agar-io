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
        
        public void OnPlayBtnClick()
        {
            NetWebSocketManager.Instance.Init();
            NetWebSocketManager.Instance.Connect();

            StartCoroutine(WaitForConnection());
        }



        #region

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