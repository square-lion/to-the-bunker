using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class CarController : MonoBehaviour
{
    public float moveSpeed;
    private float curMoveSpeed = 300;
    public float acceleration;
    public float turnSpeed;

    public Transform pivotPoint;
    public Transform camPivot;
    public Transform camStart;
    public Transform icon;
    public Camera camera;
    public float camDelay;
    public Vector3 velocity = Vector3.zero;

    private Rigidbody rb;

    public AudioSource drive;

    private float v;
    private float h;

    public Transform groundCheck;
    private bool grounded;
    public bool crashed;
    public bool start = false;

    public Slider left;
    public Slider forward;

    void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        #if UNITY_STANDALONE || UNITY_EDITOR	
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        #endif

        #if UNITY_IOS || UNITY_ANDROID
        v = forward.value;
        h = left.value;
        #endif

        grounded = Physics.OverlapSphere(transform.position, 5, 1<<LayerMask.NameToLayer("Water")).Length != 0;

        if(grounded && !crashed && start){
            Vector3 velocity = (transform.forward * v) * curMoveSpeed * Time.fixedDeltaTime;
            velocity.y = rb.velocity.y;


            if(v != 0){
                curMoveSpeed += Time.deltaTime * acceleration;
                //Don't Play Audio
                //if(!drive.isPlaying)
                    //drive.Play();
            }
            if(curMoveSpeed >= moveSpeed)
                curMoveSpeed = moveSpeed;
            if(v == 0){
                curMoveSpeed = 300;

            }

            rb.velocity = velocity;

            if(v > 0)
                transform.RotateAround(pivotPoint.position,Vector3.up,h * turnSpeed * Time.fixedDeltaTime);
            else if(v < 0)
                 transform.RotateAround(pivotPoint.position,Vector3.up,-h * turnSpeed * Time.fixedDeltaTime);
        }

        if(crashed)
            drive.Stop();

        camDelay -= Time.deltaTime;
        if(camDelay < 0){
        Quaternion rotation = Quaternion.LookRotation(camPivot.position - camera.transform.position, Vector3.up);
        camera.transform.rotation = rotation;
            if(!crashed)
                camera.transform.position = Vector3.SmoothDamp(camera.transform.position, camStart.position, ref velocity, 0.3f);
            else{
                if(icon.transform.childCount > 0)
                    icon.transform.GetChild(0).parent = null;
        }
        }
        icon.position = new Vector3(transform.position.x, transform.position.y + 40, transform.position.z);
        icon.rotation = Quaternion.Euler(90, transform.eulerAngles.y, 0);
    }

    public void LeftEnd(){
        left.value = 0;
    }

    public void ForEnd(){
        forward.value = 0;
    }

    public void DeactivateTouchControls(){
        left.gameObject.SetActive(false);
        forward.gameObject.SetActive(false);
    }
}
