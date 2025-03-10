using Godot;
using System;
using System.Collections.Generic;

namespace SpecFreqCustomZone
{
    public partial class LightPost : StaticBody3D
    {
        [Export]
        public Godot.Collections.Array<NodePath> LightPaths = new Godot.Collections.Array<NodePath>();
        [Export]
        public float LightOnEnergy = 15;

        private List<SpotLight3D> _lights;
        private List<int> _onHours;
        private List<int> _offHours;
        public override void _Ready()
        {
            _onHours = new List<int>();
            _onHours.Add(0);
            _onHours.Add(1);
            _onHours.Add(2);
            _onHours.Add(3);
            _onHours.Add(4);
            _onHours.Add(5);
            _onHours.Add(6);
            _onHours.Add(7);
            _onHours.Add(17);
            _onHours.Add(18);
            _onHours.Add(19);
            _onHours.Add(20);
            _onHours.Add(21);
            _onHours.Add(22);
            _onHours.Add(23);

            _offHours = new List<int>();
            _offHours.Add(8);
            _offHours.Add(9);
            _offHours.Add(10);
            _offHours.Add(11);
            _offHours.Add(12);
            _offHours.Add(13);
            _offHours.Add(14);
            _offHours.Add(15);
            _offHours.Add(16);

            _lights = new List<SpotLight3D>();
            foreach (NodePath path in LightPaths)
            {
                SpotLight3D light = GetNodeOrNull<SpotLight3D>(path);
                if (light != null)
                {
                    _lights.Add(light);
                }
            }

            SpecFreqAPI.RegisterTimeTrigger(On, _onHours);
            SpecFreqAPI.RegisterTimeTrigger(Off, _offHours);
        }

        public void On()
        {
            foreach (SpotLight3D light in _lights)
            {
                light.LightEnergy = LightOnEnergy;
            }
        }
        public void Off()
        {
            foreach (SpotLight3D light in _lights)
            {
                if (light != null)
                    light.LightEnergy = 0;
            }
        }
    }
}