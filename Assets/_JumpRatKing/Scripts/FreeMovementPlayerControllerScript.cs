using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FreeMovementPlayerControllerScript : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    [Tooltip("Speed of Movement")]
    //the speed of the Player.
    private float _speed;

    //stores our Player's body.
    private Rigidbody _body;

    [SerializeField]
    //stores a reference to the Camera.
    private Camera _camera;

    [SerializeField]
    //stores a reference to our Player's weapon
    private GameObject _meleeWeapon;

    private Animator _animator;

    void Awake()
    {
        //get our rigidbody.
        _body = GetComponent<Rigidbody>();

        _animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //strafe
        float xAxis = Input.GetAxisRaw("Player1Horizontal");
        //jump
        float yAxis = Input.GetAxisRaw("Player1Vertical");
        //forward/backward
        float zAxis = Input.GetAxisRaw("Player1Depth");


        UnityEngine.Debug.Log((xAxis + ", " + yAxis + ", " + zAxis + ", "));

        //sets the animation xAxis parameter.
        _animator.SetFloat("xAxis", xAxis); 
        //sets the animation yAxis parameter.
        _animator.SetFloat("yAxis", yAxis);
        //sets the animation zAxis parameter.
        _animator.SetFloat("zAxis", zAxis);
    }
}
