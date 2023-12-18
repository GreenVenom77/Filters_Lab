using System;
using UnityEngine;
using UnityEngine.UI;

public class Filters_Control : MonoBehaviour
{
	public GameObject Filter_Strength;
	public GameObject Con_Bri;

	public Text Filter_Strenght;	// effect strength value
	public Text Cont_Str;			// contrast value
	public Text Brt_Str;			// brightness value
	public Text Filter;

	public Slider strength;			// effect strength controller
	public Slider contrast;			// contrast controller
	public Slider bright;			// brightness controller
	
	public Effects FX;
	public AudioClip clip;
	
	private AudioSource _audioSource;
	private int Filter_Index;

	void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		if (!FX)
		{
			FX = Camera.main.GetComponent<Effects>();
			SetEffect();
		}
	}

	public void Original () {
		FX.DisableFx ();
	}

	public void Effect () {
		FX.EnableFx ();
	}

	public void ValueChange () {
		FX.styleStrength = strength.value;
		Filter_Strenght.text = "Filter Strength : " + (strength.value * 100).ToString ("0");
	}

	public void Contrast () {
		FX.contrast = contrast.value;
		Cont_Str.text = "Contrast : " + contrast.value.ToString ("F2");
	}

	public void Bright () {
		FX.brightness = bright.value;
		Brt_Str.text = "Brightness : " + bright.value.ToString ("F2");
	}

	public void ToggleOn (bool Con_Bri_On) {
		if (!Con_Bri_On)
		{
			Con_Bri.SetActive(false);
		}
		else
		{
			Con_Bri.SetActive(true);
		}
		FX.isCon_Bri_on = Con_Bri_On;
		platform_sound();
	}

	public void SetEffect (int ind = 0) {
		switch (ind) {
			case 0:
				Original();
				Filter_Strength.SetActive(false);
				Filter.text = "None";
				break;
			case 1:
				Effect();
				FX.SetFx (Fx.greyscale);
				Filter_Strength.SetActive(true);
				Filter.text = "GreyScale";
				break;
			case 2:
				Effect();
				FX.SetFx (Fx.sepia);
				Filter_Strength.SetActive(true);
				Filter.text = "Sepia";
				break;
			case 3:
				Effect();
				FX.SetFx (Fx.negative);
				Filter_Strength.SetActive(false);
				Filter.text = "Negative";
				break;
		}
		platform_sound();
	}

	public void platform_sound()
	{
		_audioSource.PlayOneShot(clip);
	}

	public void Next_Filter()
	{
		if (Filter_Index is >= 0 and < 3)
		{
			Filter_Index++;
			SetEffect(Filter_Index);
		}
	}

	public void Previous_Filter()
	{
		if (Filter_Index is <= 3 and >= 1)
		{
			Filter_Index--;
			SetEffect(Filter_Index);
		}
	}
}
