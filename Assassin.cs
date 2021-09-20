using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour
{
    public GameObject mine;

    public float sightRange;
    public float mineMax;
    public float mineUp;

    public Vector2 throwDir;

    private bool thrown;

    public void Update(){
        Collider[] hits = Physics.OverlapSphere(transform.position, sightRange);
        foreach(Collider c in hits){
            if(c.GetComponent<CarController>() && !thrown){
                Throw();
                thrown = true;
            }
        }
    }

    public void Throw(){
        Rigidbody m = Instantiate(mine, transform.position,transform.rotation).GetComponent<Rigidbody>();
        float dist = Random.Range(3,mineMax);
        m.velocity = new Vector3(throwDir.x * dist, mineUp, throwDir.y * dist);
    }
}
