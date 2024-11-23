using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;
    public GameObject menu;
    public Transform head;
    private bool firstFrame = true;
    InputAction RCR_Scene;
    InputAction AED_Scene;
    InputAction options;

    void Start()
    {
        RCR_Scene = InputSystem.actions.FindAction("RCR_Scene");
        AED_Scene = InputSystem.actions.FindAction("AED_Scene");
        options   = InputSystem.actions.FindAction("Options"  );
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFrame) {
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * 2;
            menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
            menu.transform.forward *= -1;
            firstFrame = false;
        }

        if (RCR_Scene.IsPressed())
        {
            SceneManager.LoadScene(1);
        }

        if (AED_Scene.IsPressed())
        {
            SceneManager.LoadScene(2);
        }

        if (options.IsPressed())
        {
            ToggleOptions();
        }
    }

    public void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void ToggleOptions()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
        mainMenu   .SetActive(!mainMenu   .activeSelf);
    }
}
