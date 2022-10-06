using UnityEngine;

namespace PlayerLogic.Spirit
{
    public class SpiritBox : Spirit
    {
        [SerializeField] private SpiritSetter _spiritSetter;

        private Vector3 _gatePosition;

        private Vector3 Center
        {
            get
            {
                Vector3 playerPosition = Player.transform.position;

                if (_gatePosition == default)
                {
                    gameObject.SetActive(false);
                    _gatePosition = playerPosition;
                }
                else
                {
                    gameObject.SetActive(true);
                }

                Vector3 offset = (_gatePosition - playerPosition) / 2;

                return playerPosition + offset;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _spiritSetter.SpiritPointFound += UpdateGatePosition;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _spiritSetter.SpiritPointFound -= UpdateGatePosition;
        }

        protected override void Update()
        {
            base.Update();
            SetZScale();
        }

        private void UpdateGatePosition(Vector3 gatePosition)
        {
            _gatePosition = gatePosition;
        }
    
        protected override void SetPosition()
        {
            transform.position = Center;
        }
    
        private void SetZScale()
        {
            float scaleZ = (Center - Player.transform.position).z * 2;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scaleZ);
        }
    }
}
