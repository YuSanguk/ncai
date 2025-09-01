using System;

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