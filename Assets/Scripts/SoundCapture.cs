using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCapture : MonoBehaviour
{
    public string stereoMixerString = "ステレオ ミキサー";
    [SerializeField, Range(0f, 10f)]public float m_gain = 1f; // 音量に掛ける倍率
    public float m_volumeRate; // 音量(0-1)
    public float passVolume = 0f; // 周波数の音量？
    public float vibrationVolume = 0.5f; // 振動する音量
    public int cutoffHighFreq = 0; // カットオフする高いほうの周波数
    public int cutoffLowFreq = 0; // カットオフする低い方の周波数
    private float[] spectrum = new float[256]; // スペクトラム
    private ControllerVibration controllerVibration;
    private AudioSource audioSource;
    public string[] soundDevices;
    [SerializeField]
    public DeviceUIChanger deviceUIChanger;
    // Start is called before the first frame update
    void Start()
    {
        controllerVibration = GetComponent <ControllerVibration> ();
        audioSource = GetComponent<AudioSource>();
        soundDevices = Microphone.devices;

        // ドロップダウンメニューにデバイス名一覧を設定
        deviceUIChanger.SetDropdownDevice(soundDevices);

        int count = 0;
        foreach (string str in Microphone.devices)
        {
            Debug.Log(str);
            if (str.StartsWith(stereoMixerString))
            {
                Debug.Log("ステレオミキサーを導入");
                int minFreq, maxFreq;
                Microphone.GetDeviceCaps(str, out minFreq, out maxFreq);
                audioSource.clip = Microphone.Start(str, true, 2, minFreq);
                audioSource.Play();

                // ドロップダウンメニューを線tなくする
                deviceUIChanger.SetDropdownIndex(count);
                return;
            }
            count++;

        }
    }
    private float lastLow = 0f;
    public float t = 0.5f;
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(m_volumeRate);
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        float sum = 0f;
        for(int i = cutoffLowFreq;i < spectrum.Length; i++)
        {
            float spec = spectrum[i];
            sum += spec;
            if(i >= cutoffHighFreq)
            {
                break;
            }
        }
        passVolume = Mathf.Clamp01(lastLow * (1 - t) + sum * t);
        lastLow = passVolume;
        if (vibrationVolume < passVolume)
        {
            controllerVibration.isRightVibration = true;
            controllerVibration.isLeftVibration = true;
        }
        else
        {
            controllerVibration.isRightVibration = false;
            controllerVibration.isLeftVibration = false;
        }
    }

    // オーディオが読まれるたびに実行される
    private void OnAudioFilterRead(float[] data, int channels)
    {
        float sum = 0f;
        for (int i = 0; i < data.Length; ++i)
        {
            sum += Mathf.Abs(data[i]); // データ（波形）の絶対値を足す
        }
        // データ数で割ったものに倍率をかけて音量とする
        m_volumeRate = Mathf.Clamp01(sum * m_gain / (float)data.Length);
        
        //if (vibrationVolume < m_volumeRate)
        //{
        //    controllerVibration.isRightVibration = true;
        //    controllerVibration.isLeftVibration = true;
        //}
        //else
        //{
        //    controllerVibration.isRightVibration = false;
        //    controllerVibration.isLeftVibration = false;
        //}
    }

    /// <summary>
    /// デバイスを変更する
    /// </summary>
    /// <param name="deviceNumber"></param>
    public void ChangeSoundDevice(int deviceNumber)
    {
        string str = soundDevices[deviceNumber];
        int minFreq, maxFreq;
        audioSource.Stop();
        Microphone.GetDeviceCaps(str, out minFreq, out maxFreq);
        audioSource.clip = Microphone.Start(str, true, 2, minFreq);
        audioSource.Play();
    }
}
