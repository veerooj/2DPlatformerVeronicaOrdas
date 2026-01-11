using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HoldToLoadLevel : MonoBehaviour
{
    public float holdDuration = 1f;

    public Image fillCircle;


    private float holdTimer = 0f;

    private bool isHolding = false;

    public static event Action OnHoldComplete;
    
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isHolding = true;
        }

        if (isHolding && Input.GetKey(KeyCode.E))
        {
            holdTimer += Time.deltaTime;
            fillCircle.fillAmount = holdTimer / holdDuration;
            
            if (holdTimer >= holdDuration)
            {
                Debug.Log("Level passed");
                OnHoldComplete.Invoke();
                ResetHold();
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (holdTimer < holdDuration)
            {
                ResetHold(); 
            }
        }
       
    }
    

    private void ResetHold()
    {
        isHolding = false;
        holdTimer = 0;
        fillCircle.fillAmount = 0;
    }
}
