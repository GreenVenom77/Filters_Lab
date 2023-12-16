using UnityEngine;
using UnityEngine.UI;

public class Settings_Manager : MonoBehaviour
{
    [SerializeField] private Slider Volume_Slider;
    [SerializeField] private Dropdown Graphics_Dropdown;
    [SerializeField] private Dropdown FPS_Dropdown;
    
    void Awake()
    {
        Load();
    }

    public void Change_Volume()
    {
        AudioListener.volume = Volume_Slider.value;
        PlayerPrefs.SetFloat("Game_Volume", Volume_Slider.value);
        Save();
    }

    public void Change_Graphics(int Quality_Index)
    {
        Graphics_Dropdown.value = Quality_Index;
        QualitySettings.SetQualityLevel(Quality_Index);
        PlayerPrefs.SetInt("Game_Quality", Quality_Index);
        Save();
    }
    
    public void Change_FPS(int Selection_Index)
    {
        if (Selection_Index == 0)
        {
            Application.targetFrameRate = 30;
        }
        else if (Selection_Index == 1)
        {
            Application.targetFrameRate = 45;
        }
        else if (Selection_Index == 2)
        {
            Application.targetFrameRate = 60;
        }
        else if (Selection_Index == 3)
        {
            Application.targetFrameRate = 90;
        }

        PlayerPrefs.SetInt("Game_FPS", FPS_Dropdown.value);
        Save();
    }

    private int Load_FPS(int Index)
    {
        if (Index == 0)
        {
            return 30;
        }
        else if (Index == 1)
        {
            return 45;
        }
        else if (Index == 2)
        {
            return 60;
        }
        else if (Index == 3)
        {
            return 90;
        }

        return -1;
    }

    private void Load()
    {
        FPS_Dropdown.value = PlayerPrefs.GetInt("Game_FPS");
        Application.targetFrameRate = Load_FPS(PlayerPrefs.GetInt("Game_FPS"));
        
        Graphics_Dropdown.value = PlayerPrefs.GetInt("Game_Quality");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Game_Quality"));
        
        Volume_Slider.value = PlayerPrefs.GetFloat("Game_Volume");
    }

    private void Save()
    {
        PlayerPrefs.Save();
    }
}
