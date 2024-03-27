using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TagTimerScript : MonoBehaviour
{
    [SerializeField]
    //the duration of the countdown.
    private float _startSeconds;
    //the amount of seconds left in the countdown.
    private float _currentSeconds;
    //true if the timer should count down.
    private bool _timerActive;
    //when the time runs out, stores true.
    private bool _finished = false;
    //true if this is player1.
    [SerializeField]
    private bool _player1 = false;
    [SerializeField]
    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        //set the current time.
        _currentSeconds = _startSeconds;

        //give default number to the timer field.
        _text.text = ((int)_startSeconds).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //escape if we arent it.
        if (!_timerActive)
        {
            //resets the timer.
            _currentSeconds = _startSeconds;

            //changes the text to be the full duration.
            _text.text = ((int)_startSeconds).ToString();

            return;
        }

        //subtract time from the current time left.
        if (_currentSeconds > 0.1)
            _currentSeconds -= Time.deltaTime;
        //else mark this script as finished.
        else
            _finished = true;

        //changes the text of the game object this is attached to, to be the current seconds left.
        _text.text = ((int)_currentSeconds).ToString();
    }

    //set if this timer should be active.
    public void IsActive(bool value)
    {
        //sets the boolean value.
        _timerActive = value;
    }

    //return if we are finished or not.
    public bool IsFinished()
    {
        return _finished;
    }

    //return 1 or 2 depending on what player this script is apart of.
    public int IsPlayer()
    {
        if(_player1)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
    //resets the timer.
    public void ResetTimer()
    {
        _currentSeconds = _startSeconds;
    }
}
