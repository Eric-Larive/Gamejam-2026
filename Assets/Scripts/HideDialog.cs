using UnityEngine;

public class HideDialog : MonoBehaviour
{
    public float delay = 10f; // seconds before disappearing

    private void Start()
    {
        Invoke(nameof(Hide), delay);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
