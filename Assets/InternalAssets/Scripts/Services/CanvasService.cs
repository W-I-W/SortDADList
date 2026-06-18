
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;

public class CanvasService : MonoBehaviour
{
    [SerializeField] private DADPanel m_DADPanel;
    [SerializeField] private List<ElementContainer> m_Containers;


    public int countContainers => m_Containers.Count;

    public DADPanel dadPanel => m_DADPanel;

    public ElementContainer GetContainer(int index)
    {
        if (index < 0 || index >= m_Containers.Count)
            return null;

        return m_Containers[index];
    }
}
