using UnityEngine;

public class SortElements : MonoBehaviour
{
    [SerializeField] private ElementContainer m_Container;

    public void SortName()
    {
        m_Container.database.SortName();

        m_Container.RebuildUI();
    }

    public void SortAge()
    {
        m_Container.database.SortAge();

        m_Container.RebuildUI();
    }
}
