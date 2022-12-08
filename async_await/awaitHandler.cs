namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("General")]
    [Tooltip("using Unitask with Playmaker")]
    public class awaitHandler : FsmStateAction
    {


        public asyncHandler asyncHandler;

        public FsmEvent sendEvent1;
        public FsmEvent sendEvent2;
        public FsmEvent sendEvent3;
        public FsmEvent completeEvent;


        // Code that runs on entering the state.
        public override void Reset()
        {

            asyncHandler = null;
            sendEvent1 = null;
            sendEvent2 = null;
            sendEvent3 = null;
            completeEvent = null;

        }


        public override void OnEnter()
        {

            asyncHandler.startAsync(sendEvent1?.Name, sendEvent2?.Name, sendEvent3?.Name, completeEvent?.Name);

        }

    }

}
