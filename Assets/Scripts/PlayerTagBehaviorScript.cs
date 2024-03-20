using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerControllerScript))]
public class PlayerTagBehaviorScript : MonoBehaviour
{
    [SerializeField]
    //stores a reference to the particle system this player uses.
    //can be set in the inspector.
    private ParticleSystem _taggedParticles;

    [SerializeField]
    //stores if we are tagged or not.
    private bool _isTagged;

    //stores if we can be tagged or not.
    private bool _canBeTagged;

    //property for our tagged status
    public bool IsTagged
    {
        get => _isTagged;
        set => _isTagged = value;
    }

    public bool Tag()
    {
        //return false if we cant be tagged.
        if(!_canBeTagged) return false;

        //set tagged to true.
        _isTagged = true;
        //if getting the trail component succeeds, enable the trail.
        //out puts the trail component into the scope.
        if (TryGetComponent(out TrailRenderer trail)) trail.enabled = true;

        //if tagged particles exist
        if(_taggedParticles)
        {
            //play the particles.
            _taggedParticles.Play();
            //invoke clear particles after 0.5 seconds.
            Invoke("ClearParticles", 0.5f);
        }

        //return true if the tag was successful 
        return true;
    }


    public void Start()
    {
        //get the trail component.
        TrailRenderer trail = GetComponent<TrailRenderer>();
        //return if it doesnt exist.
        if (!trail) return;

        //if this player is tagged, enable the player's trail.
        if (IsTagged)
            trail.enabled = true;
        //if this player isnt tagged, disable it.
        else
            trail.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //return if we are not tagged.
        if (!IsTagged) return;

        //get the target we collided with's TagBehaviorScript.
        PlayerTagBehaviorScript tagBehavior = collision.gameObject.GetComponent<PlayerTagBehaviorScript>();

        //if the collided object does not have TagBehavior, return.
        if (tagBehavior == null) return;

        //if tag has failed, return.
        if (!tagBehavior.Tag()) return;

        //set tag for this player to false.
        _isTagged = false;
        //then say we can no longer be tagged
        _canBeTagged = false;

        //"out" returns the variable out of the if condition and into the scope, the variable trail dies when it leaves the if statement.
        //the TryGetComponent still returns true or false.
        if (TryGetComponent(out TrailRenderer trail))
        {
           trail.enabled = false;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        //Invoke waits until a time has passed, then calls a specific function.
        Invoke("SetCanBeTagged", 0.5f);
    }


    //sets _canBeTagged to be true.
    private void SetCanBeTagged()
    {
        _canBeTagged = true;
    }

    //clears the current particles that this can emmit.
    private void ClearParticles()
    {
        _taggedParticles.Clear();
    }
}
