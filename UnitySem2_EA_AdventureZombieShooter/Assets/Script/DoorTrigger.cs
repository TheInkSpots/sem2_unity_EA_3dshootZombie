using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            anim.SetTrigger("open");
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        anim.SetTrigger("close");
    }

}
