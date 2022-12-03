
namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("General")]
    [Tooltip("using Unitask with Playmaker")]
    public class awaitHandler : FsmStateAction
    {


        public asyncHandler asyncHandler;

        public FsmEvent await1sendEvent;
        public FsmEvent await2sendEvent;
        public FsmEvent await3sendEvent;
        public FsmEvent awaitCompleteEvent;


        // Code that runs on entering the state.
        public override void Reset()
        {

            asyncHandler = null;
            await1sendEvent = null;
            await2sendEvent = null;
            await3sendEvent = null;
            awaitCompleteEvent = null;

        }


        public override void OnEnter()
        {

            asyncHandler.sendEvent(await1sendEvent?.Name, await2sendEvent?.Name, await3sendEvent?.Name, awaitCompleteEvent?.Name);

        }

    }

}
