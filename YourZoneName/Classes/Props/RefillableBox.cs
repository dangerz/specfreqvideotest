using Godot;
using System;

namespace SpecFreqCustomZone
{
    public partial class RefillableBox : StaticBody3D
    {
        [Export]
        public SpecFreqAPIEnums.ProjectileTypes ProjectileType = SpecFreqAPIEnums.ProjectileTypes.Bullet;
        [Export]
        public int RefillAmount = 0;
        [Export]
        public int MoveSpeed = 2;
        [Export]
        public int OpenLid = -46;
        [Export]
        public int ClosedLid = 0;
        [Export]
        public int LightEnergy = 2;

        private OmniLight3D _light;
        private Node3D _lid;
        private int _numPeopleInside;
        private Vector3 _lidRotation;

        public override void _Ready()
        {
            _light = GetNode<OmniLight3D>("Light");
            _lid = GetNode<Node3D>("Lid");
            _lidRotation = new Vector3(0, 0, 0);
            _numPeopleInside = 0;
            if (RefillAmount == 0)
                _light.LightEnergy = 0;
            else
                _light.LightEnergy = LightEnergy;
        }

        public override void _Process(double delta)
        {
            _lidRotation = _lid.RotationDegrees;
            if (_numPeopleInside == 0)
            {
                if (_lidRotation.X < ClosedLid)
                    _lidRotation.X += MoveSpeed;
            }
            else
            {
                if (_lidRotation.X > OpenLid)
                    _lidRotation.X -= MoveSpeed;
            }

            _lid.RotationDegrees = _lidRotation;
        }

        public void _on_Area_body_entered(Node aBody)
        {
            if (aBody is PlayerAPI)
            {
                if (RefillAmount > 0)
                {
                    _numPeopleInside++;
                    int playerId = -1;
                    if (Int32.TryParse(aBody.Name, out playerId))
                    {
                        if (SpecFreqAPI.IsLocalPlayer(playerId))
                        {
                            SpecFreqAPI.AddAmmoToPlayer(ProjectileType, RefillAmount);
                        }
                    }
                }
            }
        }
        public void _on_Area_body_exited(Node aBody)
        {
            if (aBody is PlayerAPI)
            {
                if (_numPeopleInside > 0)
                    _numPeopleInside--;
            }
        }

    }
}