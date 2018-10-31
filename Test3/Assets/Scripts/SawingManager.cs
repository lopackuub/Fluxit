﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawingManager : MonoBehaviour {
    public static SawingManager Instance = null;
    public Transform contact_position = null;
    public GameObject Foot = null;
    public Vector3 NormalVector = new Vector3(1,0,0);
    public Vector3 Privious_contact_Position;
    public bool registered = false;
    public float max_tolerence;
    public float current_tolerence;
    private void Awake()
    {
        Instance = this;
    }
    public bool Register(Transform contact_position,GameObject Foot) {
        this.Foot = Foot;
        this.contact_position = contact_position;
        this.registered = true;
        this.Privious_contact_Position = contact_position.position;
        current_tolerence = max_tolerence;
        return true;
    }
    public bool Resign() {
        this.contact_position = null;
        this.registered = false;
        this.Foot = null;
        this.current_tolerence = 0;
        return true;
    }
    public static float LenProjAB(Vector3 a, Vector3 b) {
        float result = a.x * b.x + a.y * b.y + a.z * b.z;
        return Mathf.Abs(result);
    }
    private void Update()
    {
        if (registered) {
            Vector3 delta = contact_position.position - Privious_contact_Position;
            Privious_contact_Position = contact_position.position;
            current_tolerence -= Mathf.Min(0.6f, LenProjAB(NormalVector, delta))*Time.deltaTime;
            if (current_tolerence < 0) {
                //The foot is cut off do something
                this.Foot = null;
                this.Resign();
            }
        }
    }
    
}