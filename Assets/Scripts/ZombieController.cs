using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ZombieController : MonoBehaviour
{
    public float distanceToStartHearing = 5;
    private AudioSource audio;
    private GameObject player;
    private SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        sp = GetComponentInChildren<SpriteRenderer>();
        audio.volume = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(sp.name != "bloodsplash")
        {
            var d = player.transform.position - this.transform.position;
            if (d.magnitude > -distanceToStartHearing && d.magnitude < distanceToStartHearing)
            {
                var volPercent = 1 - Mathf.Abs(d.magnitude / distanceToStartHearing);
                audio.volume = volPercent;
            }
            else
            {
                audio.volume = 0;
            }

        } else
        {
            audio.volume = 0;
        }
    }
}
