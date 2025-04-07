using System.Collections;
using Assets;
using DG.Tweening;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class MapFigureView : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void Move(WaypointView waypoint)
        {
            _animator.Play("Move");
            Flip(waypoint.transform.position);
            transform.DOJump(waypoint.transform.position, 0.2f, 3, 1f).SetEase(Ease.Flash)
                .OnComplete(() =>
                {
                    _animator.Play("Idle");
                    TriggerEvent(waypoint);
                });
            
        }

        private void Flip(Vector2 direction)
        {
            if (direction.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
        }

        private void TriggerEvent(WaypointView waypoint)
        {
            MapStorage storage = AssetLoader.LoadSync<MapStorage>("Map/Storage");
            
            storage.CurrentPlayerWaypointIndex = waypoint.Index;
            
            waypoint.CurrentWaypointType.TriggerEvent();
            waypoint.ResetWaypoint();
            StartCoroutine(EnableFigureController());
        }

        private IEnumerator EnableFigureController()
        {
            yield return new WaitForSeconds(0.2f);
            MapFigureController figureController = G.Get<MapFigureController>();

            if (figureController != null)
            {
                figureController.Enable();
            }
        }
    }
}
