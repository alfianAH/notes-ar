using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NotesHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private Text titleText,
        bodyText;

    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image backgroundImage;
    
    private Color backgroundColor;
    private bool isPressed;

    public Text TitleText => titleText;

    public Text BodyText => bodyText;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        backgroundImage = GetComponent<Image>();
        backgroundColor = backgroundImage.color;

        // Check canvas
        if (canvas == null)
        {
            Transform canvasTransform = transform.parent;
            while (canvasTransform != null)
            {
                // Set canvas
                canvas = canvasTransform.GetComponent<Canvas>();
                // If canvas is found, break.
                if (canvas != null)
                    break;
                 
                // Check in parent
                canvasTransform = canvasTransform.parent;
            }
        }
    }
    
    /// <summary>
    /// On Begin Drag, change alpha of background image.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        backgroundColor.a = .4f;
        backgroundImage.color = backgroundColor;
    }
    
    /// <summary>
    /// On Drag note, change note's position
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition -= eventData.delta / canvas.scaleFactor;
    }
    
    /// <summary>
    /// On End Drag note, set alpha of background image to normal
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        backgroundColor.a = 1f;
        backgroundImage.color = backgroundColor;
    }
    
    /// <summary>
    /// On Pointer Down, set note as last sibling (on top of others)
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.SetAsLastSibling();
    }
}
