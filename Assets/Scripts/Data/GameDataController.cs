using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameDataController : MonoBehaviour
{
    public static SaveData SaveData;
    
    private void Awake()
    {
        // PlayerPrefs.DeleteKey(PlayerPrefsConstant.NoteNumber);
        // PlayerPrefs.DeleteKey(PlayerPrefsConstant.GameData);
        LoadData();
    }

    /// <summary>
    /// Save data to JSON
    /// </summary>
    [ContextMenu("Save Data")]
    public void SaveGame()
    {
        string data = JsonUtility.ToJson(SaveData);
        PlayerPrefs.SetString(PlayerPrefsConstant.GameData, data);
        Debug.Log("Save: " + SaveData);
    }
    
    /// <summary>
    /// Load data from JSON
    /// </summary>
    [ContextMenu("Load Data")]
    public void LoadData()
    {
        string data;
        if(PlayerPrefs.HasKey(PlayerPrefsConstant.GameData))
        {
            data = PlayerPrefs.GetString(PlayerPrefsConstant.GameData);
            SaveData = JsonUtility.FromJson<SaveData>(data);
        }
        else
        {
            data = "{}";
            PlayerPrefs.SetString(PlayerPrefsConstant.GameData, data);
            SaveData = JsonUtility.FromJson<SaveData>(data);
        }
        Debug.Log("Load: " + SaveData);
    }

    private void OnDisable()
    {
        SaveGame();
    }

    /// <summary>
    /// Get notes length
    /// </summary>
    /// <returns></returns>
    public static int GetNotesCount()
    {
        // Return 0 if there are no note datas
        // Else return length of note datas
        Debug.Log(SaveData.noteDatas);
        return SaveData.noteDatas?.Count ?? 0;
    }

    /// <summary>
    /// Get Notes' Data
    /// </summary>
    /// <param name="noteData"></param>
    /// <param name="noteName"></param>
    /// <param name="titleText"></param>
    /// <param name="bodyText"></param>
    /// <param name="notePosition"></param>
    public static void GetNotes(NoteData noteData, string noteName,
        Text titleText, Text bodyText, RectTransform notePosition)
    {
        if (SaveData.noteDatas == null) return;
        
        // Set note's data
        if (noteData.id == noteName)
        {
            Debug.Log("Set index " + noteData.id);

            // Set titleText
            titleText.text = noteData.titleText;
            // Set bodyText
            bodyText.text = noteData.bodyText;
            // Set position
            notePosition.anchoredPosition = new Vector2(noteData.xAxis, noteData.yAxis);
        }
    }
    
    /// <summary>
    /// Set notes' data
    /// </summary>
    /// <param name="noteName"></param>
    /// <param name="titleText"></param>
    /// <param name="bodyText"></param>
    /// <param name="notePosition"></param>
    public static void SetNotes(string noteName,
        Text titleText, Text bodyText, RectTransform notePosition)
    {
        if (SaveData.noteDatas == null)
        {
            SaveData.noteDatas = new List<NoteData>();
        }
        
        NoteData notesData = new NoteData
        {
            xAxis = notePosition.position.x,
            yAxis = notePosition.position.y,
            zAxis = notePosition.position.z,
            id = noteName,
            titleText = titleText.text,
            bodyText = bodyText.text
        };
        
        SaveData.noteDatas.Add(notesData);
        Debug.Log($"There are {SaveData.noteDatas.Count} data(s)");
    }

    /// <summary>
    /// Overloading
    /// Set notes' updated position
    /// </summary>
    /// <param name="notesHolder"></param>
    /// <param name="notesPosition"></param>
    public static void UpdateNotes(NotesHolder notesHolder, Vector2 notesPosition)
    {
        if (SaveData.noteDatas == null)
        {
            SaveData.noteDatas = new List<NoteData>();
        }
        
        NoteData notesData = new NoteData
        {
            xAxis = notesPosition.x,
            yAxis = notesPosition.y,
            id = notesHolder.name,
            titleText = notesHolder.TitleText.text,
            bodyText = notesHolder.BodyText.text
        };
        
        // Search notes
        for (int i = 0; i <= SaveData.noteDatas.Count; i++)
        {
            if(SaveData.noteDatas[i].id != notesHolder.name) continue;
            
            SaveData.noteDatas.RemoveAt(i);
            SaveData.noteDatas.Add(notesData);
            break;
        }
    }
}
