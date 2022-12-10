using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    /// <summary>
    /// ネットワーク接続の確認
    /// </summary>
	[ActionCategory("General")]
    [Tooltip("ネットワーク状態の簡易確認")]
    public class checkNetConnection : FsmStateAction
    {

        public FsmBool storeResult;

        public FsmEvent sendEventSuccess;
        public FsmEvent sendEventFaild;


        public override void Reset()
        {
            sendEventSuccess = null;
            sendEventFaild = null;
            storeResult = null;
        }


        public override void OnUpdate()
        {

            // ネットワークの状態
            switch (Application.internetReachability)
            {
                // ネットワーク到達不可
                case NetworkReachability.NotReachable:
                    storeResult.Value = false;
                    Fsm.Event(sendEventFaild);
                    break;
                // キャリアデータ経由到達可
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                // Wifiまたはケーブル経由到達可
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    storeResult.Value = true;
                    Fsm.Event(sendEventSuccess);
                    break;
            }


        }


    }

}
