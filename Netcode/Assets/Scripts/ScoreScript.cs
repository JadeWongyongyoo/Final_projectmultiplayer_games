using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using System;

public class ScoreScript : NetworkBehaviour
{
    TMP_Text p1Text, p2Text;
    MainPlayerScript mainPlayer;
    public NetworkVariable<int> scoreP1 = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> scoreP2 = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private void Start()
    {
        p1Text = GameObject.FindGameObjectWithTag("P1ScoreText").GetComponent<TMP_Text>();
        p2Text = GameObject.FindGameObjectWithTag("P2ScoreText").GetComponent<TMP_Text>();
        mainPlayer = GetComponent<MainPlayerScript>();

    }

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (IsOwnedByServer)
        {
            p1Text.text = $"{mainPlayer.playerNameA.Value} : {scoreP1.Value}";
        }
        else
        {
            p2Text.text = $"{mainPlayer.playerNameB.Value} : {scoreP2.Value}";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (!IsLocalPlayer) return;
        if (collision.gameObject.tag == "barrel")
        {
            if (IsOwnedByServer) { scoreP1.Value++; }
            else { scoreP2.Value++; }
        }

    }
}
