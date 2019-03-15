﻿//https://www.youtube.com/watch?v=9w2kwGDZ6wM
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class MultiplayerMenu : NetworkManager
{
    const string BasicIP = "localhost";

    Button BtnHost { get; set; }
    Button BtnJoin { get; set; }
    InputField IPAdress { get; set; }
    GameObject PnlError { get; set; }
    Button BtnOK { get; set; }
    private void Start()
    {
        InitializeReferences();
        PnlError.SetActive(false);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player;
        Transform startPos = GetStartPosition();

        if(startPos != null)
        {
            if(playerPrefab.name == "Player1")
            {
                SkinnedMeshRenderer skin = playerPrefab.transform.Find("Soldier_mesh").GetComponent<SkinnedMeshRenderer>(); //Referencing the skinned mesh renderer to change the material used to have a different type of soldier
            }

            if(playerPrefab.name == "Player2")
            {

            }
        }
    }

    private void InitializeReferences()
    {
        IPAdress = GameObject.Find("InputIP").GetComponent<InputField>();
        PnlError = GameObject.Find("PnlError");
        BtnOK = PnlError.transform.Find("BtnOK").GetComponent<Button>();
        BtnOK.onClick.AddListener(() => HidePanel());

    }

    private void HidePanel()
    {
        PnlError.SetActive(false);
    }

    public void StartUpHost()
    {
        AssociatePort();
        NetworkManager.singleton.StartHost();
    }

    public void StartUpClient()
    {
        AssociateIPAddress();

        if (string.IsNullOrEmpty(NetworkManager.singleton.networkAddress))
            PnlError.SetActive(true);
        else
        {
            AssociatePort();
            NetworkManager.singleton.StartClient();
        }
        
    }

    private void AssociateIPAddress()
    {
        NetworkManager.singleton.networkAddress = IPAdress.text;
    }

    private void AssociatePort()
    {
        NetworkManager.singleton.networkPort = 5005;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            SetMenuButtons();
        }
        else
        {
            SetOtherButtons();
        }
    }


    private void SetOtherButtons()
    {
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(() => StopGame());
    }

    void SetMenuButtons()
    {
        BtnHost = GameObject.Find("BtnHébergerLAN").GetComponent<Button>();
        BtnHost.onClick.AddListener(() => StartUpHost());
        BtnJoin = GameObject.Find("BtnJoindrePartie").GetComponent<Button>();
        BtnJoin.onClick.AddListener(() => StartUpClient());
    }

    private void StopGame()
    {
        NetworkManager.singleton.StopHost();
    }

    
}

