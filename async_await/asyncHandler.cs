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


    public void sendEvent(string _await1sendEvent, string _await2sendEvent, string _await3sendEvent, string _awaitCompleteEvent)
    {

        Debug.Log("setEvent");
        await1sendEvent = _await1sendEvent;
        await2sendEvent = _await2sendEvent;
        await3sendEvent = _await3sendEvent;
        awaitCompleteEvent = _awaitCompleteEvent;

    }


    public async void Start()
    {

        var cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        Debug.Log("async void Start");

        var (msg1, msg2, msg3)
                = await UniTask.WhenAll(
                GetAwait(await1sendEvent, cancellationToken),
                GetAwait(await2sendEvent, cancellationToken),
                GetAwait(await3sendEvent, cancellationToken)
                );

        Debug.Log(msg1 + "->" + msg2 + "->" + msg3);

        if (awaitCompleteEvent is not null)
        {
            fsm.SendEvent(awaitCompleteEvent);

            Debug.Log("awaitCompleteEvent!");

        }

        cts.Cancel();

    }


    private async UniTask<string> GetAwait(string sendEvent, CancellationToken cancellationToken)
    {

        cancellationToken.ThrowIfCancellationRequested();

        if (sendEvent is not null)
        {

            try
            {
                await UniTask.Yield();
                fsm.SendEvent(sendEvent);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log(e.Message);
            }
        }

        return sendEvent;
        
    }

}
