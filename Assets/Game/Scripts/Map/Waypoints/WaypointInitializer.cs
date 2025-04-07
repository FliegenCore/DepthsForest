using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.World
{
    public class WaypointInitializer : MonoBehaviour
    {
        private List<WaypointView> _waypointViews;
        
        public void Initialize()
        {
            _waypointViews = GetComponentsInChildren<WaypointView>(includeInactive: true)
                .OrderBy(waypoint => waypoint.transform.GetSiblingIndex())
                .ToList();

            for (int i = 0; i <_waypointViews.Count; i++)
            {
                _waypointViews[i].Index = i;
            }
            
            DrawLines();
            
        }

        public void InitializeWaypoints()
        {
            for (int i = 0; i <_waypointViews.Count; i++)
            {
                _waypointViews[i].Initialize();
            }
        }
        
        private void DrawLines()
        {
            HashSet<(WaypointView, WaypointView)> drawnPairs = new HashSet<(WaypointView, WaypointView)>();

            foreach (var waypoint in _waypointViews)
            {
                LineRenderer lineRenderer = waypoint.GetComponent<LineRenderer>();
                if (lineRenderer == null)
                {
                    lineRenderer = waypoint.gameObject.AddComponent<LineRenderer>();
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;
                    lineRenderer.material = new Material(Shader.Find("Sprites/Default")) { color = Color.blue };
                }

                var neighborsToDraw = waypoint.Neighbors
                    .Where(neighbor => !drawnPairs.Contains((waypoint, neighbor)) && 
                                       !drawnPairs.Contains((neighbor, waypoint)))
                    .ToList();

                lineRenderer.positionCount = neighborsToDraw.Count * 2;

                for (int i = 0; i < neighborsToDraw.Count; i++)
                {
                    var neighbor = neighborsToDraw[i];
            
                    lineRenderer.SetPosition(i * 2, waypoint.transform.position);
                    lineRenderer.SetPosition(i * 2 + 1, neighbor.transform.position);
            
                    drawnPairs.Add((waypoint, neighbor));
                }
            }
        }
    }
}
