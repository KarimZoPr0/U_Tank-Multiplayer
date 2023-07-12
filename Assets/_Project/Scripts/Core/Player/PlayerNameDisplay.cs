using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] private TankPlayer player;
    [SerializeField] private TMP_Text playerNameText;
    
    private void Start()
    {
        HandlePLayerNameChanged(string.Empty, player.playerName.Value);
        player.playerName.OnValueChanged += HandlePLayerNameChanged;
    }

    private void HandlePLayerNameChanged(FixedString32Bytes oldName, FixedString32Bytes newName)
    {
        playerNameText.text = newName.ToString();
    }

    private void OnDestroy()
    {
        player.playerName.OnValueChanged -= HandlePLayerNameChanged;
    }
}
