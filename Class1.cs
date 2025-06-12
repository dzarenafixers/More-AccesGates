using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Exiled.Loader;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Doors;
using Interactables.Interobjects.DoorUtils;
using MEC;
using UnityEngine;

namespace ContainmentDoors
{
    public class ContainmentDoors : Plugin<Config>
    {
        public override string Name => "Containment Doors";
        public override string Author => "Moncef50g";
        public override Version Version { get; } = new(1, 0, 0);

        private readonly HashSet<Door> _modifiedDoors = new();

        public override void OnEnabled()
        {
            ModifyDoors();

            Exiled.Events.Handlers.Map.Generated += OnMapGenerated;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            RestoreDoors();

            Exiled.Events.Handlers.Map.Generated -= OnMapGenerated;

            base.OnDisabled();
        }

        private void OnMapGenerated()
        {
            Timing.CallDelayed(1f, () =>
            {
                ModifyDoors();
            });
        }

        private void ModifyDoors()
        {
            foreach (var door in Door.List)
            {
                if (new[] {
                    DoorType.Scp049Gate,
                    DoorType.Scp173Gate,
                    DoorType.Scp173NewGate
                }.Contains(door.Type))
                {
                    door.RequiredPermissions = (DoorPermissionFlags)KeycardPermissions.ContainmentLevelTwo;

                    Log.Info($"Modified door: {door.Name} ({door.Type}) to require ContainmentLevelTwo");

                    _modifiedDoors.Add(door);
                }
            }
        }

        private void RestoreDoors()
        {
            foreach (var door in _modifiedDoors)
            {
                door.RequiredPermissions = (DoorPermissionFlags)KeycardPermissions.None;
            }

            _modifiedDoors.Clear();
        }
    }
}