using UnityEngine;

namespace HairyEngine.HairyCamera
{
    public class CenterDistanceInflunce : BaseCameraScript, IPreMove
    {
        public float MaxHorizontalInfluence = 3f;
        public float MaxVerticalInfluence = 2f;

        public float InfluenceSmoothness = .2f;

        Vector2 _influence;
        Vector2 _velocity;
        public int PriorityOrder => 2;

        public void HandleStartMove(Vector3 position)
        {
            if (enabled)
            {
                var direction = BaseCameraController.Targets.velocity.normalized;

                var hInfluence = direction.x * MaxHorizontalInfluence;
                var vInfluence = direction.y * MaxVerticalInfluence;

                Vector2 influnce = new Vector3(hInfluence, vInfluence, 0);

                _influence = Vector2.Lerp(_influence, influnce, InfluenceSmoothness * Time.deltaTime);
                //_influence = Vector2.SmoothDamp(_influence, influnce, ref _velocity, InfluenceSmoothness, Mathf.Infinity, Time.deltaTime);

                BaseCameraController.ApplyInfluence(_influence);
            }
        }
    }
}