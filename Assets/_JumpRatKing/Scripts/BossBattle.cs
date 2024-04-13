using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossBattle : MonoBehaviour
{
    [SerializeField]
    private GameObject _bossHealthBar;


    [SerializeField]
    private GameObject _playerHealthBar;

    [Space]

    [SerializeField]
    private BossController _bossController;

    //used to scale healthbar.
    public float BossHealthDecimal
    {
        get => _bossController.GetHealth / 100;
    }

    public void UpdateHealthBar()
    {
        //gets a copy of the current boss healthbar scale.
        Vector3 newScale = _bossHealthBar.transform.localScale;

        //changes the scale of the x axis.
        newScale.x = BossHealthDecimal;

        //overrides the old scale with our new scale.
        _bossHealthBar.transform.localScale = newScale;
    }
}
