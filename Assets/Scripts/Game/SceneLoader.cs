using UnityEngine.SceneManagement;

public class SceneLoader
{
    public enum Scenes
    {
        MainMenu,
        Game,
    }
   
    public static void Load(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }
}
