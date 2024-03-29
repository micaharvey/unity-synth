﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class SynthController : MonoBehaviour
{
    public ParticleSystem WhiteParticleSystem;
    public ParticleSystem BlueParticleSystem;
    public int vIndex = 0;
    private static int NUM_VOICES = 8;
    private Voice[] voices = new Voice[NUM_VOICES];
    private Oscillator[] oscs = new Oscillator[NUM_VOICES];
    private Envelope[] envelopes = new Envelope[NUM_VOICES];
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        foreach (Transform child in transform)
        {
            voices[index] = child.GetComponent<Voice>();
            oscs[index] = child.GetComponent<Oscillator>();
            envelopes[index] = child.GetComponent<Envelope>();
            index++;
        }
    }

    public void SetWaveForm(bool sine)
    {
        foreach (Oscillator o in oscs)
        {
            o.square = !sine;
        }
    }

    public void SetReverb(bool reverb)
    {
        foreach (Voice v in voices)
        {
            v.clean = !reverb;
        }

        foreach (Transform child in transform)
        {
            child.GetComponent<AudioReverbFilter>().enabled = reverb;
        }
    }

    public void SetSustain(bool sus)
    {
        foreach (Envelope e in envelopes)
        {
            e.sustain = sus;
        }
    }

    void incrementVoiceIndex()
    {
        vIndex = (vIndex + 1) % NUM_VOICES;
    }

    void Play(int key = 60)
    {
        if (voices[vIndex].playing)
        {
            int originalIndex = vIndex;
            incrementVoiceIndex();
            while (voices[vIndex].playing)
            {
                if (vIndex == originalIndex)
                    return;
                incrementVoiceIndex();
            }
        }
        voices[vIndex].playing = true;
        voices[vIndex].note = key;
        voices[vIndex].envelope.KeyOn();
        voices[vIndex].oscillator.SetNote(key);

        // shine bright like a diamond
        if (vIndex % 2 == 1)
        {
            WhiteParticleSystem.Play();
        }
        else
        {
            BlueParticleSystem.Play();
        }

        incrementVoiceIndex();
    }

    void Off(int key = 60)
    {
        for (int i = 0; i < NUM_VOICES; i++)
        {
            if (voices[i].note == key)
            {
                voices[i].playing = false;
                voices[i].envelope.KeyOff();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check for midi events
        for (int i = 36; i < 127; i++)
        {
            if (MidiMaster.GetKeyDown(0, i)) Play(i);
            if (MidiMaster.GetKeyUp(0, i)) Off(i);
        }

        // check for keyboard events
        if (Input.GetKeyDown(KeyCode.Z)) Play(48);
        if (Input.GetKeyUp(KeyCode.Z)) Off(48);
        if (Input.GetKeyDown(KeyCode.X)) Play(50);
        if (Input.GetKeyUp(KeyCode.X)) Off(50);
        if (Input.GetKeyDown(KeyCode.C)) Play(52);
        if (Input.GetKeyUp(KeyCode.C)) Off(52);
        if (Input.GetKeyDown(KeyCode.V)) Play(53);
        if (Input.GetKeyUp(KeyCode.V)) Off(53);
        if (Input.GetKeyDown(KeyCode.B)) Play(55);
        if (Input.GetKeyUp(KeyCode.B)) Off(55);
        if (Input.GetKeyDown(KeyCode.N)) Play(57);
        if (Input.GetKeyUp(KeyCode.N)) Off(57);
        if (Input.GetKeyDown(KeyCode.M)) Play(59);
        if (Input.GetKeyUp(KeyCode.M)) Off(59);
        if (Input.GetKeyDown(KeyCode.Comma)) Play(60);
        if (Input.GetKeyUp(KeyCode.Comma)) Off(60);
        if (Input.GetKeyDown(KeyCode.Period)) Play(62);
        if (Input.GetKeyUp(KeyCode.Period)) Off(62);
        if (Input.GetKeyDown(KeyCode.Slash)) Play(64);
        if (Input.GetKeyUp(KeyCode.Slash)) Off(64);

        if (Input.GetKeyDown(KeyCode.S)) Play(49);
        if (Input.GetKeyUp(KeyCode.S)) Off(49);
        if (Input.GetKeyDown(KeyCode.D)) Play(51);
        if (Input.GetKeyUp(KeyCode.D)) Off(51);
        if (Input.GetKeyDown(KeyCode.G)) Play(54);
        if (Input.GetKeyUp(KeyCode.G)) Off(54);
        if (Input.GetKeyDown(KeyCode.H)) Play(56);
        if (Input.GetKeyUp(KeyCode.H)) Off(56);
        if (Input.GetKeyDown(KeyCode.J)) Play(58);
        if (Input.GetKeyUp(KeyCode.J)) Off(58);
        if (Input.GetKeyDown(KeyCode.L)) Play(61);
        if (Input.GetKeyUp(KeyCode.L)) Off(61);
        if (Input.GetKeyDown(KeyCode.Semicolon)) Play(63);
        if (Input.GetKeyUp(KeyCode.Semicolon)) Off(63);

        if (Input.GetKeyDown(KeyCode.Q)) Play(48 + 12);
        if (Input.GetKeyUp(KeyCode.Q)) Off(48 + 12);
        if (Input.GetKeyDown(KeyCode.W)) Play(50 + 12);
        if (Input.GetKeyUp(KeyCode.W)) Off(50 + 12);
        if (Input.GetKeyDown(KeyCode.E)) Play(52 + 12);
        if (Input.GetKeyUp(KeyCode.E)) Off(52 + 12);
        if (Input.GetKeyDown(KeyCode.R)) Play(53 + 12);
        if (Input.GetKeyUp(KeyCode.R)) Off(53 + 12);
        if (Input.GetKeyDown(KeyCode.T)) Play(55 + 12);
        if (Input.GetKeyUp(KeyCode.T)) Off(55 + 12);
        if (Input.GetKeyDown(KeyCode.Y)) Play(57 + 12);
        if (Input.GetKeyUp(KeyCode.Y)) Off(57 + 12);
        if (Input.GetKeyDown(KeyCode.U)) Play(59 + 12);
        if (Input.GetKeyUp(KeyCode.U)) Off(59 + 12);
        if (Input.GetKeyDown(KeyCode.I)) Play(60 + 12);
        if (Input.GetKeyUp(KeyCode.I)) Off(60 + 12);
        if (Input.GetKeyDown(KeyCode.O)) Play(62 + 12);
        if (Input.GetKeyUp(KeyCode.O)) Off(62 + 12);
        if (Input.GetKeyDown(KeyCode.P)) Play(64 + 12);
        if (Input.GetKeyUp(KeyCode.P)) Off(64 + 12);
        if (Input.GetKeyDown(KeyCode.LeftBracket)) Play(65 + 12);
        if (Input.GetKeyUp(KeyCode.LeftBracket)) Off(65 + 12);
        if (Input.GetKeyDown(KeyCode.RightBracket)) Play(67 + 12);
        if (Input.GetKeyUp(KeyCode.RightBracket)) Off(67 + 12);
        if (Input.GetKeyDown(KeyCode.Backslash)) Play(69 + 12);
        if (Input.GetKeyUp(KeyCode.Backslash)) Off(69 + 12);
        if (Input.GetKeyDown(KeyCode.Pipe)) Play(69 + 12);
        if (Input.GetKeyUp(KeyCode.Pipe)) Off(69 + 12);

        if (Input.GetKeyDown(KeyCode.Alpha2)) Play(49 + 12);
        if (Input.GetKeyUp(KeyCode.Alpha2)) Off(49 + 12);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Play(51 + 12);
        if (Input.GetKeyUp(KeyCode.Alpha3)) Off(51 + 12);
        if (Input.GetKeyDown(KeyCode.Alpha5)) Play(54 + 12);
        if (Input.GetKeyUp(KeyCode.Alpha5)) Off(54 + 12);
        if (Input.GetKeyDown(KeyCode.Alpha6)) Play(56 + 12);
        if (Input.GetKeyUp(KeyCode.Alpha6)) Off(56 + 12);
        if (Input.GetKeyDown(KeyCode.Alpha7)) Play(58 + 12);
        if (Input.GetKeyUp(KeyCode.Alpha7)) Off(58 + 12);
        if (Input.GetKeyDown(KeyCode.Alpha9)) Play(61 + 12);
        if (Input.GetKeyUp(KeyCode.Alpha9)) Off(61 + 12);
        if (Input.GetKeyDown(KeyCode.Alpha0)) Play(63 + 12);
        if (Input.GetKeyUp(KeyCode.Alpha0)) Off(63 + 12);
        if (Input.GetKeyDown(KeyCode.Equals)) Play(65 + 12);
        if (Input.GetKeyUp(KeyCode.Equals)) Off(65 + 12);
    }
}
