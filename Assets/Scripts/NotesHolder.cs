using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NotesHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, 
    IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Text titleText,
        bodyText;
    [SerializeField] private Button updateButton,
        deleteButton,
        confirmDeleteButton;
    [SerializeField] private FormManager updateFormManager;
    [SerializeField] private GameObject confirmDelete;
    [SerializeField] private Canvas canvasWorld;
    public RectTransform rectTransform;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private UnityEvent updateEvent;
    
    private Color backgroundColor;
    private bool isDragging;
    
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
        isDragging = true;
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
        GameDataController.UpdateNotes(this, rectTransform.anchoredPosition);
        isDragging = false;
    }
    
    /// <summary>
    /// On Pointer Down, set note as last sibling (on top of others)
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.SetAsLastSibling();
    }
    
    /// <summary>
    /// On Pointer Up, update note when player tap the note
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        // If player tap the note, ...
        if (!isDragging)
        {
            // Activate update form manager
            updateFormManager.gameObject.SetActive(true);
            // Set input field text
            updateFormManager.inputTitle.text = titleText.text;
            updateFormManager.inputBody.text = bodyText.text;
            
            // Add listener
            // Update Button
            // Save note
            Debug.Log(this);
            updateButton.onClick.RemoveAllListeners();
            updateButton.onClick.AddListener(delegate
            {
                updateEvent?.Invoke();
            });
            
            // Delete button
            deleteButton.onClick.AddListener(() => confirmDelete.SetActive(true));
            // Confirm delete
            
            confirmDeleteButton.onClick.AddListener(() => updateFormManager.DeleteNote(this));
            confirmDeleteButton.onClick.AddListener(() => confirmDelete.SetActive(false));
            confirmDeleteButton.onClick.AddListener(() => updateFormManager.gameObject.SetActive(false));
        }
    }
}
