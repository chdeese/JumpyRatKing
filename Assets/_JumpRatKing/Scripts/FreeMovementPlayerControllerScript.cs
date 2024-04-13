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

    [Space]

    [SerializeField]
    [Tooltip("Delay before Player can attack again.")]
    //the amount of time before CanAttack() is invoked after attacking.
    private float _attackDelay;

    [SerializeField]
    //stores a reference to our Player's weapon.
    private GameObject _meleeWeapon;



    //stores a reference to the animator that animates this.
    private Animator _animator;

    //stores a reference to our rigidbody.
    private Rigidbody _body;

    //stores true if this Player is already jumping.
    private bool _jumping;

    //stores true if this Player can attack again.
    private bool _canAttack;

    //sets animator boolean to false.
    private void StopAttacking()
    {
        _animator.SetBool("isAttacking", false);
    }
    //sets _canAttack to be true.
    private void CanAttack()
    {
        _canAttack = true;
    }

    void Awake()
    {
        //get our body.
        _body = GetComponent<Rigidbody>();

        //get our animator.
        _animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        CanAttack();
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

        //if the E key is pressed: Attack.
        if (Input.GetKeyDown(KeyCode.E) && _canAttack)
        {
            //begin attacking.
            _animator.SetBool("isAttacking", true);
            //disable attacking.
            _canAttack = false;

            //re-enables Player attacking after a delay.
            Invoke("CanAttack", _attackDelay);
        }
        else
            //stop attacking.
            StopAttacking();

        //if we are already jumping.
        if(_jumping)
        {
            //prevent jump input from reaching animation controller.
            yAxis = 0f;
        }

        //if we are trying to jump.
        if(yAxis > 0.1f && !_jumping)
        { 
            //set jumping to true.
            _jumping = true;
        }

        //if we are not currently jumping or falling.
        if(_body.velocity.y < 0.1f && _body.velocity.y > -0.1f)
            _jumping = false;

        //sets the animation xAxis parameter.
        _animator.SetFloat("xAxis", xAxis); 
        //sets the animation yAxis parameter.
        _animator.SetFloat("yAxis", yAxis);
        //sets the animation zAxis parameter.
        _animator.SetFloat("zAxis", zAxis);



    }
}
