using Cysharp.Threading.Tasks;
using System;
using System.Threading;
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

        // var cancellationToken = this.GetCancellationTokenOnDestroy();
        var cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        Debug.Log("async void Start");

        var (msg1, msg2, msg3)
                = await UniTask.WhenAll(
                GetAwait1(cancellationToken),
                GetAwait2(cancellationToken),
                GetAwait3(cancellationToken)
                );

        Debug.Log(msg3 + "->" + msg1 + "->" + msg2);

        fsm.SendEvent(awaitCompleteEvent);

        Debug.Log("Complete!");

        cts.Cancel();

    }


    private async UniTask<string> GetAwait1(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        try
        {
            await UniTask.Delay(2000);
            fsm.SendEvent(await1sendEvent);
        }
        catch (OperationCanceledException e)
        {
            Debug.Log(e.Message);
        }
        return "await1";
    }


    private async UniTask<string> GetAwait2(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        try
        {
            await UniTask.Delay(3000);
            fsm.SendEvent(await2sendEvent);
        }
        catch (OperationCanceledException e)
        {
            Debug.Log(e.Message);
        }
        return "await2";
    }


    private async UniTask<string> GetAwait3(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        try
        {
            await UniTask.Delay(1000);
            fsm.SendEvent(await3sendEvent);
        }
        catch (OperationCanceledException e)
        {
            Debug.Log(e.Message);
        }
        return "await3";
    }
}
