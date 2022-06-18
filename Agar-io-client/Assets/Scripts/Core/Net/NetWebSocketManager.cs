using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private const string Host = "ws://14.33.110.230";
        private const ushort Port = 8080;

        private const float MaxConnectionTimeout = 5f;

        #endregion


        #region Private variables

        private WebSocket _webSocket;
        private GameObject _opponent;
        private Stopwatch _stopWatch;
        private bool _autoReconnection = true;
        private bool _onPongReceived = true;

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
            Debug.Log(e);
        }

        private void WebSocketOnMessageCallback(object sender, MessageEventArgs e)
        {
            if (e.RawData.Length == 0)
            {
                Latency = _stopWatch.ElapsedMilliseconds;
                _stopWatch.Stop();
                _onPongReceived = true;
                Debug.Log("pong");
            }
            
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
            Debug.Log($"{e.Exception} / {e.Message}");
        }

        private void WebSocketOnCloseCallback(object sender, CloseEventArgs e)
        {
            Debug.Log($"{e.Code.ToString()} / {e.Reason}");
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
            
            var task = new Task(() =>
            {
                while (true)
                {
                    if (IsConnected)
                    {
                        if (_onPongReceived)
                        {
                            _stopWatch.Reset();
                            _stopWatch.Start();
                            
                            _webSocket.Ping();
                            _onPongReceived = false;
                        }
                    }

                    Thread.Sleep(100);
                }
            });

            task.Start();
        }

        public void Send(byte[] rawData)
        {
            if(IsConnected)
                _webSocket.Send(rawData);
        }

        public void Send(string json)
        {
            if(IsConnected)
                _webSocket.Send(json);
        }

        public void Send<T>(T obj)
        {
            if(IsConnected)
                _webSocket.Send(JsonUtility.ToJson(obj));
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

        #endregion
    }
}