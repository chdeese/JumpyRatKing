using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creates a new rigidbody if it doesnt exist on the object.
//rigidbody cannot be delted off an object with this script (it errors).
[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerScript : MonoBehaviour
{

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
    [SerializeField, Min(0)]
    //adds a tool tip to the variable in the inspector.
    [Tooltip("Player Acceleration")]
    //speed of movement.
    private float _acceleration = 50;

    //compound attributes.
    [SerializeField, Range(0, 10), Tooltip("Jump Speed")]
    //speed of jumping.
    private float _jumpHeight = 2;

    [Space]
    [SerializeField]
    private Vector3 _groundCheck;
    [SerializeField]
    private Vector3 _groundCheckExtense;


    //the maximum speed that this can have.
    private float _maxSpeed = 100;

    //stores if the target is currently grounded or not.
    private bool _isTargetGrounded = false;

    //stores if the player is jumping or not.
    private bool _jumpInput;

    //stores the direction of movement on the x axis.
    private Vector3 _movement;



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
        _jumpInput = Input.GetAxisRaw("Jump") != 0;
    }

    private void FixedUpdate()
    {
        //finds if the player is grounded or not.
        _isTargetGrounded = Physics.OverlapBox(transform.position + _groundCheck, _groundCheckExtense, transform.rotation).Length > 1;

        //magnifies the speed of player movement.
        float movementMagnifier = 70;

        //adds movement force.
        _rigidbody.AddForce(_movement * _acceleration * movementMagnifier * Time.fixedDeltaTime, ForceMode.VelocityChange);

        //Clamp velocity to _maxSpeed.
        Vector3 velocity = _rigidbody.velocity;
        float newXspeed = Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed, _maxSpeed);
        velocity.x = newXspeed;
        _rigidbody.velocity = velocity;

        //clamp velocity to maxSpeed.
        if (_jumpInput && _isTargetGrounded)
        {
            float force = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isTargetGrounded = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //Draw ground check
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + _groundCheck, _groundCheckExtense);
    }
#endif
}

