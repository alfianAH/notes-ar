using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NotesHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private Text titleText,
        bodyText;

    [SerializeField] private Canvas canvasWorld;
    public RectTransform rectTransform;
    [SerializeField] private Image backgroundImage;
    
    private Color backgroundColor;
    
    public Text TitleText => titleText;

    public Text BodyText => bodyText;

    private void Awake()
    {
        GetNoteComponents();
    }
    
    /// <summary>
    /// Get Notes' Components
    /// </summary>
    private void GetNoteComponents()
    {
        rectTransform = GetComponent<RectTransform>();
        backgroundImage = GetComponent<Image>();
        backgroundColor = backgroundImage.color;

        // Check canvas
        if (canvasWorld == null)
        {
            Transform canvasTransform = transform.parent;
            while (canvasTransform != null)
            {
                // Set canvas
                canvasWorld = canvasTransform.GetComponent<Canvas>();
                // If canvas is found, break.
                if (canvasWorld != null)
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
        rectTransform.anchoredPosition -= eventData.delta / canvasWorld.scaleFactor;
    }
    
    /// <summary>
    /// On End Drag note, set alpha of background image to normal
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        backgroundColor.a = 1f;
        backgroundImage.color = backgroundColor;
        GameDataController.SetNotes(this, rectTransform.anchoredPosition);
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
