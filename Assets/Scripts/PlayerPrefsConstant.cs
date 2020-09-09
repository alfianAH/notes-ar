using UnityEngine;

public class PlayerPrefsConstant
{
    [Tooltip("ID of each notes")] 
    public static string Id = "Id";
    
    [Tooltip("Title of a note. \nHow to access: NoteTitle + ID")]
    public static string NoteTitle = "NoteTitle";
    
    [Tooltip("Body of a note. \nHow to access: NoteBody + ID")]
    public static string NoteBody = "NoteBody";
}
