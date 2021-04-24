using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synth : MonoBehaviour
{
    public float volume = 0.1f;

    public double sample_freq = 48000.0;

    private Envelope envelope;
    private Oscillator oscillator;
    private DownSample downSample;

    void Awake() {
        // frequencies = new float[8];
        envelope = GetComponent<Envelope>();
        oscillator = GetComponent<Oscillator>();
        downSample = GetComponent<DownSample>();
        envelope.amplifier = volume;
    }


    void Update()
    {
        // check for keyboard events
        if (Input.GetKeyDown(KeyCode.Space))
        {
            envelope.KeyOn();
            oscillator.SetNote(55);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            envelope.KeyOff();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            envelope.KeyOn();
            oscillator.SetNote(48);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            envelope.KeyOff();
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            envelope.Update();

            float sample =(float)(envelope.GetLevel() * oscillator.Tick());
            float downSampled = downSample.Tick(sample);

            data[i] = downSampled;
            if (channels == 2) data[i+1] = data[i];
        }
    }
}
