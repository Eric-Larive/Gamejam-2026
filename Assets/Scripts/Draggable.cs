using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class DragClothToCastor : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("References")]
    [SerializeField] private Canvas canvas;              // assign your Canvas
    [SerializeField] private RectTransform castorZone;   // drag the Castor RectTransform here
    [SerializeField] private Image castorImage;          // drag the Castor Image here
    [SerializeField] private Sprite castorWithCloth;     // sprite of castor wearing cloth

    [Header("Behavior")]
    [SerializeField] private bool hideClothOnSuccess = true;
    [SerializeField] private float snapToZone = 0f; // 0 = no snap, otherwise snaps cloth to zone center

    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private Vector3 startLocalPos;
    private Transform startParent;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();

        if (canvas == null) canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startLocalPos = rect.localPosition;
        startParent = rect.parent;

        // Put on top while dragging
        rect.SetAsLastSibling();

        // Let raycasts pass through cloth while dragging so we can "see" the castor zone
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move by delta (best for UI)
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        //bool success = IsOverlapping(rect, castorZone);
        bool success = RectTransformUtility.RectangleContainsScreenPoint(
            castorZone,
            eventData.position,
            eventData.pressEventCamera
        );

        if (success)
        {
            // Swap castor sprite
            castorImage.sprite = castorWithCloth;

            // Optionally snap cloth to castor center
            if (snapToZone > 0f)
                rect.position = castorZone.position;

            // Hide or reset cloth
            if (hideClothOnSuccess)
                gameObject.SetActive(false);
        }
        else
        {
            // Return cloth to start
            rect.SetParent(startParent, worldPositionStays: false);
            rect.localPosition = startLocalPos;
        }
    }

    private static bool IsOverlapping(RectTransform a, RectTransform b)
    {
        // Works great for Screen Space Overlay canvases
        return RectTransformUtility.RectangleContainsScreenPoint(
            b,
            RectTransformUtility.WorldToScreenPoint(null, a.position),
            null
        ) && RectTransformUtility.RectangleContainsScreenPoint(
            b,
            RectTransformUtility.WorldToScreenPoint(null, a.TransformPoint(a.rect.min)),
            null
        );
    }
}