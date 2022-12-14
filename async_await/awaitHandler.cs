
using UnityEngine;
namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("General")]
    [Tooltip("using Unitask with Playmaker")]
    public class awaitHandler : FsmStateAction
    {

        public asyncHandler asyncHandler;

        public FsmString sendEventName1;
        public FsmString sendEventName2;
        public FsmString sendEventName3;
        public FsmString completeEventName;
        public FsmBool isDelay;
        public FsmFloat delaySec;


        // Code that runs on entering the state.
        public override void Reset()
        {

            asyncHandler = null;
            sendEventName1 = null;
            sendEventName2 = null;
            sendEventName3 = null;
            completeEventName = null;
            isDelay = null;
            delaySec = null;

        }

        public override void OnEnter()
        {

            asyncHandler.startAsync(sendEventName1.Value, sendEventName2.Value, sendEventName3.Value, completeEventName.Value, isDelay.Value, delaySec.Value);

        }

    }

}
