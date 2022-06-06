using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Utils;
using WebSocketSharp;
using Debug = UnityEngine.Debug;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace Core.Net
{
    public class NetWebSocketManager : MonoSingleton<NetWebSocketManager>
    {
        #region Constants

        private const string Host = "ws://127.0.0.1";
        private const ushort Port = 3003;

        private const float MaxConnectionTimeout = 5f;

        #endregion


        #region Private variables

        private WebSocket _webSocket;
        private GameObject _opponent;
        private Stopwatch _stopWatch;
        private bool _autoReconnection = true;

        #endregion

        #region Public variables

        public bool IsConnected => _webSocket.IsAlive;
        public long Latency { get; private set; }

        #endregion

        #region Unity event methods

        private void OnApplicationQuit()
        {
            _webSocket.CloseAsync();
        }

        #endregion


        #region Callbacks

        private void WebSocketOnOpenCallback(object sender, EventArgs e)
        {
        }

        private void WebSocketOnMessageCallback(object sender, MessageEventArgs e)
        {
            if (e.Data == null)
            {
                Vector3 pos = CustomUtils.BytesToVector3(e.RawData);
                Vector3 rot = CustomUtils.BytesToVector3(e.RawData, 12);
                Vector3 scale = CustomUtils.BytesToVector3(e.RawData, 24);

                _opponent.transform.position = Vector3.Lerp(_opponent.transform.position, pos, 1f);
                _opponent.transform.eulerAngles = rot;
                _opponent.transform.localScale = scale;

                Debug.Log(_opponent.name);
            }
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
            _stopWatch = new Stopwatch();

            // Register callbacks
            _webSocket.OnOpen += WebSocketOnOpenCallback;
            _webSocket.OnMessage += WebSocketOnMessageCallback;
            _webSocket.OnError += WebSocketOnErrorCallback;
            _webSocket.OnClose += WebSocketOnCloseCallback;
        }

        public void Connect()
        {
            StartCoroutine(WaitForConnectionCoroutine());
            StartCoroutine(PingInterval());
        }

        public void SendAsync(byte[] rawData)
        {
            if (IsConnected)
                _webSocket.SendAsync(rawData, null);
        }

        public void SendAsync(string json)
        {
            if (IsConnected)
                _webSocket.SendAsync(json, null);
        }

        public void SendAsync<T>(T obj)
        {
            if (IsConnected)
                _webSocket.SendAsync(JsonUtility.ToJson(obj), null);
        }

        public void AddOpponent(GameObject obj)
        {
            _opponent = obj;
        }

        #endregion


        #region Coroutines

        private IEnumerator WaitForConnectionCoroutine()
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

        private IEnumerator PingInterval()
        {
            while (true)
            {
                if (IsConnected)
                {
                    _stopWatch.Reset();
                    _stopWatch.Start();
                    if (_webSocket.Ping())
                    {
                        _stopWatch.Stop();
                        Latency = _stopWatch.ElapsedMilliseconds;
                    }
                }

                yield return null;
            }
        }

        #endregion
    }
}