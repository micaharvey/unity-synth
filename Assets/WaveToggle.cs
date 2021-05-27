using UnityEngine;
using UnityEngine.UI;

public class WaveToggle : MonoBehaviour
{
    Toggle m_Toggle;
    public Image m_squareWaveImage;
    void Start()
    {
        m_Toggle = GetComponent<Toggle>();

        m_Toggle.onValueChanged.AddListener(delegate
        {
            OnWaveToggle(m_Toggle);
        });

        m_squareWaveImage.gameObject.SetActive(!m_Toggle.isOn);
    }

    void OnWaveToggle(Toggle change)
    {
        m_squareWaveImage.gameObject.SetActive(!m_Toggle.isOn);
        GameObject.Find("Synth").GetComponent<SynthController>().SetWaveForm(m_Toggle.isOn);
    }
}
