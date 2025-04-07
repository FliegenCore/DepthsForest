using Assets;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class HoverTooltipController : MonoBehaviour, IService
    {

        private bool _wasInitialied;
        
        private HoverTooltip _hoverTooltip;
        
        public bool CanDestroyed => false;
        
        public void Initialize()
        {
            if (_wasInitialied)
            {
                return;
            }

            var asset = AssetLoader.LoadSync<HoverTooltip>("Common/TooltipView");
            
            _hoverTooltip = Instantiate(asset, transform);
            
            DontDestroyOnLoad(gameObject);
            
            _wasInitialied = true;
        }

        private void Update()
        {
            FindInformator();
        }

        private void FindInformator()
        {
            Vector2 rayPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(rayPoint, Vector2.zero);

            if (hit.collider == null)
            {
                _hoverTooltip.Hide();
                return;
            }
            
            if (hit.collider.TryGetComponent(out ITooltipInformator informator))
            {
                _hoverTooltip.Show(informator.Description);

                Vector2 tooltipPosition = Camera.main.ScreenToWorldPoint(
                    (Vector2)Input.mousePosition) + (Vector3)_hoverTooltip.Offset;
                
                _hoverTooltip.transform.position = tooltipPosition;
            }
            else
            {
                _hoverTooltip.Hide();
            }
        }
    }

    public interface ITooltipInformator
    {
        string Description { get; }
    }
}
