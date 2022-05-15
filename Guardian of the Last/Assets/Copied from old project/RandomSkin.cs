using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkin : MonoBehaviour
{
    public Texture2D[] possibleSkins;
    private void Start()
    {
        Material mat = gameObject.GetComponent<SkinnedMeshRenderer>().material;

        mat.mainTexture = possibleSkins[Random.Range(0, possibleSkins.Length)];
    }
}
