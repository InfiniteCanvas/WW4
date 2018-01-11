using System.Collections.Generic;
using UnityEngine;

//Not ready yet!!!

public class RandomBook : MonoBehaviour
{
    [SerializeField] private Material[] books;

    private void Start()
    {
        books = Resources.LoadAll<Material>("Books");
        GetComponent<Renderer>().material = books[Random.Range(0, books.Length)];
    }
}