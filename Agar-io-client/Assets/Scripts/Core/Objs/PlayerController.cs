using System.Collections;
using System.Collections.Generic;
using Core.Net;
using UnityEngine;
using Utils;

namespace Core.Objs
{
    public class PlayerController : MonoBehaviour
    {
        #region Private variables

        [SerializeField] private float movementSpd = 5f;

        #endregion


        #region Unity Event Methods

        void Update()
        {
            ProcessInput();
        }

        #endregion


        #region Private methods

        private void ProcessInput()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            float gap = Time.deltaTime * movementSpd;
            transform.position += new Vector3(x * gap, y * gap, 0);
        }

        #endregion
    }   
}