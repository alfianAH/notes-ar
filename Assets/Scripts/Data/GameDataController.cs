using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameDataController : MonoBehaviour
{
    public static SaveData SaveData;
    
    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
        LoadData();
    }

    /// <summary>
    /// Save data to JSON
    /// </summary>
    [ContextMenu("Save Data")]
    public void SaveGame()
    {
        Debug.Log("Save: " + SaveData);
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
    /// <param name="index"></param>
    /// <param name="noteName"></param>
    /// <param name="titleText"></param>
    /// <param name="bodyText"></param>
    /// <param name="notePosition"></param>
    public static void GetNotes(int index, string noteName,
        Text titleText, Text bodyText, RectTransform notePosition)
    {
        if (SaveData.noteDatas == null) return;
        
        // Set note's data
        if (SaveData.noteDatas[index].Id == noteName)
        {
            Debug.Log("Set index " + index);

            // Set titleText
            titleText.text = SaveData.noteDatas[index].TitleText;
            // Set bodyText
            bodyText.text = SaveData.noteDatas[index].BodyText;
            // Set position
            notePosition.position = new Vector3(SaveData.noteDatas[index].XAxis,
                                                SaveData.noteDatas[index].YAxis,
                                                SaveData.noteDatas[index].ZAxis); 
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
            XAxis = notePosition.position.x,
            YAxis = notePosition.position.y,
            ZAxis = notePosition.position.z,
            Id = noteName,
            TitleText = titleText.text,
            BodyText = bodyText.text
        };
        
        SaveData.noteDatas.Add(notesData);
        // SaveData.noteDatas.RemoveAll(t => t.Id == notesData.Id);
        Debug.Log("Add: " + SaveData.noteDatas);
        Debug.Log($"There are {SaveData.noteDatas.Count} data(s)");
    }

    /// <summary>
    /// Overloading
    /// Set notes' updated position
    /// </summary>
    /// <param name="notesHolder"></param>
    public static void SetNotes(NotesHolder notesHolder)
    {
        if (SaveData.noteDatas == null)
        {
            SaveData.noteDatas = new List<NoteData>();
        }

        var position = notesHolder.rectTransform.position;
        NoteData notesData = new NoteData
        {
            XAxis = position.x,
            YAxis = position.y,
            ZAxis = position.z
        };
        Debug.Log("Change position");
        // SaveData.noteDatas.FirstOrDefault(t => t.Id == notesHolder.name).XAxis = notesData.XAxis;
        // SaveData.noteDatas.FirstOrDefault(t => t.Id == notesHolder.name).YAxis = notesData.YAxis;
        // SaveData.noteDatas.FirstOrDefault(t => t.Id == notesHolder.name).ZAxis = notesData.ZAxis;
    }
}
