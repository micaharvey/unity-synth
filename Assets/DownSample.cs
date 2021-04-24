using System.Collections;
using UnityEngine;

public class DownSample : MonoBehaviour
{
    public int counter = 0;
    public float sampled = 0.0f;

    public int interval = 10000;
    public float mix = 1.0f;

    public float Tick(float input) {
        if (counter++ % interval == 0) {
            sampled = input;
        }
        return input * (1.0f - mix) + sampled * mix;
    }
}
