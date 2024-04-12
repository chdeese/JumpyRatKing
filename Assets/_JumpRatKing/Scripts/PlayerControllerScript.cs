using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//creates a new rigidbody if it doesnt exist on the object.
//rigidbody cannot be delted off an object with this script (it errors).
[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField]
    private bool _isPlayerOne;

    //SerializesField and sets a minimum value that can be set.
    [SerializeField, Range(0, 10)]
    //adds a tool tip to the variable in the inspector.
    [Tooltip("Player Acceleration")]
    //speed of movement.
    private float _acceleration = 50;

    [SerializeField, Range(0, 10), Tooltip("Jump Speed")]
    //speed of jumping.
    private float _jumpHeight = 2;

    [Space]
    [SerializeField]
    private Vector3 _groundCheck;
    [SerializeField]
    private Vector3 _groundCheckExtense;


    //the maximum speed that this can have.
    private float _maxSpeed = 15;
    //stores if the target is currently grounded or not.
    private bool _isTargetGrounded = false;
    //stores if the player is jumping or not.
    private bool _jumpInput;
    //stores the direction of movement on the x axis.
    private Vector3 _movement;



    //using properties allows you to control how this works.
    public float Speed
    {
        //=> is shorthand for {} but only on 1 line. (lambda syntax)
        get => _acceleration;
        //Mathf.Max returns 0 if value is lower.
        set => _acceleration = Mathf.Max(0, value);
    }


    //variable to hold our rigidbody.
    private Rigidbody _rigidbody;

    //never reference an object in awake because the set-up order is random.
    private void Awake()
    {
        //constructor

        //store component references upon construction.
        _rigidbody = GetComponent<Rigidbody>();
        //this object exists already, however it might not be awake yet.


        //optional:
        //throw error if it is null, this is basically an assert.
        //Example:
        //if (_rigidbody = null)
        //    Debug.LogError("Rigidbody was null");
        //Assert:
        Debug.Assert(_rigidbody, "Rigidbody is null");

        //Assert is meant to never happen, throws an error if it does.
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerOne)
        {
            //get axis raw is getting -1 or 1 with no smoothing. <Looks for left and right inputs and returns the direction.>
            _movement = new Vector3(Input.GetAxisRaw("Player1Horizontal"), 0, 0);
            //get the raw jump input.
            _jumpInput = Input.GetAxisRaw("Player1Jump") != 0;
        }
        else
        {
            //get axis raw is getting -1 or 1 with no smoothing. <Looks for left and right inputs and returns the direction.>
            _movement = new Vector3(Input.GetAxisRaw("Player2Horizontal"), 0, 0);
            //get the raw jump input.
            _jumpInput = Input.GetAxisRaw("Player2Jump") != 0;
        }
    }

    private void FixedUpdate()
    {
        //finds if the player is grounded or not.
        _isTargetGrounded = Physics.OverlapBox(transform.position + _groundCheck, _groundCheckExtense, transform.rotation).Length > 1;

        //magnifies the speed of player movement.
        float movementMagnifier = 10;

        //magnifies the speed of player jump movement.
        float jumpMagnifier = 0.2f;

        //adds movement force.
        _rigidbody.AddForce(_movement * _acceleration * movementMagnifier * Time.fixedDeltaTime, ForceMode.VelocityChange);

        //Clamp velocity to _maxSpeed. (we only do our x axis because we dont want to limit our falling speed. [so we dont fall slow])
        //Clamping is assuring a value is within a certain range.

        //store a copy of our velocity.
        Vector3 velocity = _rigidbody.velocity;
        //store our new speed within a certain range (clamping)
        float newXspeed = Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed, _maxSpeed);
        //assign the speed to our velocity
        velocity.x = newXspeed;
        //give our velocity back to the rigidbody.
        _rigidbody.velocity = velocity;

        //if we are moving right
        if (velocity.x > 0.1f)
        {
            //set our rotation to the right.
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        //else if we are moving left
        else
        {
            //set our rotation to the left.
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }


        //if jump is possible and we are trying to jump
        if (_jumpInput && _isTargetGrounded)
        {
            //find the force of the jump and magnify the jump height.
            float jumpForce = Mathf.Sqrt(_jumpHeight * jumpMagnifier * -2f * Physics.gravity.y);

            //add the force to our rigidbody.
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //selects the color of our gizmo.
        Gizmos.color = Color.green;
        //draws a wire cube that indicates where our ground collision is.
        Gizmos.DrawWireCube(transform.position + _groundCheck, _groundCheckExtense);
    }
#endif






    //NOTES:


    //Attributes are written like this:
    //[Attributes]

    //sets a range with a slider in the inspector.
    //[Range(min, max)]

    //hides variable in inspector
    //[HideInInspector]
    //public float publicValue;

    //shows private variable in inspector
    //[SerializeField]

    //Attributes can be stacked in the same square brackets.
    //SerializesField and sets a minimum value that can be set.
    //[SerializeField, Min(0)]
    //adds a tool tip to the variable in the inspector.
    //[Tooltip("Player Acceleration")]

    //adds a space
    //[Space]


}

