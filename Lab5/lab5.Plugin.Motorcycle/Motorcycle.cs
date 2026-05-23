// =============================================================================
// Файл: Motorcycle.cs — класс ТС из плагина (отдельная сборка lab5.Plugin.Motorcycle.dll).
// =============================================================================

using System;
using lab5.Contracts.Domain;
using Microsoft.VisualBasic;

namespace lab5.Plugin.Motorcycle
{
    [Serializable]
    public class Motorcycle : PassengerVehicle
    {
        public bool HasSidecar { get; set; }

        public override string GetInfo()
        {
            string sidecar = HasSidecar ? "yes" : "no";
            return $"{Name}, year {Year}, seats {PassengerCapacity}, sidecar: {sidecar}";
        }

        public override void Edit()
        {
            EditCommon(
                Interaction.InputBox("Enter new name (leave empty to keep current):", "Edit vehicle", Name),
                Interaction.InputBox("Enter production year (leave empty to keep current):", "Edit vehicle",
                    Year == 0 ? string.Empty : Year.ToString()));
            EditPassengerCapacity(Interaction.InputBox(
                "Enter passenger capacity (leave empty to keep current):", "Edit motorcycle",
                PassengerCapacity == 0 ? string.Empty : PassengerCapacity.ToString()));

            string sidecarInput = Interaction.InputBox(
                "Has sidecar? (y/n, leave empty to keep current):", "Edit motorcycle",
                HasSidecar ? "y" : "n");
            if (string.Equals(sidecarInput, "y", StringComparison.OrdinalIgnoreCase))
                HasSidecar = true;
            else if (string.Equals(sidecarInput, "n", StringComparison.OrdinalIgnoreCase))
                HasSidecar = false;
        }
    }
}
