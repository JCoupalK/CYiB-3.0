using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    public Material characterMaterial; // Assign this in the inspector
    public Texture2D[] skinTextures; // Assign all possible skins in the inspector

    void ApplySkin(string skinName)
    {
        foreach (Texture2D texture in skinTextures)
        {
            if (texture.name == skinName)
            {
                characterMaterial.mainTexture = texture;
                break;
            }
        }
    }
    void Start()
    {
        string selectedSkinName = ZPlayerPrefs.GetString("SelectedSkin", "DefaultSkin");
        ApplySkin(selectedSkinName);
    }
}