using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using WebSocketSharp;

namespace Core.Net
{
    public class NetWebSocketManager : MonoSingleton<NetWebSocketManager>
    {
        #region MyRegion
        
        private const string Host = "ws://127.0.0.1";
        private const ushort Port = 3003;
        
        private const float MaxConnectionTimeout = 5f;

        #endregion


        #region Private variables

        private WebSocket _webSocket;
        private bool _autoReconnection = true;

        #endregion

        #region Public variables

        public bool IsConnected => _webSocket.IsAlive;

        #endregion


        #region Callbacks

        private void WebSocketOnOpenCallback(object sender, EventArgs e)
        {
        }

        private void WebSocketOnMessageCallback(object sender, MessageEventArgs e)
        {
        }

        private void WebSocketOnErrorCallback(object sender, ErrorEventArgs e)
        {
        }

        private void WebSocketOnCloseCallback(object sender, CloseEventArgs e)
        {
        }

        #endregion


        #region Public methods

        public override void Init()
        {
            // Create websocket instance
            _webSocket = new WebSocket($"{Host}:{Port.ToString()}");

            // Register callbacks
            _webSocket.OnOpen += WebSocketOnOpenCallback;
            _webSocket.OnMessage += WebSocketOnMessageCallback;
            _webSocket.OnError += WebSocketOnErrorCallback;
            _webSocket.OnClose += WebSocketOnCloseCallback;
        }

        public void Connect()
        {
            StartCoroutine(WaitForConnectionCoroutine());
        }

        #endregion


        #region Coroutines

        IEnumerator WaitForConnectionCoroutine()
        {
            float elapsedTime = 0;
            _webSocket.ConnectAsync();

            while (!_webSocket.IsAlive)
            {
                if (elapsedTime < MaxConnectionTimeout)
                {
                    Debug.Log("Wait for connecting");
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                else
                {
                    Debug.Log("Connection timed out");
                    if (_autoReconnection)
                    {
                        Debug.Log("Automatically try to reconnect");
                        _webSocket.ConnectAsync();
                        elapsedTime = 0;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
        }

        #endregion
    }
}