using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class asyncHandler : MonoBehaviour
{

    public PlayMakerFSM fsm;

    private readonly CancellationTokenSource cts = new CancellationTokenSource();

    public async void startAsync(string sendEventName1, string sendEventName2, string sendEventName3, string completeEventName,
        bool isDelay, float delaySec)
    {

        //Debug.Log(">>>> async void Start <<<<");
        CancellationToken cancellationToken = cts.Token;

        // Debug.Log(Thread.CurrentThread.ManagedThreadId);
        if (isDelay == true && delaySec > 0f)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delaySec));
        }

        var (msg1, msg2, msg3) = await UniTask.WhenAll(
            sendGlobalTransition(sendEventName1, cancellationToken),
            sendGlobalTransition(sendEventName2, cancellationToken),
            sendGlobalTransition(sendEventName3, cancellationToken)
            );

        // Debug.Log(msg1 + "->" + msg2 + "->" + msg3);

        if (completeEventName is not null)
        {
            // Debug.Log("completeEventName");
            fsm.SendEvent(completeEventName);

        }

    }


    public void OnDestroy()
    {

        cts.Cancel();
        cts.Dispose();

    }


    public async UniTask<string> sendGlobalTransition(string sendEvent, CancellationToken cancellationToken)
    {

        // Debug.Log("sendGlobalTransition=>" + sendEvent);
        cancellationToken.ThrowIfCancellationRequested();


        if (sendEvent is not null)
        {

            try
            {

                // Debug.Log(Thread.CurrentThread.ManagedThreadId);
                await UniTask.SwitchToThreadPool();

                await UniTask.Yield();
                // Debug.Log(Thread.CurrentThread.ManagedThreadId);
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
