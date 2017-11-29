﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {


    private MountainRot enablecontrols;
    public Image Background;
    public Text DeathUITitle;
    public float fadespeed;

    // Use this for initialization
    void Start () {
        enablecontrols = GameObject.Find("Mountain").GetComponent<MountainRot>();
        Background.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update () {
	
		if (Input.GetKey(KeyCode.R))
		{
			SceneManager.LoadScene (0);
		}

	}
   public  IEnumerator PlayerDeath ()
    {
        Debug.Log("GAME LOSS");
        Background.gameObject.SetActive(true);
        enablecontrols.isLeftkeyEnabled = false;
        enablecontrols.isRightkeyEnabled = false;
        if (Background.gameObject.activeSelf == true)
        {


            Background.color = new Color(1f, 1f, 1f, Mathf.Lerp(Background.color.a / 2, 1.0f, fadespeed));
            DeathUITitle.color = new Color(1f, 1f, 1f, Mathf.Lerp(DeathUITitle.color.a / 2, 1.0f, fadespeed * 0.75f));
        }
        yield break;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
