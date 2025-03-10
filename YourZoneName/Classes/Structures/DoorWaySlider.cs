using Godot;
using System;

namespace SpecFreqCustomZone
{
    public partial class DoorWaySlider : DoorwaySliderAPI
    {
        private CollisionShape3D _collider;
        public override void _Ready()
        {
            _collider = GetNode<CollisionShape3D>("Collider");
            SetProcess(false);
        }

        public void DisableCollider()
        {
            _collider.Disabled = true;
        }
        public void EnableCollider()
        {
            _collider.Disabled = false;
        }
        public void SetCollider(bool aEnabled)
        {
            _collider.Disabled = aEnabled;
        }
    }
}