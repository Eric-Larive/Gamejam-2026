using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public static DialogController Instance { get; private set; }

    private void Awake()
    {
        Instance = GetComponent<DialogController>();
    }
}