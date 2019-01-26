using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rescuer : MonoBehaviour
{
	public Transform compassInitialObject;
	public Transform compassTargetObject;

	public Transform mapInitialObject;
	public Transform mapTargetObject;

	public enum ObjectStatus
	{
		Hidden,
		Hiding,
		Showing,
		Shown,
	}

	public ObjectStatus mapStatus;
	public ObjectStatus compassStatus;

	public Transform map;
	public Transform compass;

	private Coroutine currentCoroutine;

    void Start()
    {
        
    }
	
    void Update()
    {
		if(Input.GetKey(KeyCode.Y))
		{
			if(mapStatus == ObjectStatus.Hidden)
			{
				mapStatus = ObjectStatus.Showing;
				currentCoroutine = StartCoroutine(MoveObject(map, mapTargetObject, () => mapStatus = ObjectStatus.Shown));
			}
			else if(mapStatus == ObjectStatus.Hiding)
			{
				StopCoroutine(currentCoroutine);
				mapStatus = ObjectStatus.Showing;
				currentCoroutine = StartCoroutine(MoveObject(map, mapTargetObject, () => mapStatus = ObjectStatus.Shown));
			}
		}
		else
		{ 
			if (mapStatus == ObjectStatus.Shown)
			{
				mapStatus = ObjectStatus.Hiding;
				currentCoroutine = StartCoroutine(MoveObject(map, mapInitialObject, () => mapStatus = ObjectStatus.Hidden));
			}
			else if(mapStatus == ObjectStatus.Showing)
			{
				StopCoroutine(currentCoroutine);
				mapStatus = ObjectStatus.Hiding;
				currentCoroutine = StartCoroutine(MoveObject(map, mapInitialObject, () => mapStatus = ObjectStatus.Hidden));
			}
		}
		
		if (Input.GetKey(KeyCode.U))
		{
			if (compassStatus == ObjectStatus.Hidden)
			{
				compassStatus = ObjectStatus.Showing;
				currentCoroutine = StartCoroutine(MoveObject(compass, compassTargetObject, () => compassStatus = ObjectStatus.Shown));
			}
			else if (compassStatus == ObjectStatus.Hiding)
			{
				StopCoroutine(currentCoroutine);
				compassStatus = ObjectStatus.Showing;
				currentCoroutine = StartCoroutine(MoveObject(compass, compassTargetObject, () => compassStatus = ObjectStatus.Shown));
			}
		}
		else
		{
			if (compassStatus == ObjectStatus.Shown)
			{
				compassStatus = ObjectStatus.Hiding;
				currentCoroutine = StartCoroutine(MoveObject(compass, compassInitialObject, () => compassStatus = ObjectStatus.Hidden));
			}
			else if (compassStatus == ObjectStatus.Showing)
			{
				StopCoroutine(currentCoroutine);
				compassStatus = ObjectStatus.Hiding;
				currentCoroutine = StartCoroutine(MoveObject(compass, compassInitialObject, () => compassStatus = ObjectStatus.Hidden));
			}
		}
	}

	IEnumerator MoveObject(Transform obj, Transform target, Action onFinished)
	{
		float currentTime = 0;
		float totalTime = 1.0f;

		Vector3 initialPos = obj.position;
		Quaternion initialRot = obj.rotation;

		Vector3 targetPos = target.position;
		Quaternion targetRot = target.rotation;

		while(currentTime < totalTime)
		{
			obj.transform.position = Vector3.Lerp(initialPos, targetPos, currentTime / totalTime);
			obj.transform.rotation = Quaternion.Slerp(initialRot, targetRot, currentTime / totalTime);
			yield return new WaitForEndOfFrame();
			currentTime = currentTime + Time.deltaTime;
		}

		if(onFinished != null)
		{
			onFinished();
		}
	}
}
