using System.Collections;
using System.Collections.Generic;
using Core.Net;
using TMPro;
using UnityEngine;
using Utils;

namespace Core.Objs
{
    public class PlayerController : MonoBehaviour
    {
        #region Private variables
        
        [SerializeField] private float movementSpd = 5f;

        private TextMeshPro _tmProTransform;
        private NetSyncCore _netSyncCore;

        #endregion


        #region Unity Event Methods

        private void Start()
        {
            _tmProTransform = GetComponentInChildren<TextMeshPro>();
            _netSyncCore = GetComponent<NetSyncCore>();
        }

        private void Update()
        {
            ProcessInput();
        }

        #endregion


        #region Private methods

        private void ProcessInput()
        {
            if (_netSyncCore.IsOwner)
            {
                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");
                float gap = Time.deltaTime * movementSpd;
                transform.position += new Vector3(x * gap, y * gap, 0);
            }
            
            _tmProTransform.text =
                $"P: {transform.position.ToString()}\nR: {transform.eulerAngles.ToString()}\nS: {transform.localScale.ToString()}\nID: {0.ToString()}";
        }

        #endregion
    }
}