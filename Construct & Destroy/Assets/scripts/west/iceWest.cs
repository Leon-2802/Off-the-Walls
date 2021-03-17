﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceWest : MonoBehaviour
{
    private void Start() {
        Destroy(gameObject, 0.5f);
    }
     private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("playerEast") || other.CompareTag("Wall") || other.CompareTag("coverEast")) {
            Destroy(gameObject);
        }
    }
}
