using Godot;
using System;

public partial class Zone : CustomZone
{
    public override void _Ready()
    {
        
    }
    public Zone() { }
    public override void _ZoneSetup()
    {
        SpecFreqAPI.Init(); // This must be the first line.  Do not forget it or else bad things will happen (an exception will be thrown).
        SpecFreqCustomZone.R.Init();

        GD.Print("Your Zone _ZoneSetup()");

        SpecFreqAPI.SceneKey = "yz"; // this must match what's in your config file
        SpecFreqAPI.UpperSpecLimit = new Vector3(200, 0, 200);
        SpecFreqAPI.LowerSpecLimit = new Vector3(-200, 0, -200);
        SpecFreqAPI.CloudsEnabled = false;
        SpecFreqAPI.MiniMapLocation = "res://YourZone/Graphics/DZMiniMap.png";
        SpecFreqAPI.WorldSize = new Vector3(400, 0, 400);
        SpecFreqAPI.MapSize = new Vector3(400, 0, 400);

        CharacterAPI infantryCharacter = new CharacterAPI(SpecFreqAPIEnums.CharacterTypes.BuiltIn_Infantry, "Infantry", "Light duty infantry character");
        infantryCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.HandToHand);
        infantryCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.HandToHand);
        infantryCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.Light);
        infantryCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.Light);
        infantryCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.Heavy);
        infantryCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.LightBuild);
        SpecFreqAPI.AddCharacter(infantryCharacter);

        CharacterAPI medicCharacter = new CharacterAPI(SpecFreqAPIEnums.CharacterTypes.BuiltIn_Medic, "Medic", "Healer of all injuries");
        medicCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.HandToHand);
        medicCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.Light);
        medicCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.Medic);
        medicCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.Medic);
        SpecFreqAPI.AddCharacter(medicCharacter);

        CharacterAPI engineerCharacter = new CharacterAPI(SpecFreqAPIEnums.CharacterTypes.BuiltIn_Engineer, "Engineer", "Builds Things");
        engineerCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.HandToHand);
        engineerCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.Light);
        engineerCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.LightBuild);
        engineerCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.LightBuild);
        engineerCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.HeavyBuild);
        engineerCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.HeavyBuild);
        SpecFreqAPI.AddCharacter(engineerCharacter);

        CharacterAPI sniperCharacter = new CharacterAPI(SpecFreqAPIEnums.CharacterTypes.BuiltIn_Sniper, "Sniper", "Shoots things from far away");
        sniperCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.HandToHand);
        sniperCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.Light);
        sniperCharacter.AddInventorySlot(SpecFreqAPIEnums.InventorySlotType.LongRange);
        SpecFreqAPI.AddCharacter(sniperCharacter);

        SpecFreqAPI.GameMode = SpecFreqAPIEnums.GameType.CaptureTheFlag;
    }
}
