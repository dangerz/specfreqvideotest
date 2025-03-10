using Godot;
using System;
using System.Collections.Generic;

namespace SpecFreqCustomZone
{
    public partial class WalkThroughObject : StaticBody3D
    {
        [Export]
        public NodePath ModelPath;
        [Export]
        public float Transparency = 0.4f;

        private int _numPeopleInside;
        private MeshInstance3D _model;
        private Color _transparentColor;
        private static Color _opaqueColor = new Color(1, 1, 1, 1);
        public override void _Ready()
        {
            _model = GetNode<MeshInstance3D>(ModelPath);
            _model.MaterialOverride = (Material)_model.GetActiveMaterial(0).Duplicate();
            _model.SetSurfaceOverrideMaterial(0, null);
            _transparentColor = new Color(1, 1, 1, Transparency);
            _numPeopleInside = 0;
        }
        public override void _Process(double delta)
        {
            if (_numPeopleInside == 0)
            {
                _model.MaterialOverride.Set("albedo_color", _opaqueColor);
            }
            else
            {
                _model.MaterialOverride.Set("albedo_color", _transparentColor);
            }
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
                if (_numPeopleInside < 0)
                    _numPeopleInside = 0;
            }
        }
    }
}