using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player"))
            FindObjectOfType<GameController>().Win();
    }
}
