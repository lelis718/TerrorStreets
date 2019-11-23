using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider2D))]
public class MagicRock : MonoBehaviour
{

    CarBehaviour car;
    Collider2D currentCollider;
    MeshRenderer currentRenderer;
    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<CarBehaviour>();
        currentCollider = GetComponent<Collider2D>();
        currentRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (car.HasActivatedMagic())
        {
            currentCollider.enabled = false;
            currentRenderer.enabled = false;
        }
        else
        {
            currentCollider.enabled = true;
            currentRenderer.enabled = true;
        }
    }
}
