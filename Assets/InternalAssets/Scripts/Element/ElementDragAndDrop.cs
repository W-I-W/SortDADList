using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ElementDragAndDrop : MonoBehaviour
{
    [SerializeField] private RectTransform m_Rect;
    [SerializeField] private Element m_Element;

    private ElementContainer m_Container;
    private DADPanel m_DAD;
    private RectTransform m_PreviousParent;
    private Vector2 m_DragOffset;
    private Camera m_Camera;

    public RectTransform previousParent => m_PreviousParent;

    public DADPanel dadPanel => m_DAD;


    private void Start()
    {
        m_Container = transform.parent.GetComponent<ElementContainer>();
        m_DAD = m_Container.canvasService.dadPanel;
        m_PreviousParent = m_Container.rectTransform;
        m_Camera = Camera.main;
    }

    public void OnDownClick()
    {
        m_Container.database.RemoveElement(m_Element);
        m_Rect.SetParent(m_DAD.rectTransform);
    }

    public void OnDrop()
    {
        int countContainer = m_Container.canvasService.countContainers;
        for (int i = 0; i < countContainer; i++)
        {
            ElementContainer container = m_Container.canvasService.GetContainer(i);
            if (container == null)
                continue;
            int index = container.OnExit();
            if (index != -1)
            {

                container.ElementInsert(m_Element);
                m_Container = container;
                m_PreviousParent = container.rectTransform;
                return;
            }
        }
        ResetDrop();
    }

    public void OnBeginDrag(Vector2 position)
    {
        bool isRectTransform = RectTransformUtility.ScreenPointToLocalPointInRectangle(m_DAD.rectTransform, position, m_Camera, out Vector2 localPoint);
        m_DragOffset = m_Rect.anchoredPosition - localPoint;
    }

    public void OnDrag(Vector2 position)
    {
        bool isRectTransform = RectTransformUtility.ScreenPointToLocalPointInRectangle(m_DAD.rectTransform, position, m_Camera, out Vector2 localPoint);
        m_Rect.anchoredPosition = localPoint + m_DragOffset;
        int countContainer = m_Container.canvasService.countContainers;
        for (int i = 0; i < countContainer; i++)
        {
            ElementContainer container = m_Container.canvasService.GetContainer(i);
            if (container == null)
                continue;
            container.SetIndexPositionDrop(position);
        }
    }

    private void ResetDrop()
    {
        m_Rect.SetParent(previousParent);
        m_Container.database.AddElement(m_Element);
    }
}
