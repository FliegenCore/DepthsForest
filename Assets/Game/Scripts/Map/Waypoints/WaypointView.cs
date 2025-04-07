using System;
using Assets;
using Game.Common;
using UnityEditor;
using UnityEngine;

namespace Game.World
{
    public enum WaypointType
    {
        Empty,
        Start,
        Enemy,
        Delivery,
        NPC
    }
    
    public class WaypointView : MonoBehaviour, ITooltipInformator
    {
        [SerializeField] private WaypointType _waypointType;
        [SerializeField] private WaypointView[] _neighbors;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeReference] private Waypoint _currentWaypointType;
        
        private LineRenderer _lineRenderer;

        private EnemyWaypointEvent _enemyWaypoint;
        private StartWayPoint _startWayPoint;
        private NPCWayPointEvent _NPCWayPoint;
        private DeliveryPointEvent _deliveryWayPoint;
        
        public WaypointView[] Neighbors => _neighbors;
        public Waypoint CurrentWaypointType => _currentWaypointType;
        public WaypointType WaypointType => _waypointType;
        public string Description => _currentWaypointType.Description;

        public int Index;
        
        public void Initialize()
        {
            InitializeWaypoint();
            UpdateSpriteIcon();
        }

        public void SetWaypointType(WaypointType newWaypointType)
        {
            _waypointType = newWaypointType;
        }
        
        public void ResetWaypoint()
        {
            _waypointType = WaypointType.Empty;
        }
        
        private void OnValidate()
        {
            InitializeWaypoint();
            UpdateEditorIcon();
        }
    
        private void InitializeWaypoint()
        {
            if (_currentWaypointType != null && IsCorrectType(_currentWaypointType, _waypointType))
                return;
            
            switch (_waypointType)
            {
                case WaypointType.Empty:
                    _currentWaypointType = new EmptyWaypointEvent();
                    break;
                case WaypointType.Start:
                    _currentWaypointType = new StartWayPoint();
                    break;
                case WaypointType.Enemy:
                    _currentWaypointType = new EnemyWaypointEvent();
                    break;
                case WaypointType.NPC:
                    _currentWaypointType = new NPCWayPointEvent();
                    break;
                case WaypointType.Delivery:
                    _currentWaypointType = new DeliveryPointEvent();
                    break;
                default:
                    _currentWaypointType = new EnemyWaypointEvent();
                    break;
            }
        }
    
        private bool IsCorrectType(Waypoint waypoint, WaypointType type)
        {
            return type switch
            {
                WaypointType.Start => waypoint is StartWayPoint,
                WaypointType.Empty => waypoint is EmptyWaypointEvent,
                WaypointType.Enemy => waypoint is EnemyWaypointEvent,
                WaypointType.NPC => waypoint is NPCWayPointEvent,
                WaypointType.Delivery => waypoint is DeliveryPointEvent,
                _ => waypoint is EnemyWaypointEvent
            };
        }
    
        private void UpdateEditorIcon()
        {
#if UNITY_EDITOR
            string iconName = _waypointType switch
            {
                WaypointType.Start => "sv_label_0",
                WaypointType.Empty => "sv_label_4",
                WaypointType.Enemy => "sv_label_6",
                WaypointType.NPC => "sv_label_1",
                WaypointType.Delivery => "sv_label_2",
                _ => "sv_label_6"
            };
        
            var icon = EditorGUIUtility.IconContent(iconName).image as Texture2D;
            EditorGUIUtility.SetIconForObject(gameObject, icon);
#endif
        }

        private void UpdateSpriteIcon()
        {
            string iconName = _waypointType switch
            {
                WaypointType.Start => "Empty",
                WaypointType.Empty => "Empty",
                WaypointType.Enemy => "Red",
                WaypointType.NPC => "Yellow",
                WaypointType.Delivery => "Green",
                _ => "Yellow"
            };

            Sprite sprite = AssetLoader.LoadSync<Sprite>("Sprites/MapIcons/" + iconName + "Unknown");

            _spriteRenderer.sprite = sprite;
        }
        
    }

    public class Waypoint
    {
        public string Description => _description;
        
        protected string _description;
        
        public virtual void TriggerEvent()
        {
        }
    }

    [Serializable]
    public class EmptyWaypointEvent : Waypoint
    {
        public EmptyWaypointEvent()
        {
            _description = "Empty";
        }
    }
    
    [Serializable]
    public class EnemyWaypointEvent : Waypoint
    {
        [SerializeField] private NPC _enemy;

        public EnemyWaypointEvent()
        {
            _description = "Enemy";
        }
        
        public override void TriggerEvent()
        {
            G.Get<MapFigureController>().Disable();
            G.Get<FadeController>().FadeIn(() =>
            {
                G.Get<SceneController>().ChangeScene(SceneName.BattleScene);
                G.Get<FadeController>().FadeOut();
                
                var enemyStorage = AssetLoader.LoadSync<EnemyStorage>("Enemy/Storage/Storage");
                
                enemyStorage.Enemy = _enemy;
            });
        }
    }
    
    [Serializable]
    public class NPCWayPointEvent : Waypoint
    {
        [SerializeField] private NPC _enemy;

        public NPCWayPointEvent()
        {
            _description = "NPC";
        }
        
        public override void TriggerEvent()
        {
            G.Get<MapFigureController>().Disable();
            G.Get<FadeController>().FadeIn(() =>
            {
                G.Get<SceneController>().ChangeScene(SceneName.BattleScene);
                G.Get<FadeController>().FadeOut();
                
                var enemyStorage = AssetLoader.LoadSync<EnemyStorage>("Enemy/Storage/Storage");
                
                enemyStorage.Enemy = _enemy;
            });
        }
    }
    
    [Serializable]
    public class DeliveryPointEvent : Waypoint
    {
        [SerializeField] private MailAccepter _mailAccepter;
        [SerializeField] private bool _isRight;
        [SerializeField] private Sprite _location;
        
        public DeliveryPointEvent()
        {
            _description = "Delivery";
        }
        
        public override void TriggerEvent()
        {
            G.Get<MapFigureController>().Disable();

            G.Get<FadeController>().FadeIn(() =>
            {
                var deliveryStorage = AssetLoader.LoadSync<MailAccepterStorage>("NPC/MailAccepter/Storage");
                deliveryStorage.MailAccepter = _mailAccepter;
                deliveryStorage.NpcIsRight = _isRight;
                deliveryStorage.LocationSprite = _location;
                G.Get<SceneController>().ChangeScene(SceneName.DeliveryScene);
                G.Get<FadeController>().FadeOut();
            });
        }
    }

    public class StartWayPoint : Waypoint
    {
        public StartWayPoint()
        {
            _description = "Start";
        }
    }
}
