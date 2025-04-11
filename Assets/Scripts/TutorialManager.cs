using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanettoneGames.GenEvents;

public class TutorialManager : MonoBehaviour, IGameEventListener<int>
{
    [Header("These are related to completing the tutorial")]
    public IntEvent tutorialEvents;
    public int eventCount;
    public string[] tutorialMessages;
    private int currentMessage = 0;
    [Header("This is for the Notifications, so that this knows when a message is hidden and can respond")]
    public static int ToastHideID = 1000;

    private bool[] eventCompletion;


    void Awake()
    {
        ToastNotification.Show(tutorialMessages[0]);
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
        if(item == ToastHideID) {
            
        }else {
            
            if(currentMessage < tutorialMessages.Length - 1 && item + 1== currentMessage) {
                ToastNotification.Hide();
                currentMessage++;
                ToastNotification.Show(tutorialMessages[currentMessage], 1000);
            }
               

            eventCompletion[item] = true;
            if(isTutorialComplete()) {
                ToastNotification.Show("Tutorial Complete!");
            }
        }
        
    }
}
