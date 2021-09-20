using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float force;
    public float lift;

    public AudioSource explosion;
    public AudioSource drop;

    void OnCollisionEnter(Collision col){
        if(col.gameObject.GetComponent<CarController>()){
            col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 5, lift);
            col.gameObject.GetComponent<CarController>().crashed = true;
            FindObjectOfType<GameController>().Lose();
            GetComponent<ParticleSystem>().Play();
            explosion.Play();
            Destroy(transform.GetChild(1).gameObject);
            Destroy(transform.GetChild(0).gameObject);
            Destroy(this);
        }
        if(col.transform.CompareTag("Floor"))
            drop.Play();

        if(col.transform.CompareTag("Side")){
            Physics.IgnoreCollision(this.GetComponent<Collider>(), col.gameObject.GetComponent<Collider>());
        }

    }
}
