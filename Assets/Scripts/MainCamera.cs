using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    [SerializeField] private Transform target;
    private Transform cam;
   
    private void Start()
    {
        cam = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(target.position.x + 6f, cam.position.y, cam.position.z);

    }
}
