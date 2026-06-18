

using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ElementCollection : MonoBehaviour
{
    [SerializeField] private ElementAdded m_ElementAdded;
    [SerializeField] private ElementContainer m_Container;



    private void Awake()
    {
        m_ElementAdded.Init(m_Container.database);
    }

    private void OnEnable()
    {
        m_ElementAdded?.OnEnable();
    }

    private void OnDisable()
    {
        m_ElementAdded?.OnDisable();
    }


}


[System.Serializable]
public class ElementModel
{

}


[System.Serializable]
public class ElementAdded
{
    [SerializeField] private Transform m_ParentElement;
    [Space]
    [SerializeField] private Button m_ButtonAdd;
    [Space]
    [SerializeField] private Element m_PrefabElement;
    [Space]
    [SerializeField] private TMP_InputField m_InputFieldName;
    [SerializeField] private TMP_InputField m_InputFieldAge;

    private DatabaseElement m_Database;


    public void Init(DatabaseElement collection)
    {
        m_Database = collection;
    }

    public void OnEnable()
    {
        m_ButtonAdd.onClick.AddListener(OnClickButtonAdd);
    }

    public void OnDisable()
    {
        m_ButtonAdd.onClick.RemoveListener(OnClickButtonAdd);
    }

    private void OnClickButtonAdd()
    {
        Element element = InstantiateElement();
        m_Database.AddElement(element);
    }

    private Element InstantiateElement()
    {
        if (TryAddElement(out string name, out int age))
        {
            Element element = Object.Instantiate(m_PrefabElement, m_ParentElement);
            element.Init(name, age);
            return element;
        }
        return null;
    }

    private bool TryAddElement(out string name, out int age)
    {
        name = string.Empty;
        age = 0;

        if (m_InputFieldName.text == null || m_InputFieldName.text.Trim() == string.Empty) return false;
        if (m_InputFieldAge.text == null || m_InputFieldAge.text.Trim() == string.Empty) return false;

        name = m_InputFieldName.text;
        age = int.Parse(m_InputFieldAge.text);

        if (age <= 0) return false;

        return true;
    }
}


[System.Serializable]
public class DatabaseElement
{
    private List<Element> m_Elements;

    public int count => m_Elements.Count;

    public void Init()
    {
        m_Elements = new List<Element>();
    }

    public void AddElement(Element element)
    {
        m_Elements.Add(element);
    }

    public Element GetElement(int index)
    {
        if (index < 0 || index >= m_Elements.Count)
            return null;
        return m_Elements[index];
    }

    public void InsertElement(int index, Element element)
    {
        m_Elements.Insert(index, element);
    }

    public void RemoveElement(Element element)
    {
        m_Elements.Remove(element);
    }

    public void SortName()
    {
        m_Elements.Sort((a, b) => string.Compare(a.textName, b.textName));
    }

    public void SortAge()
    {
        m_Elements.Sort((a, b) => a.textAge.CompareTo(b.textAge));
    }

    public void Clear()
    {
        m_Elements.Clear();
    }
}
