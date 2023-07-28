using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapItems : MonoBehaviour
{
    Transform player;
    bool snapping = false;
    //bool startSnap = false;
    float startTime;
    public float duration;
    Vector3 currentPos;
    Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!snapping) {
            float dist = Vector3.Distance(transform.position, player.position);
            if (dist < 5) {
                snapping = true;
                //if (!startSnap) {
                    startTime = Time.time;
                    //startSnap = true;
                //}
            }
        } else {
            currentPos = transform.position;
            endPos = player.position;
            transform.position = Vector3.Lerp(currentPos, endPos, (Time.time - startTime) / duration);
        }
    }
}
