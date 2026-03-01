using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private DialogController dialog;

    private int stepIndex = 0;
    private string waitingForClickId = null;

    [Serializable]
    public class Step
    {
        public StepType type;
        [TextArea(2, 5)] public string text;

        public string targetId;              // for WaitClick
        public float delaySeconds = 0.8f;    // for Delay
    }

    public enum StepType { Say, Delay, WaitClick }

    public List<Step> steps = new();

    void Start() => RunNext();

    public void NotifyClicked(string id)
    {
        if (waitingForClickId == null) return;
        if (id != waitingForClickId) return;

        waitingForClickId = null;
        RunNext();
    }

    private void RunNext()
    {
        if (stepIndex >= steps.Count) return;

        Step s = steps[stepIndex++];
        switch (s.type)
        {
            case StepType.Say:
                dialog.Show(s.text);
                // auto-continue or wait? choose one:
                StartCoroutine(ContinueAfter(1.2f));
                break;

            case StepType.Delay:
                StartCoroutine(ContinueAfter(s.delaySeconds));
                break;

            case StepType.WaitClick:
                dialog.Show(s.text); // prompt like "Clique sur la fenêtre"
                waitingForClickId = s.targetId;
                break;
        }
    }

    private IEnumerator ContinueAfter(float t)
    {
        yield return new WaitForSeconds(t);
        RunNext();
    }
}