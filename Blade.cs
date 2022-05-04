using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    
    public string Direction = "Left";
    public float Speed = 0.05f;
    public float RotationSpeed = 1f;
    public GameObject MC;

	void Start(){
		MC = GameObject.Find("Main Camera");
	}

    // Update is called once per frame
    void Update()
    {
		if(MC.GetComponent<Game>().Playing){
        
            switch(Direction){
                case "Up":
                    transform.position += new Vector3(0f, Speed, 0f);
                    break;
                case "Down":
                    transform.position += new Vector3(0f, -Speed, 0f);
                    break;
                case "Right":
                    transform.position += new Vector3(Speed, 0f, 0f);
                    break;
                case "Left":
                    transform.position += new Vector3(-Speed, 0f, 0f);
                    break;
                default:
                    break;
            }

            transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * RotationSpeed));
        }

        if(transform.position.x < -100 || transform.position.y < -100){
            Destroy(gameObject);
        }
    }

}
