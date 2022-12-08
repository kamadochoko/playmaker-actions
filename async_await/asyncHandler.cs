using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class asyncHandler : MonoBehaviour
{

    public PlayMakerFSM fsm;

    private readonly CancellationTokenSource cts = new CancellationTokenSource();


    public async void startAsync(string sendEvent1, string sendEvent2, string sendEvent3, string completeEvent)
    {

        //Debug.Log(">>>> async void Start <<<<");

        CancellationToken cancellationToken = cts.Token;

        var (msg1, msg2, msg3) = await UniTask.WhenAll(
            sendGlobalTransition(sendEvent1, cancellationToken),
            sendGlobalTransition(sendEvent2, cancellationToken),
            sendGlobalTransition(sendEvent3, cancellationToken)
            );

        //Debug.Log(msg1 + "->" + msg2 + "->" + msg3);

        if (completeEvent is not null)
        {

            fsm.SendEvent(completeEvent);

            //Debug.Log("completeEvent");

        }

    }


    public void OnDestroy()
    {

        cts.Cancel();
        cts.Dispose();

    }


    public async UniTask<string> sendGlobalTransition(string sendEvent, CancellationToken cancellationToken)
    {

        cancellationToken.ThrowIfCancellationRequested();

        await UniTask.SwitchToThreadPool();

        //await UniTask.Delay(TimeSpan.FromSeconds(1));

        if (sendEvent is not null)
        {

            try
            {

                await UniTask.Yield();
                fsm.SendEvent(sendEvent);

            }
            catch (Exception e) when (!(e is OperationCanceledException))
            {

                Debug.Log(e);
            }

        }

        return sendEvent;

    }

}
