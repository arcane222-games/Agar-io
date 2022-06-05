using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSyncCore : MonoBehaviour
{
    #region Private variables
    
    [SerializeField] private int tickRate = 20;
    [SerializeField] private bool isOwner = true;
    
    #endregion

    #region Public variables

    public float TickRate => tickRate;
    public bool IsOwner => isOwner;

    #endregion
}
