// =============================================================================
// Файл: BuiltInVehicles.cs — встроенные классы ТС (не из DLL-плагина).
// Редактирование через Microsoft.VisualBasic Interaction.InputBox.
// =============================================================================

using System;
using lab4.Contracts.Domain;
using Microsoft.VisualBasic;

namespace lab4.Domain
{
    // --- Встроенный тип: легковой автомобиль ---
    /// <summary>Легковой автомобиль: пассажирский + кондиционер.</summary>
    [Serializable]
    public class Car : PassengerVehicle
    {
        public bool HasAirConditioning { get; set; }

        public override string GetInfo()
        {
            string ac = HasAirConditioning ? "yes" : "no";
            return $"{Name}, year {Year}, seats {PassengerCapacity}, AC: {ac}";
        }

        public override void Edit()
        {
            EditCommon(
                Interaction.InputBox("Enter new name (leave empty to keep current):", "Edit vehicle", Name),
                Interaction.InputBox("Enter production year (leave empty to keep current):", "Edit vehicle",
                    Year == 0 ? string.Empty : Year.ToString()));
            EditPassengerCapacity(Interaction.InputBox(
                "Enter passenger capacity (leave empty to keep current):", "Edit passenger vehicle",
                PassengerCapacity == 0 ? string.Empty : PassengerCapacity.ToString()));

            string acInput = Interaction.InputBox(
                "Has air conditioning? (y/n, leave empty to keep current):", "Edit car",
                HasAirConditioning ? "y" : "n");
            if (string.Equals(acInput, "y", StringComparison.OrdinalIgnoreCase))
                HasAirConditioning = true;
            else if (string.Equals(acInput, "n", StringComparison.OrdinalIgnoreCase))
                HasAirConditioning = false;
        }
    }

    // --- Встроенный тип: автобус ---
    [Serializable]
    public class Bus : PassengerVehicle
    {
        public bool IsCityBus { get; set; }

        public override string GetInfo()
        {
            string type = IsCityBus ? "city" : "intercity";
            return $"{Name}, year {Year}, seats {PassengerCapacity}, type: {type}";
        }

        public override void Edit()
        {
            EditCommon(
                Interaction.InputBox("Enter new name (leave empty to keep current):", "Edit vehicle", Name),
                Interaction.InputBox("Enter production year (leave empty to keep current):", "Edit vehicle",
                    Year == 0 ? string.Empty : Year.ToString()));
            EditPassengerCapacity(Interaction.InputBox(
                "Enter passenger capacity (leave empty to keep current):", "Edit passenger vehicle",
                PassengerCapacity == 0 ? string.Empty : PassengerCapacity.ToString()));

            string cityInput = Interaction.InputBox(
                "Is city bus? (y/n, leave empty to keep current):", "Edit bus", IsCityBus ? "y" : "n");
            if (string.Equals(cityInput, "y", StringComparison.OrdinalIgnoreCase))
                IsCityBus = true;
            else if (string.Equals(cityInput, "n", StringComparison.OrdinalIgnoreCase))
                IsCityBus = false;
        }
    }

    // --- Встроенный тип: грузовик ---
    [Serializable]
    public class Truck : CargoVehicle
    {
        public int Axles { get; set; }

        public override string GetInfo() =>
            $"{Name}, year {Year}, max load {MaxLoadKg} kg, axles {Axles}";

        public override void Edit()
        {
            EditCommon(
                Interaction.InputBox("Enter new name (leave empty to keep current):", "Edit vehicle", Name),
                Interaction.InputBox("Enter production year (leave empty to keep current):", "Edit vehicle",
                    Year == 0 ? string.Empty : Year.ToString()));
            EditMaxLoad(Interaction.InputBox(
                "Enter max load (kg) (leave empty to keep current):", "Edit cargo vehicle",
                MaxLoadKg == 0 ? string.Empty : MaxLoadKg.ToString(System.Globalization.CultureInfo.InvariantCulture)));

            string axlesInput = Interaction.InputBox(
                "Enter number of axles (leave empty to keep current):", "Edit truck",
                Axles == 0 ? string.Empty : Axles.ToString());
            if (int.TryParse(axlesInput, out int axles))
                Axles = axles;
        }
    }

    // --- Встроенный тип: фургон ---
    [Serializable]
    public class Van : CargoVehicle
    {
        public bool Refrigerated { get; set; }

        public override string GetInfo()
        {
            string refText = Refrigerated ? "yes" : "no";
            return $"{Name}, year {Year}, max load {MaxLoadKg} kg, refrigerated: {refText}";
        }

        public override void Edit()
        {
            EditCommon(
                Interaction.InputBox("Enter new name (leave empty to keep current):", "Edit vehicle", Name),
                Interaction.InputBox("Enter production year (leave empty to keep current):", "Edit vehicle",
                    Year == 0 ? string.Empty : Year.ToString()));
            EditMaxLoad(Interaction.InputBox(
                "Enter max load (kg) (leave empty to keep current):", "Edit cargo vehicle",
                MaxLoadKg == 0 ? string.Empty : MaxLoadKg.ToString(System.Globalization.CultureInfo.InvariantCulture)));

            string refInput = Interaction.InputBox(
                "Is refrigerated? (y/n, leave empty to keep current):", "Edit van", Refrigerated ? "y" : "n");
            if (string.Equals(refInput, "y", StringComparison.OrdinalIgnoreCase))
                Refrigerated = true;
            else if (string.Equals(refInput, "n", StringComparison.OrdinalIgnoreCase))
                Refrigerated = false;
        }
    }
}
