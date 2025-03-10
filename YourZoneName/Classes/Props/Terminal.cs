using Godot;
using System;

namespace SpecFreqCustomZone
{
    public partial class Terminal : StaticBody3D
    {
        [Export]
        public SpecFreqAPIEnums.TerminalTypes TerminalType = SpecFreqAPIEnums.TerminalTypes.Broken;
        [Export]
        public string TerminalText = "";

        private bool _playerInside;

        public override void _Ready()
        {
            _playerInside = false;
            SetProcess(false);
        }
        public override void _Process(double delta)
        {
            if (_playerInside)
            {
                if (Input.IsActionJustPressed(SpecFreqAPIInputNames.Action))
                {
                    if (TerminalType == SpecFreqAPIEnums.TerminalTypes.Broken)
                    {
                        SpecFreqAPI.AddMessageToChatWindow("Terminal Broken");
                    }
                    else
                    {
                        SpecFreqAPI.TriggerTerminalDisplay(TerminalType);
                    }
                }
            }
        }
        public void _on_InteractiveArea_body_entered(Node aBody)
        {
            int playerId = -1;
            if (Int32.TryParse(aBody.Name, out playerId))
            {
                if (SpecFreqAPI.IsLocalPlayer(playerId))
                {
                    if (TerminalText.Length > 0)
                        SpecFreqAPI.TerminalSetLabel(TerminalType.ToString(), true);

                    _playerInside = true;
                    SetProcess(true);
                }
            }
        }
        public void _on_InteractiveArea_body_exited(Node aBody)
        {
            int playerId = -1;
            if (Int32.TryParse(aBody.Name, out playerId))
            {
                if (SpecFreqAPI.IsLocalPlayer(playerId))
                {
                    if (TerminalText.Length > 0)
                        SpecFreqAPI.TerminalSetLabel("", false);

                    _playerInside = false;
                    SetProcess(false);
                }
            }
        }
    }
}