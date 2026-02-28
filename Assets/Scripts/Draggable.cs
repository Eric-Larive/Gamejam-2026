using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ClothDragToCastor : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag")]
    [SerializeField] private Canvas canvas;

    [Header("Drop Target")]
    [SerializeField] private RectTransform castorDropZone;  // usually the Castor rect
    [SerializeField] private CastorDressController castorState;  // reference to controller

    [Header("What this cloth is")]
    [SerializeField] private Outfit piece;                  // e.g. Outfit.Shirt

    private RectTransform rect;
    private CanvasGroup cg;
    private Vector2 startAnchoredPos;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();

        if (canvas == null) canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startAnchoredPos = rect.anchoredPosition;
        rect.SetAsLastSibling();
        cg.blocksRaycasts = false; // allow target under pointer to be detected
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = true;

        bool droppedOnCastor = RectTransformUtility.RectangleContainsScreenPoint(
            castorDropZone,
            eventData.position,
            eventData.pressEventCamera
        );

        if (piece != Outfit.None && droppedOnCastor)
        {
            castorState.EquipAndReact(piece);
            gameObject.SetActive(false);
        }
        else
        {
            castorState.FailReact();
            rect.anchoredPosition = startAnchoredPos;
        }
    }
}