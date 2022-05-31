using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Objs
{
    public class MainCameraController : MonoBehaviour
    {
        #region Private variables

        [SerializeField] private float movementSpeed;

        private float _size;

        private Transform _playerTransform;
    
        #endregion


        #region Unity Event Methods

    
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 lerp = Vector3.Lerp(transform.position, _playerTransform.position, Time.deltaTime * movementSpeed);
            lerp.z = -10;
            transform.position = lerp;
        }

        #endregion


        #region Private Methods

        private void Init()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        #endregion
    }
}