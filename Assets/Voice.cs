using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice : MonoBehaviour
{
    public float volume = 0.1f;

    public double sample_freq = 48000.0;

    public bool playing = false;
    public int note = 0;

    public bool clean = false;

    public Envelope envelope;
    public Oscillator oscillator;
    private DownSample downSample;
    private const int NUM_DELAYS = 8;
    private Delay[] delays = new Delay[NUM_DELAYS];

    void Awake() {
        envelope = GetComponent<Envelope>();
        oscillator = GetComponent<Oscillator>();
        downSample = GetComponent<DownSample>();
        for (int i = 0; i < NUM_DELAYS; i++)
        {
            int delayLength = (int) (sample_freq / 2f) * (i + 1);
            delays[i] = new Delay(delayLength);
        }
        envelope.amplifier = volume;
    }


    void Update()
    {

    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            envelope.Update();

            float sample =(float)(envelope.GetLevel() * oscillator.Tick());
            float downSampled = downSample.Tick(sample);

            float delayedSample = 0f;
            for (int j = 0; j < NUM_DELAYS; j++)
            {
                delayedSample += delays[j].Tick(downSampled) * Mathf.Pow(0.5f, (j + 1));
            }

            float finalSample = downSampled + delayedSample;

            if (clean) finalSample = sample;

            data[i] = finalSample;
            if (channels == 2) data[i+1] = data[i];
        }
    }
}
