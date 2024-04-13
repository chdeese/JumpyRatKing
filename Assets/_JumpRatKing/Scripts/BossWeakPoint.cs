using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakPoint : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Delay before boss can be damaged again.")]
    private float _damageDelay;

    //stores if the boss can be hit from here again.
    private bool _canBeHit;

    [SerializeField]
    [Tooltip("The boss who's weakpoint this belongs to.")]
    private BossController _boss;


    //invoked to set _canBeHit to true.
    public void CanBeHit()
    {
        _canBeHit = true;
    }

    //upon collision does 25 damage.
    private void OnCollisionEnter(Collision collision)
    {
        //only do damage if the weakpoint can be hit again.
        if (_canBeHit)
        {
            //boss takes damage.
            _boss.TakeDamage(25);

            //set _canBeHit to false because the weakpoint was just hit.
            _canBeHit = false;

            //invoke delay before damage can be done again.
            Invoke("CanBeHit", _damageDelay);
        }
    }
}
