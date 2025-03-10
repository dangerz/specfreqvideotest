using Godot;
using System;

namespace SpecFreqCustomZone
{
    public partial class DropshipTeleporter : StaticBody3D
    {
        [Export]
        public bool TeleporterEnabled = true;
        [Export]
        public string TeleportLocationName = "Teleport Location Area";
        [Export]
        public Vector3 TeleportLocation = new Vector3(0, 0, 0);
        [Export]
        public NodePath PlatformPath;
        [Export]
        public NodePath ParticlesPath;
        [Export]
        public int CountdownAmount = 5;
        [Export]
        public int TeleportDeviation = 1;

        private MeshInstance3D _elevatorPlatform;
        private GpuParticles3D _particles;
        private double _countDown;
        private string _labelText;

        private int _charactersInRampArea;
        private bool _playerInside;
        public override void _Ready()
        {
            _labelText = $"Teleporter to [b]{TeleportLocationName}[/b]";
            _playerInside = false;
            _charactersInRampArea = 0;
            if (PlatformPath != null)
                _elevatorPlatform = GetNode<MeshInstance3D>(PlatformPath);
            if (ParticlesPath != null)
                _particles = GetNode<GpuParticles3D>(ParticlesPath);

            SetProcess(false);
        }

        public override void _Process(double delta)
        {
            if (_playerInside)
            {
                if (TeleporterEnabled)
                {
                    if (SpecFreqAPI.IsTriggerEnabled(SpecFreqAPIEnums.Triggers.CHARACTER_SELECTED))
                    {
                        _countDown -= delta;

                        if (_countDown <= 0)
                        {
                            SpecFreqAPI.TeleportSetLabel(Name, "", false);
                            SpecFreqAPI.TeleportPlayerTo(new Vector3(TeleportLocation.X + R.GetRandomNumber(0, TeleportDeviation), 0, TeleportLocation.Z + R.GetRandomNumber(0, TeleportDeviation)));
                        }
                        else
                        {
                            SpecFreqAPI.TeleportSetLabel(Name, $"Teleporting to [b]{TeleportLocationName}[/b] in [b]{_countDown.ToString("F1")}[/b].  Leave Teleporter to cancel.", true);
                        }
                    }
                    else
                    {
                        SpecFreqAPI.TeleportSetLabel(Name, "[color=white]You must [b]Gear Up[/b] first![/color]", true);
                    }
                }
                else
                {
                    SpecFreqAPI.TeleportSetLabel(Name, $"[color=gray]Teleporter to {TeleportLocationName} [b]Disabled[/b][/color]", true);
                }
            }
        }
        public void _on_AnimationArea_body_entered(Node aBody)
        {
            if (aBody is PlayerAPI)
            {
                _charactersInRampArea++;
                if (TeleporterEnabled)
                {
                    if (_elevatorPlatform != null)
                        _elevatorPlatform.SetProcess(true);
                    if (_particles != null)
                        _particles.Emitting = true;
                }
                int playerId = -1;
                if (Int32.TryParse(aBody.Name, out playerId))
                {
                    if (SpecFreqAPI.IsLocalPlayer(playerId))
                    {
                        if (TeleporterEnabled)
                            SpecFreqAPI.TeleportSetLabel(Name, _labelText, true);
                        else
                            SpecFreqAPI.TeleportSetLabel(Name, "[color=gray]Teleporter [b]Disabled[/b][/color]", true);
                    }
                }
            }
        }
        public void _on_AnimationArea_body_exited(Node aBody)
        {
            if (aBody is PlayerAPI)
            {
                if (_charactersInRampArea > 0)
                    _charactersInRampArea--;

                if (_charactersInRampArea == 0)
                {
                    if (_elevatorPlatform != null)
                        _elevatorPlatform.SetProcess(false);
                    if (_particles != null)
                        _particles.Emitting = false;
                }

                int playerId = -1;
                if (Int32.TryParse(aBody.Name, out playerId))
                {
                    if (SpecFreqAPI.IsLocalPlayer(playerId))
                    {
                        SpecFreqAPI.TeleportSetLabel(Name, "", false);
                    }
                }
            }
        }
        public void _on_TeleportationArea_body_entered(Node aBody)
        {
            if (aBody is PlayerAPI)
            {
                int playerId = -1;
                if (Int32.TryParse(aBody.Name, out playerId))
                {
                    if (SpecFreqAPI.IsLocalPlayer(playerId))
                    {
                        SpecFreqAPI.TeleportSetLabel(Name, _labelText, true);
                        _playerInside = true;
                        _countDown = CountdownAmount;
                        SetProcess(true);
                    }
                }
            }
        }
        public void _on_TeleportationArea_body_exited(Node aBody)
        {
            if (aBody is PlayerAPI)
            {
                int playerId = -1;
                if (Int32.TryParse(aBody.Name, out playerId))
                {
                    if (SpecFreqAPI.IsLocalPlayer(playerId))
                    {
                        SpecFreqAPI.TeleportSetLabel(Name, _labelText, false);
                        SetProcess(false);
                        _playerInside = false;
                    }
                }
            }
        }
    }
}