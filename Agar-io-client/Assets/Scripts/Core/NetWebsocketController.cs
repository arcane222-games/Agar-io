using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace Core
{
    public class NetWebsocketController : MonoBehaviour
    {
        private WebSocket _webSocket;
        public void OnPlayBtnClick()
        {
            if (_webSocket == null)
            {
                _webSocket = new WebSocket("ws://127.0.0.1:3003");
                _webSocket.Connect();
                
                _webSocket.OnOpen += (sender, args) =>
                {
                    Debug.Log("Connect!");
                };

                _webSocket.OnMessage += (sender, args) =>
                {
                    if(!string.IsNullOrEmpty(args.Data))
                        Debug.Log(args.Data);
                };
                
                _webSocket.OnError += (sender, args) =>
                {

                };

                _webSocket.OnClose += (sender, args) =>
                {
                    
                };
            }
        }
    }
}