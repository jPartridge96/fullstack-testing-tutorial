using TMPro;
using UnityEngine;

public class TextMeshProWrapper : IText
{
    TextMeshProUGUI tmpro;

    public TextMeshProWrapper(TextMeshProUGUI tmpro)
    {
        this.tmpro = tmpro;
    }

    public string text { get => tmpro.text; set => tmpro.text = value; }

    public Color color { get => tmpro.color; set => tmpro.color = value; }
}