using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public Transform head;
    private bool firstFrame = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
    }
}
