using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class EndTutorialDoor : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRange = 5f;

    [Header("Scene Settings")]
    public string sceneToLoad;

    private Outline outline;
    private Camera mainCamera;

    void Start()
    {
        outline = GetComponent<Outline>();
        mainCamera = Camera.main;

        if (outline != null)
            outline.enabled = false;
    }

    public void OnGrab(InputAction.CallbackContext context)
	{
        
		bool lookingAt = IsLookingAtThis();
        if(lookingAt) {
            Debug.Log("Interacting with the door");
        }
        if (lookingAt && TutorialManager.currentEvent == TutorialManager.TutorialEventIDs.Finished)
        {
            LoadScene();
        }
	}

    void Update()
    {
        // Enable Outline if the flag is active
        if (TutorialManager.currentEvent == TutorialManager.TutorialEventIDs.Finished && outline != null)
        {
            

            outline.enabled = true;

            
        }
        else if (outline != null)
        {
            outline.enabled = false;
        }
    }

    bool IsLookingAtThis()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            return hit.transform == transform;
        }

        return false;
    }

    void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene name not set on " + gameObject.name);
        }
    }
}
