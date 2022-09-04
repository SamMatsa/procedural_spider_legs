using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to rotate the Mop in the direction of the player movement - thats it!
public class MopScript : MonoBehaviour
{

    [SerializeField] float _turnSpeed = 360;
    [SerializeField] Transform body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    void Look()
    {
        Vector3 _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 _upwardsVector = new Vector3(0, 0.25f, 0);
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input+ _upwardsVector, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);

    }

}
