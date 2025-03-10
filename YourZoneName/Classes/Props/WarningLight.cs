using Godot;
using System;

namespace SpecFreqCustomZone
{
    public partial class WarningLight : StaticBody3D
    {
        [Export]
        public string LightColor = "#ff0000";
        [Export]
        public int LightEnergy = 3;
        public override void _Ready()
        {
            OmniLight3D light = GetNode<OmniLight3D>("Light");
            light.LightColor = new Color(LightColor);
            light.LightEnergy = LightEnergy;
        }
    }
}