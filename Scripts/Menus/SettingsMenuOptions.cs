using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SettingsMenuOptions : MonoBehaviour
{
	public AudioMixer Master;
	public Slider SFX;
	public Slider Music;
	public Dropdown resolutionDropdown;

	private Resolution[] resolutions;

	private void Awake()
	{
		// It's good to use constants or readonly variables for PlayerPrefs keys to avoid typos
		SFX.value = PlayerPrefs.GetFloat("sfxSliderNumber", 1f);
		Music.value = PlayerPrefs.GetFloat("musicSliderNumber", 1f);
	}

	private void Start()
	{
		Master.SetFloat("Volume", PlayerPrefs.GetFloat("sfxSliderNumber"));
		Master.SetFloat("Volume", PlayerPrefs.GetFloat("musicSliderNumber"));

		InitializeResolutions();
	}

	private void Update()
	{
		PlayerPrefs.SetFloat("sfxSliderNumber", SFX.value);
		PlayerPrefs.SetFloat("musicSliderNumber", Music.value);
	}

	private void InitializeResolutions()
	{
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();
		int currentResolutionIndex = 0;

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = $"{resolutions[i].width} x {resolutions[i].height}";
			options.Add(option);

			if (resolutions[i].width == Screen.currentResolution.width &&
				resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolutionIndex = i;
			}
		}

		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}

	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetSfxLvl(float sfxLvl)
	{
		Master.SetFloat("sfxVol", Mathf.Log10(sfxLvl) * 20);
	}

	public void SetMusicLvl(float musicLvl)
	{
		Master.SetFloat("musicVol", Mathf.Log10(musicLvl) * 20);
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
		Debug.Log("FullScreen Toggled");
	}

}
