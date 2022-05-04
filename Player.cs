using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float Dampening;
    public FixedJoystick fixedJoystick;
    public Rigidbody2D rb;
    public GameObject MC;
    public GameObject Iris;

    void Start(){

    }

    public void FixedUpdate()
    {
        if(MC.GetComponent<Game>().Playing){
            MC.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            Vector3 direction = new Vector3(fixedJoystick.Horizontal, fixedJoystick.Vertical, 0f);
            transform.Translate(direction * speed  * Time.deltaTime, Space.World);
            Iris.transform.position = new Vector3((fixedJoystick.Horizontal / Dampening) + transform.position.x, (fixedJoystick.Vertical / Dampening) + transform.position.y, -1f);
        // Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        // rb.AddForce(direction * speed, ForceMode2D.Impulse);
        // rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Blade"){
            MC.GetComponent<Game>().Finished();
        }

        if(other.tag == "Wall"){
            MC.GetComponent<Game>().Finished();
        }

        if(other.tag == "Collectable"){
            MC.GetComponent<Game>().AddPoint();
            Destroy(other.gameObject);
        }
        
    }

    
}
