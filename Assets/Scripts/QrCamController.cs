﻿using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QrCamController : MonoBehaviour
{

    private WebCamTexture _camTexture;
    private Thread _qrThread;
    private Color32[] _c;
    private int _w, _h;

    public RawImage RawImage;
    public UnityEngine.UI.Text StatusText;

    private bool _isQuit;
    private bool _qrFound;

    void Start()
    {
        _camTexture = new WebCamTexture();

        OnEnable();
        RawImage.texture = _camTexture;
        RawImage.material.mainTexture = _camTexture;
        RawImage.SetNativeSize();

        StatusText.text = "Searching For QR";

        _qrThread = new Thread(DecodeQR);
        _qrThread.Start();
    }

    void Update()
    {
        if (_c == null)
        {
            _c = _camTexture.GetPixels32();
        }
        if (_qrFound)
        {
            StatusText.text = "QR Video Found";
        }

        AdjustCamera();
    }

    void AdjustCamera()
    {
        if (_camTexture.width < 100)
        {
            Debug.Log("Still waiting another frame for correct info...");
            return;
        }

        var cwNeeded = _camTexture.videoRotationAngle;
        var ccwNeeded = -cwNeeded;

        if (_camTexture.videoVerticallyMirrored) ccwNeeded += 180;

        RawImage.rectTransform.localEulerAngles = new Vector3(0f, 0f, ccwNeeded);

        var videoRatio = _camTexture.width / _camTexture.height;

        RawImage.GetComponent<AspectRatioFitter>().aspectRatio = videoRatio;

        if (_camTexture.videoVerticallyMirrored)
        {
            RawImage.uvRect = new Rect(1, 0, -1, 1); // means flip on vertical axis
        }
        else
        {
            RawImage.uvRect = new Rect(0, 0, 1, 1);  // means no flip
        }
    }

    void OnEnable()
    {
        if (_camTexture != null)
        {
            _camTexture.Play();
            _w = _camTexture.width;
            _h = _camTexture.height;
        }
    }

    void OnDisable()
    {
        if (_camTexture != null)
        {
            _camTexture.Pause();
        }
    }

    void OnDestroy()
    {
        _qrThread.Abort();
        _camTexture.Stop();
    }

    void OnApplicationQuit()
    {
        _isQuit = true;
    }

    void DecodeQR()
    {
        var barcodeReader = new BarcodeReader { AutoRotate = false, TryHarder = false };

        while (!_qrFound)
        {
            if (_isQuit)
                break;

            try
            {
                var result = barcodeReader.Decode(_c, _w, _h);
                if (result != null)
                {
                    _qrFound = true;
                    Debug.Log(result);
                    //LoadVideo();

                }
                Thread.Sleep(200);
                _c = null;
            }
            catch
            {
            }
        }
    }

    void LoadVideo()
    {
    }
}