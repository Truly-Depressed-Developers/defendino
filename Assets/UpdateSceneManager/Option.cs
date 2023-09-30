using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI m_TextMeshPro;
    public UnityEvent<string> OnChoose;
    private string text;

    // Start is called before the first frame update
    void Start()
    {
        text = m_TextMeshPro.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        OnChoose.Invoke(text);
    }

    public void ChangeText(string info) {
        m_TextMeshPro.text = info;
        text = info;
    }
}
