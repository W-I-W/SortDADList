using System;

using TMPro;

using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private ElementContainer m_Container;
    [SerializeField] private TextMeshProUGUI m_TextCount;
    [SerializeField] private string m_CountText;

    private void OnEnable()
    {
        m_Container.database.onCountChanged += OnCountChanged;
    }

    private void OnCountChanged(int value)
    {
        m_TextCount.text = m_CountText + value.ToString();
    }

    private void OnDisable()
    {
        m_Container.database.onCountChanged -= OnCountChanged;
    }
}
