﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{

    Car car;
    Vector3 goal;
    int waypoint = 1;

    public float waypointDistance = 5f;
    public GameObject gizmo;

    void Start()
    {
        car = GetComponent<Car>();
        goal = GetPositionAroundObject(car.CheckpointPassed());
    }

    void Update()
    {
        //Car steering
        if (waypoint != car.nextCheckpoint)
        {
            waypoint = car.nextCheckpoint;
            goal = GetPositionAroundObject(car.gameController.checkPoints[waypoint]);
        }
        Vector3 targetDir = goal - transform.position;
        float step = car.rotationSpeed * Time.deltaTime * Mathf.Deg2Rad;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);

        //Goal check
        //if ((transform.position - goal).magnitude < waypointDistance)
        //{
        //    waypoint++;
        //    
        //}
    }

    void FixedUpdate()
    {
        //Car movement
        car.Accelerate(1f);

        //Prueba
        ////Method 1
        //Vector3 targetDir = goal - transform.position;
        //float step = car.rotationSpeed * Time.deltaTime;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        //transform.rotation = Quaternion.LookRotation(newDir);

        ////Method2
        ////Vector3 targetDir = nodes[waypointIndex].position - transform.position;
        ////float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
        ////Debug.Log("angle: " + angle);
        ////float rotAngle;
        ////if (Mathf.Abs(angle) > deltaAngle)
        ////{
        ////    if (angle > 0)
        ////    {
        ////        Debug.Log("mayor");
        ////        rotAngle = Time.deltaTime * rotationSpeed;
        ////        if (rotAngle > angle)
        ////        {
        ////            rotAngle = angle;
        ////        }


        ////    }
        ////    else
        ////    {
        ////        Debug.Log("menor");
        ////        rotAngle = Time.deltaTime * -rotationSpeed;
        ////        if (rotAngle < angle)
        ////        {
        ////            rotAngle = angle;
        ////        }
        ////    }
        ////    Debug.Log("rotAngle: " + rotAngle);
        ////    transform.Rotate(Vector3.up, rotAngle);
        ////}

        //car.rigidBody.AddForce(transform.forward * car.accelerationForce, ForceMode.Acceleration);
        //car.rigidBody.velocity = Vector3.ClampMagnitude(car.rigidBody.velocity, car.maxSpeed);

        //car.rigidBody.velocity = car.ForwardVelocity() + car.RightVelocity() * 1 + car.UpVelocity();
    }
    Vector3 GetPositionAroundObject(Transform originalPos)
    {
        Vector3 offset = Random.insideUnitSphere * car.waypointOffset;
        offset.y = transform.position.y;
        Vector2 random = Random.insideUnitCircle * car.waypointOffset;
        Vector3 offset2 = new Vector3(random.x, transform.position.y, random.y);

        if (Vector3.Dot(offset, originalPos.forward) < 0)
        {
            offset = Vector3.Reflect(offset, originalPos.forward);
        }
        
        Vector3 newPos = originalPos.position + offset;
        newPos.y = transform.position.y;
        //Instantiate(gizmo, newPos, Quaternion.identity);
        return newPos;
    }
}