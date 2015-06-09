using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    float speed=0.33f;
    public bool grabTorch = false;
    int caseSwitch = 1;

	void Update () 
    {
        
        if(GetComponent<NetworkView>().isMine)
        {
            InputMovement();
        }
                  
	}

    private void InputMovement()
    {
        //All the movement
        if (Input.GetKeyDown("w"))
            this.transform.Translate(0, speed, 0);

        if (Input.GetKeyDown("a"))
            this.transform.Translate(-speed, 0, 0);

        if (Input.GetKeyDown("s"))
            this.transform.Translate(0, -speed, 0);

        if (Input.GetKeyDown("d"))
            this.transform.Translate(speed, 0, 0);
        //togle true/false
        if (Input.GetKeyDown("space"))
        {
            switch (caseSwitch)
            {
                case 1:
                    grabTorch = true;
                    caseSwitch++;
                    break;
                case 2:
                    grabTorch = false;
                    caseSwitch--;
                    break;
                default:
                    grabTorch = true;
                    break;
            }
        }
    }
}
