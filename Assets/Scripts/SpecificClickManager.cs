using System.Linq;
using UnityEngine;

public class SpecificClickManager : MonoBehaviour
{
    public ClickableObject[] objects;
    public GameObject[] toShow;

    public void ObjectClicked()
    {
        if (objects.All(clickableObject => clickableObject.wasClicked))
        {
            AllClicked();
        }
    }

    private void AllClicked()
    {
        foreach (var gameObject in toShow)
        {
            gameObject.SetActive(true);
        }
    }
}