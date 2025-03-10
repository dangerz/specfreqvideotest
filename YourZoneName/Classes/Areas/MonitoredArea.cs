using Godot;
using System;

namespace SpecFreqCustomZone
{
    public partial class MonitoredArea : Area3D
    {
        [Export]
        public SpecFreqAPIEnums.AreaTypes AreaType;
        [Export]
        public string LabelName = "";

        public override void _Ready()
        {
            if (AreaType == SpecFreqAPIEnums.AreaTypes.NoFire)
                SpecFreqAPI.RegisterNoFireArea(this);
        }

        public void _on_Area_body_entered(Node aBody)
        {
            int playerId = -1;
            if (Int32.TryParse(aBody.Name, out playerId))
            {
                if (SpecFreqAPI.IsLocalPlayer(playerId))
                {
                    SpecFreqAPI.RegisterAreaTrigger(AreaType, LabelName, true);
                }
            }
        }
        public void _on_Area_body_exited(Node aBody)
        {
            int playerId = -1;
            if (Int32.TryParse(aBody.Name, out playerId))
            {
                if (SpecFreqAPI.IsLocalPlayer(playerId))
                {
                    SpecFreqAPI.RegisterAreaTrigger(AreaType, LabelName, false);
                }
            }
        }
    }
}