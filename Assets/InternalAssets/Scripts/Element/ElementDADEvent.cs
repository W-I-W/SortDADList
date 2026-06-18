using UnityEngine;
using UnityEngine.EventSystems;

public class ElementDADEvent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    [SerializeField] private ElementDragAndDrop m_ElementDAD;


    public void OnPointerDown(PointerEventData eventData)
    {

        m_ElementDAD.OnDownClick();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_ElementDAD.OnDrop();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_ElementDAD.OnBeginDrag(eventData.position);
        //    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //m_ElementDAD.dadPanel.rectTransform,
        //eventData.position,
        //m_Camera,
        //out Vector2 localPoint);

        //    m_DragOffset = m_ElementDAD..anchoredPosition - localPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_ElementDAD.OnDrag(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
