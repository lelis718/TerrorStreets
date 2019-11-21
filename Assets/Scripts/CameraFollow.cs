using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    Vector3 playerOffset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerOffset = this.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var vectx = player.transform.position.x + playerOffset.x;
        var vecty = this.transform.position.y;
        var vectz = this.transform.position.z;
        this.transform.position = new Vector3(vectx, vecty, vectz);
    }
}
