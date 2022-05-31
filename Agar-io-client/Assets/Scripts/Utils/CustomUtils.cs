using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Utils
{
    public class CustomUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static Vector3 BytesToVector3(byte[] bytes, int offset = 0)
        {
            var x = BitConverter.ToSingle(bytes, offset);
            var y = BitConverter.ToSingle(bytes, offset + sizeof(float));
            var z = BitConverter.ToSingle(bytes, offset + sizeof(float) * 2);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static byte[] Vector3ToBytes(Vector3 vector3)
        {
            byte[] bytes = new byte[12];
            byte[] xToBytes = BitConverter.GetBytes(vector3.x);
            byte[] yToBytes = BitConverter.GetBytes(vector3.y);
            byte[] zToBytes = BitConverter.GetBytes(vector3.z);
            
            for (int i = 0; i < sizeof(float); i++)
            {
                bytes[i] = xToBytes[i];
                bytes[4 + i] = yToBytes[i];
                bytes[8 + i] = zToBytes[i];
            }

            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static Quaternion BytesToQuaternion(byte[] bytes, int offset = 0)
        {
            var x = BitConverter.ToSingle(bytes, offset);
            var y = BitConverter.ToSingle(bytes, offset + sizeof(float));
            var z = BitConverter.ToSingle(bytes, offset + sizeof(float) * 2);
            var w = BitConverter.ToSingle(bytes, offset + sizeof(float) * 3);

            return new Quaternion(x, y, z, w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        public static byte[] QuaternionToBytes(Quaternion quaternion)
        {
            byte[] bytes = new byte[16];
            byte[] xToBytes = BitConverter.GetBytes(quaternion.x);
            byte[] yToBytes = BitConverter.GetBytes(quaternion.y);
            byte[] zToBytes = BitConverter.GetBytes(quaternion.z);
            byte[] wToBytes = BitConverter.GetBytes(quaternion.w);
            
            for (int i = 0; i < sizeof(float); i++)
            {
                bytes[i] = xToBytes[i];
                bytes[4 + i] = yToBytes[i];
                bytes[8 + i] = zToBytes[i];
                bytes[12 + i] = wToBytes[i];
            }

            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        public static void PrintRawData(byte[] rawData)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rawData.Length; i++)
            {
                sb.Append(Convert.ToString(rawData[i], 16));
            }
            
            Debug.Log(sb.ToString());
        }
    }
}