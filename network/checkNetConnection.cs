using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    /// <summary>
    /// �l�b�g���[�N�ڑ��̊m�F
    /// </summary>
	[ActionCategory("General")]
    [Tooltip("�l�b�g���[�N��Ԃ̊ȈՊm�F")]
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

            // �l�b�g���[�N�̏��
            switch (Application.internetReachability)
            {
                // �l�b�g���[�N���B�s��
                case NetworkReachability.NotReachable:
                    storeResult.Value = false;
                    Fsm.Event(sendEventFaild);
                    break;
                // �L�����A�f�[�^�o�R���B��
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                // Wifi�܂��̓P�[�u���o�R���B��
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    storeResult.Value = true;
                    Fsm.Event(sendEventSuccess);
                    break;
            }


        }


    }

}
