﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBall : MonoBehaviour {

    private Vector3 ShootDirection;
    float moveSpeed = 5.0f;
    float hitRange = 1;

    float BallValue = 0;
    
    ElementType PowerElement;

    public MeshRenderer PowerRenderer;
	
    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position += ShootDirection * moveSpeed * Time.deltaTime;
        PowerRenderer.material.color = ElementDefine.GetElementColor(PowerElement);

        Collider[] PoweredColliders = Physics.OverlapSphere(transform.position, hitRange);
        List<GameObject> ColliderList = new List<GameObject>();
        foreach (Collider collider in PoweredColliders)
        {
            if (collider.gameObject.tag == "Element" && !collider.isTrigger)
            //if (collider.gameObject.tag != "Ground" && !collider.isTrigger && collider.gameObject.tag != "Player")
            {
                MeshRenderer mr = collider.gameObject.GetComponent<MeshRenderer>();
                ElementScript es = collider.gameObject.GetComponent<ElementScript>();
                if (es)
                {
                    ColliderList.Add(collider.gameObject);
                    //ElementColor = mr.material.color;
                }
            }
        }

        foreach (GameObject collider in ColliderList)
        {
            ElementScript es = collider.gameObject.GetComponent<ElementScript>();

            //es.SetPowerElement(PowerElement);
            es.SetPowerElement(PowerElement, BallValue);
            Destroy(this.gameObject);
        }
	}

    public void SetElement(ElementType element, float shootValue)
    {
        PowerElement = element; 
        BallValue = shootValue;
    }

    public void SetDirection(Vector3 dir)
    {
        ShootDirection = Vector3.Normalize(dir);
    }
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

}