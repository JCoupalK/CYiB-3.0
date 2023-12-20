using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsMenuOptions : MonoBehaviour
{
    [System.Serializable]
    public class Skin
    {
        public Sprite skinSprite; // The actual skin sprite
        public GameObject lockedOverlay; // The dark grey fade GameObject
        public Text unlockScoreText; // Text to display the score needed to unlock
    }

    public List<Skin> skins; // List of Skin objects
    public Image skinPreviewImage; // Reference to the Image component of SkinPreview
    public Text skinNameText; // Text element for displaying the skin's name
    public Button applyButton; // Reference to the Apply button
    public int currentIndex = 0; // Current index in the skins list

    private void Start()
    {
        UpdateSkinDisplay();
    }

    public void Next()
    {
        currentIndex = (currentIndex + 1) % skins.Count;
        UpdateSkinDisplay();
    }

    public void Previous()
    {
        currentIndex = (currentIndex - 1 + skins.Count) % skins.Count;
        UpdateSkinDisplay();
    }

    private void UpdateApplyButtonState()
    {
        int highScore = ZPlayerPrefs.GetInt("HighScore", 0);
        // Start counting from the second skin (index 1)
        int unlockScore = currentIndex * 1000;

        // The first skin (index 0) is always unlocked
        bool isSkinUnlocked = currentIndex == 0 || highScore >= unlockScore;
        applyButton.interactable = isSkinUnlocked;
    }


    private void UpdateSkinDisplay()
    {
        if (skins.Count > 0 && currentIndex >= 0 && currentIndex < skins.Count)
        {
            int highScore = ZPlayerPrefs.GetInt("HighScore", 0);
            // Start counting from the second skin (index 1)
            int unlockScore = currentIndex * 1000;

            Skin currentSkin = skins[currentIndex];
            skinPreviewImage.sprite = currentSkin.skinSprite; // Update the skin image
            skinNameText.text = currentSkin.skinSprite.name; // Update the skin name text

            // The first skin (index 0) is always unlocked
            bool isSkinUnlocked = currentIndex == 0 || highScore >= unlockScore;

            currentSkin.lockedOverlay.SetActive(!isSkinUnlocked);
            currentSkin.unlockScoreText.gameObject.SetActive(!isSkinUnlocked);

            if (!isSkinUnlocked)
            {
                currentSkin.unlockScoreText.text = "Unlock at " + unlockScore.ToString() + " points";
            }

            // Also update the apply button state
            UpdateApplyButtonState();
        }
    }

}