using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    [SerializeField, Tooltip("The starting health of the boss.")]
    //the maximum amount of health this boss can have.
    private float _maxHealth;

    //the current health the boss has.
    private float _currentHealth;

    //returns the decimal percentage of Health that this boss has left.
    public float GetHealth
    {
        get => _currentHealth / _maxHealth;
    }

    //Event when Boss is damaged.
    public UnityEvent OnBossDamage;

    private void Start()
    {
        //set the current Health of the boss.
        _currentHealth = _maxHealth;

        //insures the function UpdateHealthBar is called when OnBossDamage occurs.
        OnBossDamage.AddListener(GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BossBattle>().UpdateHealthBar);
    }


    //does damage to the boss.
    public void TakeDamage(float amount)
    {
        //reduces the Health of the boss.
        _currentHealth -= amount;

        //invokes the OnBossDamage event.
        OnBossDamage.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
    }
}
