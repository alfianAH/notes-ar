using UnityEngine;
using UnityEngine.UI;

public class NotesHolder : MonoBehaviour
{
    [SerializeField] private Text titleText,
        bodyText;

    public Text TitleText
    {
        get => titleText;
        set => titleText = value;
    }

    public Text BodyText
    {
        get => bodyText;
        set => bodyText = value;
    }
}
