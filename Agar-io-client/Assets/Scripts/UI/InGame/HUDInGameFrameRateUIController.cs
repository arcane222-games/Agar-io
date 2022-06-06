using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Core.Net;
using TMPro;
using UnityEngine;

public class HUDInGameFrameRateUIController : MonoBehaviour
{
    #region Private variables

    private TextMeshProUGUI _tmProFrameRate;

    #endregion


    #region Unity event methods

    private void Start()
    {
        _tmProFrameRate = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(SetFrameRateTextCoroutine());
    }

    void Update()
    {
    }

    #endregion


    #region Coroutines

    private IEnumerator SetFrameRateTextCoroutine()
    {
        var wfs = new WaitForSeconds(0.1f);

        while (true)
        {
            var fps = Mathf.Round(1 / Time.deltaTime);
            var ping = NetWebSocketManager.Instance.Latency;
            _tmProFrameRate.text =
                $"FPS: [{fps.ToString(CultureInfo.CurrentCulture)}], Ping: [{ping.ToString(CultureInfo.CurrentCulture)} ms]";
            yield return wfs;
        }
    }

    #endregion
}