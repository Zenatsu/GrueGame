using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    public bool isOver = false;

	void OnGUI()
    {
        if(isOver == true)
        {
            Time.timeScale = 0;
            if (GUI.Button(new Rect(100, 100, 250, 100), "Game Over"))
                Application.LoadLevel(Application.loadedLevel);
        }
    }
    
    public void EndGame()
    {
        isOver = true;
        //Debug.Log("Game Over Man!");
    }
}
