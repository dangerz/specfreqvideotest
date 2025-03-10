using Godot;
using System;

namespace SpecFreqCustomZone
{
    public partial class FenceGate : StaticBody3D
    {
        [Export]
        public DoorDirection SlidingDoorDirection;
        [Export]
        public float DoorMoveSpeed = 0.2f;
        [Export]
        public float DoorMoveDistance = 10;
        [Export]
        public float SnapToOriginalThreshold = 0.2f;
        public enum DoorDirection
        {
            Left,
            Right
        }

        private int _numPeopleInside;
        private Vector3 _baselineDoorPosition = new Vector3(0, 0, 0);
        private Vector3 _leftDoorPosition = new Vector3(1.7f, 0, 0);
        private Vector3 _rightDoorPosition = new Vector3(-1.7f, 0, 0);

        private MeshInstance3D _gate;
        private CollisionShape3D _collider;
        public override void _Ready()
        {
            _gate = GetNode<MeshInstance3D>("Gate");
            _collider = GetNode<CollisionShape3D>("GateCollider");

            _baselineDoorPosition = _gate.Position;

            _leftDoorPosition = new Vector3(DoorMoveDistance, 0, 0);
            _rightDoorPosition = new Vector3(-DoorMoveDistance, 0, 0);

            _numPeopleInside = 0;
        }
        public override void _Process(double delta)
        {
            if (_numPeopleInside == 0)
            {
                if (Math.Abs(_gate.Position.DistanceTo(_baselineDoorPosition)) > SnapToOriginalThreshold)
                {
                    _gate.Position = R.Lerp(_gate.Position, _baselineDoorPosition, (float)delta + DoorMoveSpeed);
                    Vector3 translation = _gate.Position;
                    translation.Y = _baselineDoorPosition.Y;
                    _gate.Position = translation;
                }
                else
                {
                    _gate.Position = _baselineDoorPosition;
                }
            }
            else
            {
                switch (SlidingDoorDirection)
                {
                    case DoorDirection.Left:
                        {
                            _gate.Position = R.Lerp(_gate.Position, _leftDoorPosition, (float)delta + DoorMoveSpeed);
                            Vector3 translation = _gate.Position;
                            translation.Y = _baselineDoorPosition.Y;
                            _gate.Position = translation;
                        }
                        break;
                    case DoorDirection.Right:
                        {
                            _gate.Position = R.Lerp(_gate.Position, _rightDoorPosition, (float)delta + DoorMoveSpeed);
                            Vector3 translation = _gate.Position;
                            translation.Y = _baselineDoorPosition.Y;
                            _gate.Position = translation;
                        }
                        break;
                }
            }
            _collider.Position = _gate.Position;
        }

        public void _on_Area_body_entered(Node aBody)
        {
            if (aBody is PlayerAPI || aBody is VehicleAPI)
            {
                _numPeopleInside++;
            }
        }
        public void _on_Area_body_exited(Node aBody)
        {
            if (aBody is PlayerAPI || aBody is VehicleAPI)
            {
                _numPeopleInside--;
            }
        }
    }
}