using Cysharp.Threading.Tasks;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

public class asyncHandler : MonoBehaviour
{

    public PlayMakerFSM fsm;

    public string await1sendEvent;
    public string await2sendEvent;
    public string await3sendEvent;
    public string awaitCompleteEvent;


    public void setEvent(string _await1sendEvent, string _await2sendEvent, string _await3sendEvent, string _awaitCompleteEvent)
    {
        Debug.Log("setEvent");
        await1sendEvent = _await1sendEvent;
        await2sendEvent = _await2sendEvent;
        await3sendEvent = _await3sendEvent;
        awaitCompleteEvent = _awaitCompleteEvent;

    }


    public async void Start()
    {

        Debug.Log("async void Start");
        var (msg1, msg2, msg3)
                = await UniTask.WhenAll(GetAwait1(), GetAwait2(), GetAwait3());
        Debug.Log(msg3 + "->" + msg1 + "->" + msg2);


        fsm.SendEvent(awaitCompleteEvent);
        Debug.Log("Complete!");

    }


    private async UniTask<string> GetAwait1()
    {
        await UniTask.Delay(2000);
        fsm.SendEvent(await1sendEvent);
        return "await1";
    }

    private async UniTask<string> GetAwait2()
    {
        await UniTask.Delay(3000);
        fsm.SendEvent(await2sendEvent);
        return "await2";
    }

    private async UniTask<string> GetAwait3()
    {
        await UniTask.Delay(1000);
        fsm.SendEvent(await3sendEvent);
        return "await3";
    }
}
