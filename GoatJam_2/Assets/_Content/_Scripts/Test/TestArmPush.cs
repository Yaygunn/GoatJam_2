using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yaygun
{
    public class TestArmPush : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private Transform _arm1;
        [SerializeField] private float _pushForce;
        [SerializeField] private ForceMode2D _forceMode;

        [FoldoutGroup("Anim"), SerializeField] 
        private float _pushArmDistance;
        [FoldoutGroup("Anim"), SerializeField] 
        private float _recoverTime;
        [FoldoutGroup("Anim"), SerializeField] 
        private TestModelFollow _testModelFollow;
        

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButton(0))
                Push();
        }

        private async void Push()
        {
            _testModelFollow.SetShouldFollow(false);
            
            _body.AddForce(_arm1.right * -1 * _pushForce, _forceMode );
            
            _testModelFollow.PlayPushAnim(_pushArmDistance);
            
            await UniTask.WaitForSeconds(_recoverTime);
            
            _testModelFollow.SetShouldFollow(true);

        }
    }
}
