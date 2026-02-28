using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Draggable2D : MonoBehaviour
{
    private Camera cam;
    private Vector3 offset;

    void Awake()
    {
        cam = Camera.main;
        if (cam == null)
            Debug.LogError("No MainCamera found. Tag your camera as MainCamera.");
    }

    private Vector3 MouseWorld()
    {
        Vector3 m = Input.mousePosition;
        m.z = -cam.transform.position.z;      // distance to z=0 plane (typical 2D)
        Vector3 w = cam.ScreenToWorldPoint(m);
        w.z = transform.position.z;           // keep sprite z
        return w;
    }

    private void OnMouseDown()
    {
        offset = transform.position - MouseWorld();
    }

    private void OnMouseDrag()
    {
        transform.position = MouseWorld() + offset;
    }
}