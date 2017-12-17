using System.Collections.Generic;
using UnityEngine;

//Not ready yet!!!

public class RandomBook : MonoBehaviour
{
    [SerializeField] private List<Texture2D> books;
    private Texture2D[] atlasTextures;
    private Rect[] rects;

    private void Start()
    {
        books = new List<Texture2D>(Resources.LoadAll<Texture2D>("Books"));

        atlasTextures = books.ToArray();

        Texture2D atlas = new Texture2D(512, 512);
        rects = atlas.PackTextures(atlasTextures, 2, 512);

        int x = Random.Range(0, 1);
        int y = Random.Range(0, 1);
        Material mat = new Material(GetComponent<Renderer>().sharedMaterial);
        mat.SetTexture("_MainTex", atlas);        
        mat.SetTextureOffset("_MainTex", new Vector2(x, y).normalized);
        GetComponent<Renderer>().sharedMaterial = mat;
    }
}