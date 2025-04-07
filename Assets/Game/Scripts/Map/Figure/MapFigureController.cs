using System.Linq;
using Assets;
using DG.Tweening;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class MapFigureController : MonoBehaviour, IService
    {
        private float _yOffset = -1.5f;
        
        private MapFigureView _figureView;
        private MapController _mapController;
        private Camera _mapCamera;
        private WaypointView _currentWaypoint;
        
        
        private bool _enabled;
        private bool _enableFollow;
        private float _minDepth;        
        
        public bool CanDestroyed => true;
        
        public void Initialize()
        {
            _enableFollow = true;
            _mapCamera = Camera.main;
            _enabled = true;
            _mapController = G.Get<MapController>();
            _currentWaypoint = _mapController.GetStartWaypoint();
            SpawnFigure();
            PlaceFigure();
            LoadMinMapDepth();
        }
        
        private void Update()
        {
            AutoSetDirection();
            CameraFollow();
            ClickOnWaypoint();
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }

        private void CameraFollow()
        {
            if (!_enableFollow)
            {
                return;
            }
            
            float y = _figureView.transform.position.y + _yOffset;

            y = Mathf.Clamp(y, _minDepth, 0.95f); 
            
            Vector3 cameraPosition = 
                new Vector3(_mapCamera.transform.position.x, y, _mapCamera.transform.position.z);

            _mapCamera.transform.position = 
                Vector3.Lerp(_mapCamera.transform.position, cameraPosition, Time.deltaTime * 10);
        }
        
        private void ClickOnWaypoint()
        {
            if (!_enabled)
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 rayPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(rayPoint, Vector2.zero);
                
                if (hit.collider == null)
                {
                    return;
                }
                
                if (hit.collider.TryGetComponent(out WaypointView waypoint))
                {
                    if (_currentWaypoint != null)
                    {
                        if (!_currentWaypoint.Neighbors.Contains(waypoint))
                        {
                            return;
                        }
                    }

                    _figureView.Move(waypoint);
                    _currentWaypoint = waypoint;
                    Disable();
                }
            }
        }
        
        private void SpawnFigure()
        {
            MapFigureView asset = AssetLoader.LoadSync<MapFigureView>("Map/MapFigureView");
            _figureView = Instantiate(asset);
        }

        private void PlaceFigure()
        {
            Vector2 position = _mapController.GetStartWaypointPosition();
            
            _figureView.SetPosition(position);
           
        }
        
        private void LoadMinMapDepth()
        {
            MapStorage mapStorage = AssetLoader.LoadSync<MapStorage>("Map/Storage");
            
            _minDepth = mapStorage.CurrentDepth;
            
            if (!Mathf.Approximately(mapStorage.CurrentDepth, mapStorage.LastDepth))
            {
                mapStorage.LastDepth = mapStorage.CurrentDepth;
                _enableFollow = false;
                ShowDepthAnimation();
            }
            
            float y = _figureView.transform.position.y + _yOffset;
            y = Mathf.Clamp(y, _minDepth, 0.95f); 
            Vector3 cameraPosition = 
                new Vector3(_mapCamera.transform.position.x, y, _mapCamera.transform.position.z);

            _mapCamera.transform.position = cameraPosition;
        }

        private void AutoSetDirection()
        {
            if (Input.mousePosition.y > Screen.height / 2 + Screen.height * 0.25f)
            {
                _yOffset = 1.5f; 
            }
            else if(Input.mousePosition.y < Screen.height / 2 - Screen.height * 0.25f)
            {
                _yOffset = -1.5f;
            }
        }
        
        private void ShowDepthAnimation()
        {
            _mapCamera.transform.DOMoveY(_minDepth, 2.5f)
                .OnComplete(() =>
                {
                    _enableFollow = true;
                });
        }
    }
}
