using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;

namespace Game.World
{
    public class MapController : MonoBehaviour, IService
    {
        private List<WaypointView> _waypoints = new List<WaypointView>();
        private WaypointInitializer _waypointInitializer;

        private float _currentHeight;
        
        public bool CanDestroyed => true;

        public void Initialize()
        {
            _waypointInitializer = FindObjectOfType<WaypointInitializer>();
            _waypoints = FindObjectsOfType<WaypointView>().ToList();
            _waypointInitializer.Initialize();
            
            LoadMap();
            _waypointInitializer.InitializeWaypoints();
        }

        public WaypointView GetStartWaypoint()
        {
            var mapAsset = AssetLoader.LoadSync<MapStorage>("Map/Storage");
            
            if (mapAsset.CurrentPlayerWaypointIndex > -1)
            {
                foreach (var waypoint in _waypoints)
                {
                    if (waypoint.Index == mapAsset.CurrentPlayerWaypointIndex)
                    {
                        return waypoint;
                    }
                }
            }
            
            foreach (var waypoint in _waypoints)
            {
                if (waypoint.CurrentWaypointType is StartWayPoint)
                {
                    WaypointView startWaypoint = waypoint;
                    return startWaypoint;
                }
            }
            
            return null;
        }
        
        public Vector2 GetStartWaypointPosition()
        {
            var mapAsset = AssetLoader.LoadSync<MapStorage>("Map/Storage");

            if (mapAsset.CurrentPlayerWaypointIndex > -1)
            {
                foreach (var waypoint in _waypoints)
                {
                    if (waypoint.Index == mapAsset.CurrentPlayerWaypointIndex)
                    {
                        return waypoint.transform.position;
                    }
                }
            }
            
            
            foreach (var waypoint in _waypoints)
            {
                if (waypoint.CurrentWaypointType is StartWayPoint)
                {
                    Vector2 startWaypoint = waypoint.transform.position;
                    return startWaypoint;
                }
            }
            
            return Vector2.zero;
        }

        private void LoadMap()
        {
            MapStorage mapStorage = AssetLoader.LoadSync<MapStorage>("Map/Storage");

            foreach (var waypoint in _waypoints)
            {
                foreach (var wp in mapStorage.Waypoints)
                {
                    if (waypoint.Index == wp.Key)
                    {
                        waypoint.SetWaypointType(wp.Value); 
                    }
                }
            }
        }
        
        private void SaveMap()
        {
            MapStorage mapStorage = AssetLoader.LoadSync<MapStorage>("Map/Storage");
            
            mapStorage.Waypoints.Clear();

            foreach (var waypoint in _waypoints)
            {
                mapStorage.Waypoints.Add(waypoint.Index, waypoint.WaypointType);
            }
        }
        
        private void OnDestroy()
        {
            SaveMap();
        }
    }
}
