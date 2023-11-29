using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoTracker : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI text;

    private void Start()
    {
        UpdateAmmoTracker();
    }

    private void Update()
    {
        UpdateAmmoTracker();
    }

    public void UpdateAmmoTracker()
    {
        text.text = "Bullets:" + $"{player.currentClip} / {player.maxClipSize} | {player.currentAmmo} / {player.maxAmmoSize}";
    }
}
