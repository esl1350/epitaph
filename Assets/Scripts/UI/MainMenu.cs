using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<AbilityWrapper> abilityChoices;

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(1);
        //ReinstateAbilityChoices();
    }

    public void Restart() {
        //ReinstateAbilityChoices();
        //Destroy(GameObject.Find("UI"));

        SceneManager.LoadScene(1);
    }

    public void Menu() {
        //ReinstateAbilityChoices();

        //Destroy(GameObject.Find("UI"));

        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Debug.Log("quit");
        Application.Quit();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            ReinstateAbilityChoices();
        }
    }
    public void ReinstateAbilityChoices()
    {
        Debug.Log("ReinstateAbilityChoices is happening");
        GameObject abilitySelection = GameObject.Find("AbilitySelection");
        if (abilitySelection != null)
        {
            AbilitySelection abilitySelectionComponent = abilitySelection.GetComponent<AbilitySelection>();
            if (abilitySelectionComponent != null)
            {
                abilitySelectionComponent.setAbilityChoices(abilityChoices);
            }
            else
            {
                Debug.LogError("AbilitySelection component not found.");
            }
        }
        else
        {
            Debug.LogError("GameObject 'AbilitySelection' not found.");
        }
    }
}