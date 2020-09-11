using UnityEngine;

public class NotesManager : MonoBehaviour
{
    [SerializeField] private NotesHolder notesHolder;
    [SerializeField] private Canvas canvasWorld;
    
    private void Start()
    {
        Debug.Log($"There are {GameDataController.GetNotesCount()} data(s)");
        // Make notes if available
        if(GameDataController.GetNotesCount() > 0)
        {
            for (int i = 0; i < GameDataController.GetNotesCount(); i++)
            {
                // Duplicate notes holder
                GameObject notesDuplicate = Instantiate(notesHolder.gameObject, canvasWorld.transform, true);
                // Name the object
                notesDuplicate.name = $"Note {i}";
                
                // Get NotesHolder component
                NotesHolder notesHolderDuplicate = notesDuplicate.GetComponent<NotesHolder>();
                RectTransform notesRectTransform = notesDuplicate.GetComponent<RectTransform>();
                
                notesDuplicate.SetActive(true);
                
                // Get notes
                GameDataController.GetNotes(i,
                    notesDuplicate.name,
                    notesHolderDuplicate.TitleText,
                    notesHolderDuplicate.BodyText,
                    notesRectTransform);
            }
        }
    }
}