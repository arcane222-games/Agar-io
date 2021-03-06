using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Core.Net;
using UnityEngine;
using Utils;

[RequireComponent(typeof(NetSyncCore))]
public class NetSyncTransform : MonoBehaviour
{
    #region Struct

    [Serializable]
    private struct SyncOptions
    {
        public bool x, y, z;

        public SyncOptions(bool x, bool y, bool z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    #endregion


    #region Private variables

    [Header("Sync options")] [SerializeField]
    private SyncOptions syncPosition = new SyncOptions(true, true, true);

    [SerializeField] private SyncOptions syncRotation = new SyncOptions(true, true, true);
    [SerializeField] private SyncOptions syncScale = new SyncOptions(true, true, true);

    private NetSyncCore _netSyncCore;

    #endregion


    #region Unity event methods

    private void Start()
    {
        // _netSyncCore = GetComponent<NetSyncCore>();
        // var execTime = (int)Math.Round(1000 / _netSyncCore.TickRate);
        //
        // Task task = new Task(() =>
        // {
        //     while (true)
        //     {
        //         byte[] payload = TransformToBytes();
        //         if (NetWebSocketManager.Instance.IsConnected)
        //             NetWebSocketManager.Instance.SendAsync(payload);
        //
        //         Thread.Sleep(execTime);
        //     }
        // });
        //
        // if (_netSyncCore.IsOwner)
        //     task.Start();
        // else
        //     NetWebSocketManager.Instance.AddOpponent(gameObject);
    }

    #endregion


    #region Private methods

    private byte[] TransformToBytes()
    {
        var pos = CustomUtils.Vector3ToBytes(transform.position);
        var rot = CustomUtils.Vector3ToBytes(transform.eulerAngles);
        var scale = CustomUtils.Vector3ToBytes(transform.localScale);

        var bytes = new byte[pos.Length + rot.Length + scale.Length];
        for (int i = 0; i < pos.Length; i++)
        {
            bytes[i] = pos[i];
            bytes[12 + i] = rot[i];
            bytes[24 + i] = scale[i];
        }

        return bytes;
    }

    #endregion
}