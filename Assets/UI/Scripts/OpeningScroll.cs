using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningScroll : MonoBehaviour {
    private Image imageObject;
    public List<Sprite> scrollingSprites;
    private int index = 1;
    private bool isTransitioning = false;

	// Use this for initialization
	void Start () {

        imageObject = transform.Find("Image").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isTransitioning)
        {
            if (index < scrollingSprites.Count)
            {
                StartCoroutine("NextImage");
            }
            else
            {
                StartCoroutine("NextImage");
                
            }
        }
	}

    private IEnumerator NextImage()
    {
        isTransitioning = true;
        float i = 100;
        while (i > 0)
        {
            i--;
            print(i);
            imageObject.color = new Color(i/100, i / 100, i / 100, 1);
            yield return new WaitForSeconds(0.0003f);
        }
        if (index < scrollingSprites.Count)
        {
            imageObject.sprite = scrollingSprites[index];
            while (i < 100)
            {
                i++;
                imageObject.color = new Color(i / 100, i / 100, i / 100, 1);
                yield return new WaitForSeconds(0.0003f);
            }
        }
        else
        {
            SceneManager.LoadScene("Map");
            imageObject.color = new Color(0, 0, 0, 1);
            
            while (i < 100)
            {
                yield return new WaitForSeconds(0.0003f);
                i++;
            }
        }
        index++;
        isTransitioning = false;

    }

}
