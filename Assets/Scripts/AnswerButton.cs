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
    public GameObject[] toShow;
    public GameObject[] toHide;
    
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
            {
                sprite.position = new Vector3(0, 0, 0);
                foreach (var gameObjects in toShow)
                {
                    gameObjects.SetActive(true);
                }

                foreach (var gameObjects in toHide)
                {
                    gameObjects.SetActive(false);
                }
            }
        }
    }

    public void OnButtonClicked()
    {
        moveSprite = true;
    }
}