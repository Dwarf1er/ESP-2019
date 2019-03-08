﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworking : NetworkBehaviour
{
    //References
    [SerializeField]
    List<Behaviour> offlineComponents = new List<Behaviour>();
    [SerializeField]
    string onlineLayerName = "Ennemy";
    Camera lobbyCamera;

    void Start()
    {
        //Switches every online components to offline components if not related to the local player (added to the list in UI)
        //Adds the layer Ennemies to every online entities
        if (!isLocalPlayer)
        {
            DisableOnlineComponents();
            AssignOnlineLayers();
        }

        //Deactivate the LobbyCamera when player logs in
        else
        {
            lobbyCamera = Camera.main;

            //Prevents error if there is no lobbyCamera in the scene
            if (lobbyCamera != null)
                lobbyCamera.enabled = true;
        }

        //Gives the player a unique identifier
        EnlistPlayer();
    }

    void DisableOnlineComponents()
    {
        foreach (Behaviour putOffline in offlineComponents)
            putOffline.enabled = false;
    }

    void AssignOnlineLayers()
    {
        //Assign the layer Ennemies to online entitites (ennemies)
        gameObject.layer = LayerMask.NameToLayer(onlineLayerName);
    }

    void EnlistPlayer()
    {
        //Gives the player a unique identifier using his netID
        string playerID = "Player" + GetComponent<NetworkIdentity>().netId;
        transform.name = playerID;
    }

    //Reactivate the LobbyCamera
    void OnDisable()
    {
        if (lobbyCamera != null)
            lobbyCamera.enabled = true;
    }
}
