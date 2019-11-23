using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInRaceFollow : MonoBehaviour
{
    public float turnVelocity = 5.0f;
    GameObject player;
    float distanceFromCamera;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var vect = (this.transform.position - player.transform.position);
        vect.z = 0;
        distanceFromCamera = vect.magnitude;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        var rotation = player.transform.localRotation;
        this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, rotation, turnVelocity * Time.deltaTime);

        var vect = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z) + (distanceFromCamera * player.transform.right);
        this.transform.position = vect;
    }
}
