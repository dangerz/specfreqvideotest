using Godot;
using System;

namespace SpecFreqCustomZone
{
    public partial class DoorWay : DoorwayAPI
    {
        [Export]
        public NodePath SlidingDoorPath;
        [Export]
        public DoorDirection SlidingDoorDirection;
        [Export]
        public float DoorMoveSpeed = 0.2f;
        public enum DoorDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        private AudioStreamPlayer3D _doorSound;
        private const float MAX_SOUND_DISTANCE = 50;
        private int _numPeopleInside;
        private int _previousNumPeopleInside;
        private Vector3 _baselineDoorPosition = new Vector3(0, 0, 0);
        private Vector3 _leftDoorPosition = new Vector3(1.7f, 0, 0);
        private Vector3 _rightDoorPosition = new Vector3(-1.7f, 0, 0);

        public DoorWaySlider _slidingDoor;
        public override void _Ready()
        {
            _doorSound = GetNode<AudioStreamPlayer3D>("DoorwaySound");
            _slidingDoor = GetNode<DoorWaySlider>(SlidingDoorPath);
            _previousNumPeopleInside = 0;
            _numPeopleInside = 0;
        }
        public override void _Process(double delta)
        {
            if (_numPeopleInside == 0)
            {
                if (_previousNumPeopleInside != 0)
                {
                    _doorSound.Stop();
                    _doorSound.Play();
                    _previousNumPeopleInside = 0;
                }
                _slidingDoor.Position = R.Lerp(_slidingDoor.Position, _baselineDoorPosition, (float)delta + DoorMoveSpeed);
            }
            else
            {
                if (_previousNumPeopleInside != 1)
                {
                    _doorSound.Stop();
                    _doorSound.Play();
                    _previousNumPeopleInside = 1;
                }
                switch (SlidingDoorDirection)
                {
                    case DoorDirection.Left:
                        _slidingDoor.Position = R.Lerp(_slidingDoor.Position, _leftDoorPosition, (float)delta + DoorMoveSpeed);
                        break;
                    case DoorDirection.Right:
                        _slidingDoor.Position = R.Lerp(_slidingDoor.Position, _rightDoorPosition, (float)delta + DoorMoveSpeed);
                        break;
                }
            }
            _slidingDoor.SetCollider(_numPeopleInside > 0);
        }

        public void _on_Area_body_entered(Node aBody)
        {
            if (aBody is PlayerAPI)
            {
                _numPeopleInside++;
            }
        }
        public void _on_Area_body_exited(Node aBody)
        {
            if (aBody is PlayerAPI)
            {
                _numPeopleInside--;
            }
        }

    }
}