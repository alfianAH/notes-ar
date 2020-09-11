using System;
using System.Collections.Generic;

[Serializable]
public struct SaveData
{
    public List<NoteData> noteDatas;
}

[Serializable]
public struct NoteData
{
    public float xAxis, yAxis, zAxis;
    public string id;
    public string titleText;
    public string bodyText;
}
