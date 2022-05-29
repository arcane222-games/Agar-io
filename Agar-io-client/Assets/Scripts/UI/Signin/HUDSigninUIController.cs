using System.Collections;
using System.Collections.Generic;
using Core.Net;
using UnityEngine;

namespace UI.Signin
{
    public class HUDSigninUIController : MonoBehaviour
    {
        public void OnPlayBtnClick()
        {
            NetWebSocketManager.Instance.Init();
            NetWebSocketManager.Instance.Connect();
        }
    }
}