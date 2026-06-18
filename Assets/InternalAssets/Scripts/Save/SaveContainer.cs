using System.Collections.Generic;

using UnityEngine;

public class SaveContainer : MonoBehaviour
{
    [SerializeField] private List<ElementContainer> m_Container;

    public void Save()
    {
        for(int i = 0; i < m_Container.Count; i++)
        {
            m_Container[i].Save($"container_{i}");
        }

    }

    public void Load()
    {
        for (int i = 0; i < m_Container.Count; i++)
        {
            m_Container[i].Load($"container_{i}");
        }
    }
}
