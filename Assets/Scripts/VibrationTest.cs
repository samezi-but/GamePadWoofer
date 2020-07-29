using System.Collections;
using UnityEngine;
using XInputDotNetPure;

public class VibrationTest : MonoBehaviour
{
    [SerializeField] float right_power = 1;
    [SerializeField] float left_power = 1;
    [SerializeField] float duration = 0.5f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine("RightVibration");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine("LeftVibration");
        }
    }

    IEnumerator RightVibration()
    {
        GamePad.SetVibration(0, 0, right_power);
        yield return new WaitForSecondsRealtime(duration);
        GamePad.SetVibration(0, 0, 0);
    }

    IEnumerator LeftVibration()
    {
        GamePad.SetVibration(0, left_power, 0);
        yield return new WaitForSecondsRealtime(duration);
        GamePad.SetVibration(0, 0, 0);
    }

}