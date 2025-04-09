using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMunicao : MonoBehaviour
{
    public GameObject pentePistola; 
    public GameObject penteFuzil;
    private int idArma; //1 - Pistola / 2 - Fuzil
    // Start is called before the first frame update
    void Start()
    {
        //Sortear a munição a ser gerada
        idArma = new System.Random().Next(1,3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
