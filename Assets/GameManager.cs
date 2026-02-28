using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public TMP_InputField nameInput;

    public string playerName;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void SetName()
    {
        playerName = nameInput.text;

        Debug.Log("Player name is: " + playerName);
    }
}