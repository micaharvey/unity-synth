using UnityEngine;
using System.Collections;

public class Envelope : MonoBehaviour {
    public float attack = 0.05f;
    public float release = 0.2f;
    public float current = 0.0f;
    public bool sustain = false;
    public float amplifier = 1.0f;

    private int s_rate = 48000;

    public float delta = 0.0f;

    public void KeyOn() {
        delta = 1.0f / (attack * s_rate );
    }

    public void KeyOff() {
        delta = -1.0f / (release * s_rate );
    }

    public void Update() {
        if (delta > 0.0f) {
            current += delta;
            if (current >= 1.0f) {
                current = 1.0f;
                if (!sustain) KeyOff();
            }
        } else {
            current = Mathf.Max(current + delta, 0.0f);
        }
    }

    public float GetLevel() {
        return current * amplifier;
    }
}
