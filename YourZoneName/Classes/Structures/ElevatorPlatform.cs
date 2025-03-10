using Godot;
using System;

namespace SpecFreqCustomZone
{
    public partial class ElevatorPlatform : MeshInstance3D
    {
        private Vector3 _currentPosition;
        private bool _down;
        private double _moveAmount;
        public override void _Ready()
        {
            _down = false;
            _currentPosition = GlobalPosition;
            SetProcess(false);
        }
        public override void _Process(double delta)
        {
            _moveAmount = delta / 10;
            _currentPosition = GlobalPosition;
            _currentPosition.Y += (float)(_down ? -_moveAmount : _moveAmount);
            if (_currentPosition.Y <= 0.1f)
            {
                _down = false;
            }
            else if (_currentPosition.Y >= 0.26f)
            {
                _down = true;
            }
            GlobalPosition = _currentPosition;
        }
    }
}