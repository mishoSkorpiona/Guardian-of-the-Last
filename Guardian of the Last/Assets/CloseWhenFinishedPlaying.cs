using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWhenFinishedPlaying : MonoBehaviour
{
    public AudioSource ass;
    private void Start()
    {
        StartCoroutine(Main());
    }

    public IEnumerator Main()
    {
        while (ass.isPlaying)
        {
            yield return new WaitForFixedUpdate();
        }

        Application.Quit();
    }
}
