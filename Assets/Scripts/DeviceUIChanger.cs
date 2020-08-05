using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceUIChanger : MonoBehaviour
{
    [SerializeField]
    public SoundCapture soundCapture;
    public Dropdown deviceDropDown;
    public bool isCanChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// デバイスリストを再設定する
    /// </summary>
    /// <param name="deviceNames"></param>
    public void SetDropdownDevice(string[] deviceNames)
    {
        deviceDropDown.ClearOptions();
        List<string> deviceList = new List<string>(deviceNames);
        deviceDropDown.AddOptions(deviceList);
        isCanChanged = true;
    }

    /// <summary>
    /// ドロップダウンのインデックスを設定する
    /// </summary>
    /// <param name="index"></param>
    public void SetDropdownIndex(int index)
    {
        deviceDropDown.value = index;
    }

    /// <summary>
    /// サウンドデバイスを変更する
    /// </summary>
    /// <param name="number"></param>
    public void ChangeSoundDevice(int number)
    {
        if (!isCanChanged)
        {
            return;
        }
        print("ドロップダウン選択：" + number);
        soundCapture.ChangeSoundDevice(number);
    }
}
