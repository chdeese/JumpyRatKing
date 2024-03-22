using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentEulerAngle = transform.rotation.eulerAngles;

        float rotateAmount = 90

        transform.rotation = Quaternion.Euler(currentEulerAngle.x, currentEulerAngle.y + 90, currentEulerAngle.z);
    }
}
