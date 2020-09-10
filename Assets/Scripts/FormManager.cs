using UnityEngine;
using UnityEngine.UI;

public class FormManager : MonoBehaviour
{
    [SerializeField] private InputField inputTitle,
        inputBody;
    [SerializeField] private GameObject canvasWorld;
    [SerializeField] private NotesHolder notesHolder;
    
    public void SaveNote()
    {
        if(!string.IsNullOrWhiteSpace(inputTitle.text) && 
           !string.IsNullOrWhiteSpace(inputBody.text))
        {
            Debug.Log("Title: " + inputTitle.text);
            Debug.Log("Body: " + inputBody.text);
            
            // Duplicate notes holder
            GameObject notesDuplicate = Instantiate(notesHolder.gameObject, canvasWorld.transform, true);
            NotesHolder notesHolderDuplicate = notesDuplicate.GetComponent<NotesHolder>();
            
            notesDuplicate.SetActive(true);
            
            // Set note's text
            notesHolderDuplicate.TitleText.text = inputTitle.text;
            notesHolderDuplicate.BodyText.text = inputBody.text;
        }
    }
}
