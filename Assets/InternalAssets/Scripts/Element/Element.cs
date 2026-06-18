using TMPro;

using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextName;
    [SerializeField] private TextMeshProUGUI m_TextAge;

    public string textName => m_TextName.text;
    public string textAge => m_TextAge.text;

    public void Init(string name, int age)
    {
        m_TextName.text = name;
        m_TextAge.text = age.ToString();
    }

    public void Init(string name, string age)
    {
        m_TextName.text = name;
        m_TextAge.text = age;
    }
}
