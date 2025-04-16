using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanettoneGames.GenEvents;
using System;

public class TutorialManager : MonoBehaviour, IGameEventListener<int>
{
    //All the tutorialIDs are handled here, so that I am not having to set a bunch of IDs in different classes
    //Thats what I was doing, but thats pretty dumb.

    //However, these will have to be manually updated and basically these corespond with the pos of the message
    //That prompts you to do the acton
    public enum TutorialEventIDs {
        DirectionInputEvent = 1,
        MouseInputEvent = 2,
        GazeObjectEvent = 5,
        GrabObjectEvent = 7,
        ObjectOnTableEvent = 8,
        Finished = 9
    }
    public static TutorialEventIDs currentEvent;
    //Here we associate each eventID with a message to prompt you to do that action.
    public string[] tutorialMessages;
    private string tutorialMessageWithEvent(TutorialEventIDs id) {
        return id switch{
            TutorialEventIDs.DirectionInputEvent => "Lets get you moving, try using WASD or the Arrow Keys!",
            TutorialEventIDs.MouseInputEvent => "Awesome! Now use the Mouse to look around the room.",
            TutorialEventIDs.GazeObjectEvent => "Now lets put those peepers to some good use! Get close to one of those *Black Cubes* and line it up with the black dot in the middle of your screen.",
            TutorialEventIDs.GrabObjectEvent => "While maintaining eye contact, press the Left Mouse Button to pick up that object.",
            _ => "Unknown tutorial step."
        };
    }
    // Function that returns the next tutorial step
    private TutorialEventIDs GetNextEvent(TutorialEventIDs currentStep)
    {
        switch (currentStep)
        {
            case TutorialEventIDs.DirectionInputEvent:
                return TutorialEventIDs.MouseInputEvent;
            case TutorialEventIDs.MouseInputEvent:
                return TutorialEventIDs.GazeObjectEvent;
            case TutorialEventIDs.GazeObjectEvent:
                return TutorialEventIDs.GrabObjectEvent;
            case TutorialEventIDs.GrabObjectEvent:
                return TutorialEventIDs.ObjectOnTableEvent;
            case TutorialEventIDs.ObjectOnTableEvent:
                return TutorialEventIDs.Finished;

            default:
                throw new System.Exception("Unknown tutorial step.");
        }
    }
    //This is here because I want to have capacity for messages between these input prompts.
    private bool requirePlayerInput(int currentMessage) {
        if( currentMessage == (int)TutorialEventIDs.DirectionInputEvent ||
            currentMessage == (int)TutorialEventIDs.MouseInputEvent || 
            currentMessage == (int)TutorialEventIDs.GazeObjectEvent ||
            currentMessage == (int)TutorialEventIDs.GrabObjectEvent){
                return true;
        }
        return false;
    }
    [Header("These are related to completing the tutorial")]
    public IntEvent tutorialEvents;
    public int eventCount;
    
    private int currentMessage = 0;
    [Header("This is for the Notifications, so that this knows when a message is hidden and can respond")]
    public static int ToastHideID = 1000;

    private bool[] eventCompletion;


    void Awake()
    {
        

        eventCompletion = new bool[eventCount];
        for(int i = 0; i < eventCount; i++) {
            eventCompletion[i] = false;
        }

        ToastNotification.Show(tutorialMessages[0]);
        eventCompletion[0] = true;

        currentEvent = TutorialEventIDs.DirectionInputEvent;
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
        Debug.Log("Recieving Event: " + item + "   CurrentMessage(" + currentMessage + ")" + "    currentEvent(" + currentEvent + ")");

        if(item == ToastHideID && currentEvent != TutorialEventIDs.Finished) {
            if(currentMessage < tutorialMessages.Length - 1) {
                currentMessage++;
                if(requirePlayerInput(currentMessage)) {
                    ToastNotification.Show(tutorialMessages[currentMessage], 1000);
                }else {
                    ToastNotification.Show(tutorialMessages[currentMessage], 5);
                    eventCompletion[currentMessage] = true;
                }
                
            }
        }else if(requirePlayerInput(currentMessage)){
            currentEvent = GetNextEvent(currentEvent);
            tutorialEvents.Raise(ToastHideID);
            eventCompletion[item] = true;
        }

        if(isTutorialComplete()) {
            ToastNotification.Show("Tutorial Complete!");
        }
        
    }
}
