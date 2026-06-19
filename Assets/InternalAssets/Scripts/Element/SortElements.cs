using UnityEngine;

public class SortElements : MonoBehaviour
{
    [SerializeField] private ElementContainer m_Container;

    public void SortName(bool isValue)
    {
        m_Container.database.SortName(!isValue);

        m_Container.RebuildUI();
    }

    public void SortAge(bool isValue)
    {
        m_Container.database.SortAge(!isValue);

        m_Container.RebuildUI();
    }
}
