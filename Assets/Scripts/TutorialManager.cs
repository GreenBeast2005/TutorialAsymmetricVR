using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanettoneGames.GenEvents;

public class TutorialManager : MonoBehaviour, IGameEventListener<int>
{
    public IntEvent tutorialEvents;
    public int eventCount;

    private bool[] eventCompletion;


    void Awake()
    {
        eventCompletion = new bool[eventCount];
        for(int i = 0; i < eventCount; i++) {
            eventCompletion[i] = false;
        }
    }
    void OnEnable()
    {
        tutorialEvents.RegisterListener(this);
    }
    void OnDisable()
    {
        tutorialEvents.UnregisterListener(this);
    }


    bool isTutorialComplete() {
        for(int i = 0; i < eventCount; i++) {
            if(!eventCompletion[i]) {
                return false;
            }
        }
        return true;
    }

    public void OnEventRaised(int item) {
        eventCompletion[item] = true;
        if(isTutorialComplete()) {
            Debug.Log("tutorial Complete");
        }
    }
}
