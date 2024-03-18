using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creates a new rigidbody if it doesnt exist on the object.
//rigidbody cannot be delted off an object with this script (it errors).
[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerScript : MonoBehaviour

//Attributes are written like this:
//[Attributes]

//sets a range with a slider in the inspector.
//[Range(min, max)]

//hides variable in inspector
//[HideInInspector]
//public float publicValue;
{
    //shows private variable in inspector
    //[SerializeField]

    //Attributes can be stacked in the same square brackets.
    //SerializesField and sets a minimum value that can be set.
    [SerializeField, Min(0)]
    //adds a tool tip to the variable in the inspector.
    [Tooltip("Player Acceleration")]
    //speed of movement.
    private float _acceleration = 50;

    //the maximum speed that this can have.
    private float _maxSpeed = 100;

    //compound attributes.
    [SerializeField, Range(0, 100), Tooltip("Jump Speed")]
    //speed of jumping.
    private float _jumpForce = 20;

    //stores the direction of movement on the x axis.
    private Vector3 _movement;

    //stores if the target is currently grounded or not.
    private bool _isTargetGrounded = false;


    //using properties allows you to controll how this works.
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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //get axis raw is getting -1 or 1 with no smoothing.
        _movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        //get the raw jump input.
        float jumpMovement = 0;
        if (Input.GetKeyDown(KeyCode.Space) && _isTargetGrounded)
        {
            jumpMovement = 1;
            _isTargetGrounded = false;
        }

        //magnifies jump and movement values.
        float jumpMagnifier = 50;

        //adds force required for jumping.
        //ForceMode.Impulse applies the force all at once.
        _rigidbody.AddForce(Vector3.up * jumpMovement * _jumpForce * jumpMagnifier * Time.deltaTime, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        float movementMagnifier = 70;
        if (_isTargetGrounded)
            //adds movement force.
            _rigidbody.AddForce(_movement * _acceleration * movementMagnifier * Time.fixedDeltaTime, ForceMode.VelocityChange);

        //clamp velocity to maxSpeed.
        if (_rigidbody.velocity.magnitude > _maxSpeed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        _isTargetGrounded = true;
    }

}

