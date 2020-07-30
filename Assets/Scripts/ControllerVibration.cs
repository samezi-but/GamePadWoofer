using System.Collections;
using UnityEngine;
using XInputDotNetPure;

public class ControllerVibration : MonoBehaviour
{
    [SerializeField] float right_power = 1;
    [SerializeField] float left_power = 1;
    [SerializeField] float duration = 0.5f;

    public AudioSource audioSource;
    public bool isRightVibration = false;
    public bool isLeftVibration = false;
    void Update()
    {
        if (isRightVibration)
        {
            GamePad.SetVibration(0, 0, right_power);
        }
        else
        {
            GamePad.SetVibration(0, 0, 0);
        }

        if (isLeftVibration)
        {
            GamePad.SetVibration(0, left_power, 0);
        }
        else
        {
            GamePad.SetVibration(0, 0, 0);
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

    private void OnDestroy()
    {
        GamePad.SetVibration(0, 0, 0);
    }

}