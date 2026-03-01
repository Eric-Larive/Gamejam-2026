using System.Collections;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject panelRoot;   // the whole dialog UI
    [SerializeField] private TMP_Text dialogText;

    [Header("Typewriter (optional)")]
    [SerializeField] private bool useTypewriter = true;
    [SerializeField] private float charsPerSecond = 40f;

    private Coroutine typingCo;

    void Awake()
    {
        if (panelRoot == null) panelRoot = gameObject;
        Hide();
    }

    public void Show(string text)
    {
        panelRoot.SetActive(true);
        SetText(text);
    }

    public void Hide()
    {
        if (typingCo != null) StopCoroutine(typingCo);
        panelRoot.SetActive(false);
    }

    public void SetText(string text)
    {
        if (!useTypewriter)
        {
            dialogText.text = text;
            return;
        }

        if (typingCo != null) StopCoroutine(typingCo);
        typingCo = StartCoroutine(TypeText(text));
    }

    public bool IsVisible() => panelRoot.activeSelf;

    private IEnumerator TypeText(string full)
    {
        dialogText.text = "";
        float delay = 1f / Mathf.Max(1f, charsPerSecond);

        for (int i = 0; i < full.Length; i++)
        {
            dialogText.text += full[i];
            yield return new WaitForSeconds(delay);
        }
        typingCo = null;
    }

    // Optional: call this from a "Next" button to instantly finish typing
    public void SkipTyping(string fullText)
    {
        if (typingCo != null)
        {
            StopCoroutine(typingCo);
            typingCo = null;
            dialogText.text = fullText;
        }
    }
}