using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjects : MonoBehaviour
{
    public int shotPower;
    public int rayLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.yellow);
        RaycastHit hit;
        if (Input.GetButtonDown("Fire1"))
            if (Physics.Raycast(ray, out hit))
                if (hit.rigidbody) {
                    hit.rigidbody.AddForce(ray.direction*shotPower, ForceMode.Impulse);
                }
    }
}
