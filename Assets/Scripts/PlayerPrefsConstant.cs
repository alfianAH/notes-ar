using UnityEngine;

public static class PlayerPrefsConstant
{
    [Tooltip("ID of each notes")] 
    public static string Id = "Id";
    
    [Tooltip("Title of a note. \nHow to access: NoteTitle + ID")]
    public static string NoteTitle = "NoteTitle";
    
    [Tooltip("Body of a note. \nHow to access: NoteBody + ID")]
    public static string NoteBody = "NoteBody";
    
    [Tooltip("Note's number for naming.")]
    public static string NoteNumber = "NoteNumber";

    [Tooltip("Game data in JSON")] 
    public static string GameData = "GameData";
}
