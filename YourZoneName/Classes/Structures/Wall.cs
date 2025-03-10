using Godot;
using System;

namespace SpecFreqCustomZone
{
    public partial class Wall : StaticBody3D
    {
        [Export]
        public bool CastShadow = true;
        [Export]
        public NodePath Mesh;

        private MeshInstance3D _mesh;
        public override void _Ready()
        {
            _mesh = GetNode<MeshInstance3D>(Mesh);
            _mesh.CastShadow = CastShadow ? GeometryInstance3D.ShadowCastingSetting.On : GeometryInstance3D.ShadowCastingSetting.Off;
            SetProcess(false);
        }
    }
}