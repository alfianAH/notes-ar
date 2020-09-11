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
    public float XAxis, YAxis, ZAxis;
    public string Id;
    public string TitleText;
    public string BodyText;
}
