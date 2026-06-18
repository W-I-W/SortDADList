using System;
using System.Collections.Generic;
using System.IO;

using Unity.VisualScripting.Antlr3.Runtime;

using UnityEngine;
using UnityEngine.EventSystems;

[DefaultExecutionOrder(-100)]
public class ElementContainer : MonoBehaviour
{
    [SerializeField] private CanvasService m_CanvasService;
    [SerializeField] private RectTransform m_Rect;
    [SerializeField] private Transform m_DropPanel;
    [SerializeField] private Element m_ElementPrefab;


    public DatabaseElement database { get; private set; }

    public int indexDroppable { get; private set; }

    public CanvasService canvasService => m_CanvasService;

    public RectTransform rectTransform => m_Rect;

    private void Awake()
    {
        database = new DatabaseElement();
        database.Init();
    }

    public void ElementInsert(Element element)
    {
        element.transform.SetParent(m_Rect, false);
        element.transform.SetSiblingIndex(indexDroppable);
        database.InsertElement(indexDroppable, element);
    }

    public int OnExit()
    {
        if (m_DropPanel.gameObject.activeSelf)
        {
            VisibleDrop(false);
            return indexDroppable;
        }
        indexDroppable = -1;
        return indexDroppable;
    }

    public void RebuildUI()
    {
        for (int i = 0; i < database.count; i++)
        {
            Element element = database.GetElement(i);
            element.transform.SetParent(m_Rect, false);
            element.transform.SetSiblingIndex(i);
        }
    }

    public void SetIndexPositionDrop(Vector2 screenPosition)
    {
        bool isInside = RectTransformUtility.RectangleContainsScreenPoint(
    m_Rect,
    screenPosition,
    Camera.main);

        VisibleDrop(isInside);

        if (!isInside)
            return;

        int index = CalculateDropIndex(screenPosition);
        indexDroppable = index;
        MoveDropIndicator(index);
    }

    public void Save(string fileName)
    {
        ElementSaveData data = new ElementSaveData();
        Debug.Log(database.count);
        for (int i = 0; i < database.count; i++)
        {
            Element element = database.GetElement(i);

            data.Elements.Add(new ElementData
            {
                Name = element.textName,
                Age = element.textAge
            });
        }

        JsonSaveSystem.Save(fileName, data);
    }

    public void Load(string fileName)
    {
        ElementSaveData data = JsonSaveSystem.Load<ElementSaveData>(fileName);

        if (data == null)
            return;

        Debug.Log(data.Elements.Count);
        for (int i = database.count - 1; i >= 0; i--)
        {
            Element element = database.GetElement(i);
            Destroy(element.gameObject);
        }
        database.Clear();

        foreach (var e in data.Elements)
        {
            Element element = CreateElement();
            Debug.Log(element);

            element.Init(e.Name, e.Age);

            database.AddElement(element);
            //database.InsertElement(database.count, element);
        }

        RebuildUI();
    }

    private void VisibleDrop(bool isActive)
    {
        m_DropPanel.gameObject.SetActive(isActive);
    }

    private void MoveDropIndicator(int index)
    {
        m_DropPanel.SetSiblingIndex(index);
    }

    private int CalculateDropIndex(Vector2 screenPosition)
    {
        RectTransform container = m_Rect;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            container,
            screenPosition,
            Camera.main,
            out Vector2 localPoint
        );

        int index = 0;

        for (int i = 0; i < container.childCount; i++)
        {
            RectTransform child = container.GetChild(i) as RectTransform;

            if (child == m_DropPanel)
                continue;

            float childY = child.anchoredPosition.y;

            if (localPoint.y > childY)
                return index;

            index++;
        }

        return index;
    }

    private Element CreateElement()
    {
        Element element = Instantiate(m_ElementPrefab, m_Rect);
        return element;
    }
}


[Serializable]
public class ElementSaveData
{
    public List<ElementData> Elements = new List<ElementData>();
}

[Serializable]
public class ElementData
{
    public string Name;
    public string Age;
}

public static class JsonSaveSystem
{
    private static string PathFile(string name)
    {
        return Path.Combine(Application.persistentDataPath, name + ".json");
    }

    public static void Save<T>(string fileName, T data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(PathFile(fileName), json);
        Debug.Log(File.ReadAllText(PathFile("elements")));
    }

    public static T Load<T>(string fileName)
    {
        string path = PathFile(fileName);

        if (!File.Exists(path))
            return default;

        string json = File.ReadAllText(path);
        Debug.Log(json);
        return JsonUtility.FromJson<T>(json);
    }
}
