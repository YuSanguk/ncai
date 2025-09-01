using System; // Serializable을 사용하기 위해 필요

[Serializable]
public class ElementDefine
{
    public string name;
    public int count;

    public ElementDefine(string elementName, int initialCount)
    {
        name = elementName;
        count = initialCount;
    }
}