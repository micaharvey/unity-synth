using UnityEngine;
using UnityEngine.UI;

public class ReverbToggle : MonoBehaviour
{

    Toggle m_Toggle;
    void Start()
    {
        m_Toggle = GetComponent<Toggle>();

        m_Toggle.onValueChanged.AddListener(delegate
        {
            OnToggle(m_Toggle);
        });
    }

    void OnToggle(Toggle change)
    {
        GameObject.Find("Synth").GetComponent<SynthController>().SetReverb(m_Toggle.isOn);
    }
}
