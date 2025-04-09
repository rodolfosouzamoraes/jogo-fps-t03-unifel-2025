using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    public static PlayerMng Instance;
    public static MovimentarPlayer MovimentarPlayer;
    public static DisparoPlayer DisparoPlayer;
    public static VisaoCamera VisaoCamera;

    void Awake()
    {
        if(Instance == null){
            MovimentarPlayer = GetComponent<MovimentarPlayer>();
            DisparoPlayer = GetComponent<DisparoPlayer>();
            VisaoCamera = GetComponentInChildren<VisaoCamera>();
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public GameObject lanterna;
    void Start()
    {
        lanterna.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Ativar ou desativar a lanterna
        if(Input.GetKeyDown(KeyCode.F)){
            lanterna.SetActive(!lanterna.activeSelf);
        }
    }
}
