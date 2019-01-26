using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
	public GameObject northArrow;
    void Start()
    {
        
    }
	
    void Update()
    {
		northArrow.transform.up = Vector3.ProjectOnPlane(Vector3.forward, transform.up).normalized;
	}
}
