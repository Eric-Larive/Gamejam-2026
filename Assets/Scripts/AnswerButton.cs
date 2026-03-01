using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [Header("References")]
    public Transform sprite;
    public Transform waypoint;

    [Header("Movement")]
    public float speed = 3f;

    [Header("Answer")]
    public bool isCorrect;
    
    private bool moveSprite;

    private void Update()
    {
        if (!moveSprite) return;

        sprite.position = Vector3.MoveTowards(
            sprite.position,
            waypoint.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(sprite.position, waypoint.position) < 0.05f)
        {
            moveSprite = false;

            if (isCorrect)
                Debug.Log("Correct answer!");
            else
                Debug.Log("Wrong answer!");
        }
    }

    public void OnButtonClicked()
    {
        moveSprite = true;
    }
}