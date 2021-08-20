using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject steer;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float animationSpeed = 1f;

    private bool isRotating = false;



    void Update(){
      Debug.Log("Rotating Steer");
      if (isRotating){
        Vector3 targetDir = target.position - steer.transform.position;
        float singleStep = animationSpeed * Time.deltaTime;

        Vector3 rotationDirection = Vector3.RotateTowards(
        steer.transform.forward,
        targetDir,
        singleStep,
        0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, rotationDirection, Color.red);

        steer.transform.rotation = Quaternion.LookRotation(rotationDirection);

      }
    }

    public void OnMovementInput(){
      if (!isRotating) {
        isRotating = true;
      }
    }

    // TODO gets called too often
    public void OnMovementInputStopped(){
      if (isRotating) {
        isRotating = false;
      }
    }

}
