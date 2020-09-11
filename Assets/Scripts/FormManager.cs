using UnityEngine;
using UnityEngine.UI;

public class FormManager : MonoBehaviour
{
    [SerializeField] private InputField inputTitle,
        inputBody;
    [SerializeField] private GameObject canvasWorld;
    [SerializeField] private NotesHolder notesHolder;
    
    [Tooltip("To make note's name unique")]
    private int noteNumber;

    private void Start()
    {
        // Check key on player prefs
        if (PlayerPrefs.HasKey(PlayerPrefsConstant.NoteNumber))
        {
            noteNumber = PlayerPrefs.GetInt(PlayerPrefsConstant.NoteNumber);
        }
        else
        {
            noteNumber = 0;
            PlayerPrefs.SetInt(PlayerPrefsConstant.NoteNumber, noteNumber);
        }
    }

    public void SaveNote()
    {
        if(!string.IsNullOrWhiteSpace(inputTitle.text) && 
           !string.IsNullOrWhiteSpace(inputBody.text))
        {
            Debug.Log("Title: " + inputTitle.text);
            Debug.Log("Body: " + inputBody.text);
            
            // Duplicate notes holder
            GameObject notesDuplicate = Instantiate(notesHolder.gameObject, canvasWorld.transform, true);
            // Name the object
            notesDuplicate.name = $"Note {noteNumber}";
            AddNoteNumber(); // Add number to make name unique
            
            // Get NotesHolder component
            NotesHolder notesHolderDuplicate = notesDuplicate.GetComponent<NotesHolder>();
            RectTransform notesRectTransform = notesDuplicate.GetComponent<RectTransform>();
            
            notesDuplicate.SetActive(true);
            
            // Set note's text
            notesHolderDuplicate.TitleText.text = inputTitle.text;
            notesHolderDuplicate.BodyText.text = inputBody.text;
            
            // Save to JSON
            GameDataController.SetNotes(notesDuplicate.name,
                notesHolderDuplicate.TitleText,
                notesHolderDuplicate.BodyText,
                notesRectTransform);
        }
    }
    
    /// <summary>
    /// Add Note Number to prefs to make note's name unique
    /// </summary>
    private void AddNoteNumber()
    {
        noteNumber++;
        PlayerPrefs.SetInt(PlayerPrefsConstant.NoteNumber, noteNumber);
    }
}
