// License: https://en.wikipedia.org/wiki/MIT_License
// The code in this script is written by Arnab Raha
// Code may be redistributed in source form, provided all the comments at the top here are kept intact

using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Filters_Control : MonoBehaviour
{
	public GameObject Filter_Strength;
	public GameObject Con_Bri;

	public Text str;				// effect strength shower Text
	public Text contStr;			// contrast value
	public Text brtStr;				// brightness value

	public Slider strength;			// effect strength controller
	public Slider contrast;			// contrast controller
	public Slider bright;			// brightness controller
	
	public Effects fx;
	public AudioClip clip;
	
	private AudioSource _audioSource;

	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		//Original();
	}
	
	public void Original () {
		fx.DisableFx ();
	}

	public void Effect () {
		fx.EnableFx ();
	}

	public void ValueChange () {
		fx.styleStrength = strength.value;
		str.text = "Filter Strength : " + (strength.value * 100).ToString ("0");
	}

	public void Contrast () {
		fx.contrast = contrast.value;
		contStr.text = "Contrast : " + contrast.value.ToString ("F2");
	}

	public void Bright () {
		fx.brightness = bright.value;
		brtStr.text = "Brightness : " + bright.value.ToString ("F2");
	}

	public void ToggleOn (bool Con_Bri_On) {
		if (Con_Bri_On)
		{
			Con_Bri.SetActive(true);
		}
		else
		{
			Con_Bri.SetActive(false);
		}
		fx.isCon_Bri_on = Con_Bri_On;
		platform_sound();
	}

	public void SetEffect (int ind) {
		switch (ind) {
			case 1:
				fx.SetFx (Fx.greyscale);
				break;
			case 2:
				fx.SetFx (Fx.sepia);
				break;
			case 3:
				fx.SetFx (Fx.negative);
				break;
		}
		platform_sound();
		Filter_Strength.SetActive(true);
	}
	
	public void platform_sound()
	{
		_audioSource.PlayOneShot(clip);
	} 
}
