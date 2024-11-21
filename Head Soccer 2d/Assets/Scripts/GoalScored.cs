using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScored : MonoBehaviour
{

    [SerializeField] private ScoreController scoreController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            //Debug.Log(" Goal Scored!");
            scoreController.IncreaseScore(gameObject.tag, 1);
           
        }
    }
}