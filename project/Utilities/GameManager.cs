using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 ___ ___   ____  _        ___ __ __   ___   _        ___  ____   ______ 
|   |   | /    || |      /  _]  |  | /   \ | |      /  _]|    \ |      |
| _   _ ||  o  || |     /  [_|  |  ||     || |     /  [_ |  _  ||      |
|  \_/  ||     || |___ |    _]  |  ||  O  || |___ |    _]|  |  ||_|  |_|
|   |   ||  _  ||     ||   [_|  :  ||     ||     ||   [_ |  |  |  |  |  
|   |   ||  |  ||     ||     |\   / |     ||     ||     ||  |  |  |  |  
|___|___||__|__||_____||_____| \_/   \___/ |_____||_____||__|__|  |__|  
                                                                        
                  _____ __ __  ____   ____  ____     ___ 
                 / ___/|  |  ||    \ |    ||    \   /  _]
                (   \_ |  |  ||  D  ) |  | |  _  | /  [_ 
                 \__  ||  _  ||    /  |  | |  |  ||    _]
                 /  \ ||  |  ||    \  |  | |  |  ||   [_ 
                 \    ||  |  ||  .  \ |  | |  |  ||     |
                  \___||__|__||__|\_||____||__|__||_____|
                                         
                                         .""--..__
                     _                     []       ``-.._
                  .'` `'.                  ||__           `-._
                 /    ,-.\                 ||_ ```---..__     `-.
                /    /:::\\               /|//}          ``--._  `.
                |    |:::||              |////}                `-. \
                |    |:::||             //'///                    `.\
                |    |:::||            //  ||'                      `|
                /    |:::|/        _,-//\  ||
               /`    |:::|`-,__,-'`  |/  \ ||
             /`  |   |'' ||           \   |||
           /`    \   |   ||            |  /||
         |`       |  |   |)            \ | ||
        |          \ |   /      ,.__    \| ||
        /           `         /`    `\   | ||
       |                     /        \  / ||
       |                     |        | /  ||
       /         /           |        `(   ||
      /          .           /          )  ||
     |            \          |     ________||
    /             |          /     `-------.|
   |\            /          |              ||
   \/`-._       |           /              ||
    //   `.    /`           |              ||
   //`.    `. |             \              ||
  ///\ `-._  )/             |              ||
 //// )   .(/               |              ||
 ||||   ,'` )               /              //
 ||||  /                    /             || 
 `\\` /`                    |             // 
     |`                     \            ||  
    /                        |           //  
  /`                          \         //   
/`                            |        ||    
`-.___,-.      .-.        ___,'        (/    
         `---'`   `'----'`
Future Update Features:
- Custom keybinds
- Custom pixel fonts
- More enemy types - Orc, Red tree
- Enemy hit indication
- New layer for 2D verticality - Objects in front of player
- Highlighting menu items
*/

// Game manager behaviour
public class GameManager : MonoBehaviour
{
    [Header("Game Variables")]
    public TransitionValues transitionValues;
    public ObjectValues treasureChest;
    public ObjectValues keyDoor;
    public ObjectValues[] heartPickups;
    public InventoryValues inventory; 
    public HealthValues playerHealth;

    [Header("Scene Transition Variables")]
    public string overworld;
    public string mainMenu;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    // Restart level
    public void RestartLevel()
    {
        ResetVariables();
        StartCoroutine(SceneTransitionCo(overworld));
    }

    // Quit to main menu
    public void QuitToMainMenu()
    {
        ResetVariables();
        StartCoroutine(SceneTransitionCo(mainMenu));
    }

    // Reset game variables
    public void ResetVariables()
    {
        transitionValues.runtimePlayerPosition = transitionValues.defaultPlayerPosition;
        transitionValues.runtimeCameraMinPosition = transitionValues.defaultCameraMinPosition;
        transitionValues.runtimeCameraMaxPosition = transitionValues.defaultCameraMaxPosition;
        playerHealth.runtimeValue = playerHealth.defaultValue;
        inventory.runtimeKeyCount = inventory.defaultKeyCount;
        treasureChest.runtimeChestIsOpen = treasureChest.defaultChestIsOpen;
        keyDoor.runtimeDoorIsOpen = keyDoor.defaultDoorIsOpen;
        foreach (ObjectValues heartPickup in heartPickups)
        {
            heartPickup.runtimeHeartIsPickedUp = heartPickup.defaultHeartIsPickedUp;

        }
        Time.timeScale = 1f;
    }

    // Scene transition coroutine from pause menu
    private IEnumerator SceneTransitionCo(string sceneToLoad)
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    // Quit application
    public void Quit()
    {
        Application.Quit();
    }
}