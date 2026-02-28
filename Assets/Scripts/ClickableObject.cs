using UnityEngine;
using UnityEngine.InputSystem;

public class ClickableObject : MonoBehaviour
{
    public GameObject spriteOn;
    public GameObject spriteOff;

    private bool _isOn = true;

    public void Toggle()
    {
        _isOn = !_isOn;

        spriteOn.SetActive(_isOn);
        spriteOff.SetActive(!_isOn);
    }
}