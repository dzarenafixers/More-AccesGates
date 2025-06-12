using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;

namespace ContainmentDoors
{
    public class Config : IConfig
    {
        [Description("Load the plugin")]
        public bool IsEnabled { get; set; } = true;

        [Description( "False Debug mode")]
        public bool Debug { get; set; } = false;
        
        [Description("Doors Acces (Like GateA, Scp173Gate...)")]
        public List<DoorType> DoorTypes { get; set; } = new()
        {
            DoorType.GateA,
            DoorType.Scp173Armory,
            DoorType.Scp173Gate
        };

        [Description("نوع الكارت المطلوب لفتح هذه الأبواب")]
        public KeycardPermissions RequiredKeycard { get; set; } = KeycardPermissions.ContainmentLevelTwo;
    }
}

    
