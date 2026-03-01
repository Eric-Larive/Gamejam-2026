using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject[] spriteOn;
    public GameObject[] spriteOff;
    public bool wasClicked = false;
    public string id; // "thermometer", "window", "boots", etc.
    public GameFlow flow; // drag the manager here

    private bool _isOn = true;

    [TextArea(2, 5)]
    public string message;

    public DialogController dialog; // drag your DialogUI here

    public void Trigger()
    {
        if (dialog == null)
        {
            Debug.LogError("DialogTrigger: dialog reference missing.");
            return;
        }
        flow.NotifyClicked(id);
        dialog.Show(message);
    }

    public void Toggle()
    {
        wasClicked = true;
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