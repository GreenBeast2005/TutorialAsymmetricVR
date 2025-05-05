using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanettoneGames.GenEvents;
using System;
// using UnityEditor.EditorTools;

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
    [Header("Hover for ToolTip")]
    [Tooltip("You have to change the enum to match with coresponding message element. For example, the direction input event is 1, and it goes with element 1.")]
    public string[] tutorialMessages;
    public int messageTime = 5;
    private string tutorialMessageWithEvent(TutorialEventIDs id) {
        return id switch{
            TutorialEventIDs.DirectionInputEvent => "Lets get you moving, try using WASD or the Arrow Keys!",
            TutorialEventIDs.MouseInputEvent => "Awesome! Now use the Mouse to look around the room.",
            TutorialEventIDs.GazeObjectEvent => "Now lets put those peepers to some good use! Get close to one of those *Black Cubes* and line it up with the black dot in the middle of your screen.",
            TutorialEventIDs.GrabObjectEvent => "While maintaining eye contact, press the Left Mouse Button to pick up that object.",
            _ => "Unknown tutorial step."
        };
    }
    // Function that returns the next tutorial step, if more events are added this has to be changed.
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
            
            //Notice that at the last event it just loops on itself, this is intential
            case TutorialEventIDs.ObjectOnTableEvent:
                return TutorialEventIDs.ObjectOnTableEvent;

            //As I have it written the only way to set the current event to finished is to manually set it.
            case TutorialEventIDs.Finished:
                return TutorialEventIDs.Finished;

            default:
                throw new System.Exception("Unknown tutorial step.");
        }
    }
    //This is here because I want to have capacity for messages between these input prompts, this also has to be changed if 
    //New input events are added.
    private bool requirePlayerInput(int currentMessage) {
        if( currentMessage == (int)TutorialEventIDs.DirectionInputEvent ||
            currentMessage == (int)TutorialEventIDs.MouseInputEvent || 
            currentMessage == (int)TutorialEventIDs.GazeObjectEvent ||
            currentMessage == (int)TutorialEventIDs.GrabObjectEvent ||
            currentMessage == (int)TutorialEventIDs.ObjectOnTableEvent){
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

    //This is wha gets checked to see if the tutorial is complete.
    private bool[] eventCompletion;


    void Awake()
    {
        eventCompletion = new bool[eventCount];
        for(int i = 0; i < eventCount; i++) {
            eventCompletion[i] = false;
        }

        ToastNotification.Show(tutorialMessages[0], messageTime);
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

    //This the function that gets called when in other scripts you call tutorialEvents.raise(int)
    public void OnEventRaised(int item) {
        // If the event is the toastHidID that means its a message that is being hidden
        if(item == ToastHideID) {
            // Move to the next message
            if(currentMessage < eventCount) {
                eventCompletion[currentMessage] = true;
                currentMessage++;
                // If the next message requires the player to something, we want it to stay up for a while, otherwise it disapears after messageTime seconds
                if(requirePlayerInput(currentMessage)) {
                    ToastNotification.Show(tutorialMessages[currentMessage], 1000);
                }else {
                    ToastNotification.Show(tutorialMessages[currentMessage], messageTime);
                }
                
            }
        // Otherwise we check if this id requires player input.
        }else if(requirePlayerInput(currentMessage)){
            // Move to next input event event
            currentEvent = GetNextEvent(currentEvent);
            tutorialEvents.Raise(ToastHideID);
            eventCompletion[item] = true;
        }

        // Once the tutorial is complete we display the final message and set the current even to finished so no more events
        // are sent.
        if(isTutorialComplete()) {
            currentEvent = TutorialEventIDs.Finished;
            ToastNotification.Show(tutorialMessages[eventCount], messageTime+10);
        }
        
    }
}
