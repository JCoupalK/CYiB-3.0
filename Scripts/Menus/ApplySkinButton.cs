using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySkinButton : MonoBehaviour
{
    public SkinsMenuOptions skinsMenuOptions; // Reference to SkinsMenuOptions script

    private void SelectSkin(string skinName)
    {
        ZPlayerPrefs.SetString("SelectedSkin", skinName);
        ZPlayerPrefs.Save();
    }

    public void ApplySkin()
    {
        if (skinsMenuOptions == null || skinsMenuOptions.skins.Count == 0)
        {
            Debug.LogError("SkinsMenuOptions is not set or has no skins.");
            return;
        }

        // Get the current skin's sprite and then its name
        SkinsMenuOptions.Skin currentSkin = skinsMenuOptions.skins[skinsMenuOptions.currentIndex];
        string skinName = currentSkin.skinSprite.name;

        // Save the selected skin
        SelectSkin(skinName);
        Debug.Log("Skin " + skinName + " was applied.");
    }
}