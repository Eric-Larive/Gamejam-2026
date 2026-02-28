using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject[] spriteOn;
    public GameObject[] spriteOff;

    private bool _isOn = true;

    public void Toggle()
    {
        _isOn = !_isOn;

        foreach (var sprite in spriteOn)
        {
            sprite.SetActive(_isOn);
        }
        
        foreach (var sprite in spriteOff)
        {
            sprite.SetActive(!_isOn); 
        }
    }
}