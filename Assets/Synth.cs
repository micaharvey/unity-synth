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
    private const int numDelays = 8;
    private Delay[] delays = new Delay[numDelays];
    // private float[] delaySamples = new float[numDelays];
    

    void Awake() {
        // frequencies = new float[8];
        envelope = GetComponent<Envelope>();
        oscillator = GetComponent<Oscillator>();
        downSample = GetComponent<DownSample>();
        for (int i = 0; i < numDelays; i++)
        {
            int delayLength = (int) (sample_freq / 2f) * (i + 1);
            delays[i] = new Delay(delayLength);
        }
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

            float delayedSample = 0f;
            for (int j = 0; j < numDelays; j++)
            {
                delayedSample += delays[j].Tick(downSampled) * Mathf.Pow(0.5f, (j + 1));
            }

            float finalSample = downSampled + delayedSample;

            data[i] = finalSample;
            if (channels == 2) data[i+1] = data[i];
        }
    }
}
