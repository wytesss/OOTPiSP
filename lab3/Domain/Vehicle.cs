using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace lab3.Domain
{
    /// <summary>
    /// Represents base abstract vehicle with common properties and edit logic.
    /// </summary>
    [Serializable]
    public abstract class Vehicle
    {
        /// <summary>
        /// Unique identifier of the vehicle instance.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Human friendly vehicle name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Production year of the vehicle.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Returns short formatted description of the vehicle.
        /// </summary>
        public abstract string GetInfo();

        /// <summary>
        /// Allows user to edit common properties of the vehicle using input dialogs.
        /// </summary>
        public virtual void Edit()
        {
            // Edit name.
            string nameInput = Interaction.InputBox(
                "Enter new name (leave empty to keep current):",
                "Edit vehicle",
                Name);
            if (!string.IsNullOrWhiteSpace(nameInput))
            {
                Name = nameInput;
            }

            // Edit production year.
            string yearInput = Interaction.InputBox(
                "Enter production year (leave empty to keep current):",
                "Edit vehicle",
                Year == 0 ? string.Empty : Year.ToString());
            if (int.TryParse(yearInput, out int year))
            {
                Year = year;
            }
        }

        /// <summary>
        /// Returns string representation based on polymorphic GetInfo method.
        /// </summary>
        public override string ToString()
        {
            return $"{GetType().Name} - {GetInfo()}";
        }
    }

    /// <summary>
    /// Passenger vehicle base class that stores passenger capacity.
    /// </summary>
    [Serializable]
    public abstract class PassengerVehicle : Vehicle
    {
        /// <summary>
        /// Maximum number of passengers.
        /// </summary>
        public int PassengerCapacity { get; set; }

        /// <summary>
        /// Extends edit dialog with passenger capacity property.
        /// </summary>
        public override void Edit()
        {
            base.Edit();

            string capacityInput = Interaction.InputBox(
                "Enter passenger capacity (leave empty to keep current):",
                "Edit passenger vehicle",
                PassengerCapacity == 0 ? string.Empty : PassengerCapacity.ToString());
            if (int.TryParse(capacityInput, out int capacity))
            {
                PassengerCapacity = capacity;
            }
        }
    }

    /// <summary>
    /// Cargo vehicle base class that stores maximum load in kilograms.
    /// </summary>
    [Serializable]
    public abstract class CargoVehicle : Vehicle
    {
        /// <summary>
        /// Maximum allowed cargo load in kilograms.
        /// </summary>
        public double MaxLoadKg { get; set; }

        /// <summary>
        /// Extends edit dialog with maximum load property.
        /// </summary>
        public override void Edit()
        {
            base.Edit();

            string loadInput = Interaction.InputBox(
                "Enter max load (kg) (leave empty to keep current):",
                "Edit cargo vehicle",
                MaxLoadKg == 0 ? string.Empty : MaxLoadKg.ToString());
            if (double.TryParse(loadInput, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double load))
            {
                MaxLoadKg = load;
            }
        }
    }

    /// <summary>
    /// Stores information about available vehicle type and its factory.
    /// </summary>
    public sealed class VehicleTypeInfo
    {
        /// <summary>
        /// Internal identifier of the vehicle type.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Text that is displayed in the user interface.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Factory delegate that creates new vehicle instance.
        /// </summary>
        public Func<Vehicle> Factory { get; }

        /// <summary>
        /// Initializes new instance of vehicle type info.
        /// </summary>
        public VehicleTypeInfo(string id, string displayName, Func<Vehicle> factory)
        {
            Id = id;
            DisplayName = displayName;
            Factory = factory;
        }

        /// <summary>
        /// Returns user friendly display name.
        /// </summary>
        public override string ToString() => DisplayName;
    }

    /// <summary>
    /// Global registry that stores all available vehicle types.
    /// </summary>
    public static class VehicleTypeRegistry
    {
        private static readonly List<VehicleTypeInfo> _types = new();

        /// <summary>
        /// Static constructor registers default vehicle types.
        /// </summary>
        static VehicleTypeRegistry()
        {
            // Register built in vehicle types.
            Register("car", "Car", () => new Car());
            Register("bus", "Bus", () => new Bus());
            Register("truck", "Truck", () => new Truck());
            Register("van", "Van", () => new Van());
        }

        /// <summary>
        /// Returns read only collection of all registered vehicle types.
        /// </summary>
        public static IReadOnlyList<VehicleTypeInfo> Types => _types.AsReadOnly();

        /// <summary>
        /// Registers new vehicle type in registry.
        /// </summary>
        public static void Register(string id, string displayName, Func<Vehicle> factory)
        {
            _types.Add(new VehicleTypeInfo(id, displayName, factory));
        }
    }

    /// <summary>
    /// Concrete passenger car implementation.
    /// </summary>
    [Serializable]
    public class Car : PassengerVehicle
    {
        /// <summary>
        /// Indicates if car has air conditioning.
        /// </summary>
        public bool HasAirConditioning { get; set; }

        /// <summary>
        /// Returns formatted description string of car.
        /// </summary>
        public override string GetInfo()
        {
            string ac = HasAirConditioning ? "yes" : "no";
            return $"{Name}, year {Year}, seats {PassengerCapacity}, AC: {ac}";
        }

        /// <summary>
        /// Extends edit dialog with air conditioning flag.
        /// </summary>
        public override void Edit()
        {
            base.Edit();

            string acInput = Interaction.InputBox(
                "Has air conditioning? (y/n, leave empty to keep current):",
                "Edit car",
                HasAirConditioning ? "y" : "n");
            if (string.Equals(acInput, "y", StringComparison.OrdinalIgnoreCase))
            {
                HasAirConditioning = true;
            }
            else if (string.Equals(acInput, "n", StringComparison.OrdinalIgnoreCase))
            {
                HasAirConditioning = false;
            }
        }
    }

    /// <summary>
    /// Concrete bus implementation.
    /// </summary>
    [Serializable]
    public class Bus : PassengerVehicle
    {
        /// <summary>
        /// Indicates if bus is a city bus.
        /// </summary>
        public bool IsCityBus { get; set; }

        /// <summary>
        /// Returns formatted description string of bus.
        /// </summary>
        public override string GetInfo()
        {
            string type = IsCityBus ? "city" : "intercity";
            return $"{Name}, year {Year}, seats {PassengerCapacity}, type: {type}";
        }

        /// <summary>
        /// Extends edit dialog with city bus flag.
        /// </summary>
        public override void Edit()
        {
            base.Edit();

            string cityInput = Interaction.InputBox(
                "Is city bus? (y/n, leave empty to keep current):",
                "Edit bus",
                IsCityBus ? "y" : "n");
            if (string.Equals(cityInput, "y", StringComparison.OrdinalIgnoreCase))
            {
                IsCityBus = true;
            }
            else if (string.Equals(cityInput, "n", StringComparison.OrdinalIgnoreCase))
            {
                IsCityBus = false;
            }
        }
    }

    /// <summary>
    /// Concrete truck implementation.
    /// </summary>
    [Serializable]
    public class Truck : CargoVehicle
    {
        /// <summary>
        /// Number of axles.
        /// </summary>
        public int Axles { get; set; }

        /// <summary>
        /// Returns formatted description string of truck.
        /// </summary>
        public override string GetInfo()
        {
            return $"{Name}, year {Year}, max load {MaxLoadKg} kg, axles {Axles}";
        }

        /// <summary>
        /// Extends edit dialog with number of axles.
        /// </summary>
        public override void Edit()
        {
            base.Edit();

            string axlesInput = Interaction.InputBox(
                "Enter number of axles (leave empty to keep current):",
                "Edit truck",
                Axles == 0 ? string.Empty : Axles.ToString());
            if (int.TryParse(axlesInput, out int axles))
            {
                Axles = axles;
            }
        }
    }

    /// <summary>
    /// Concrete van implementation.
    /// </summary>
    [Serializable]
    public class Van : CargoVehicle
    {
        /// <summary>
        /// Indicates if van has refrigeration equipment.
        /// </summary>
        public bool Refrigerated { get; set; }

        /// <summary>
        /// Returns formatted description string of van.
        /// </summary>
        public override string GetInfo()
        {
            string refText = Refrigerated ? "yes" : "no";
            return $"{Name}, year {Year}, max load {MaxLoadKg} kg, refrigerated: {refText}";
        }

        /// <summary>
        /// Extends edit dialog with refrigerated flag.
        /// </summary>
        public override void Edit()
        {
            base.Edit();

            string refInput = Interaction.InputBox(
                "Is refrigerated? (y/n, leave empty to keep current):",
                "Edit van",
                Refrigerated ? "y" : "n");
            if (string.Equals(refInput, "y", StringComparison.OrdinalIgnoreCase))
            {
                Refrigerated = true;
            }
            else if (string.Equals(refInput, "n", StringComparison.OrdinalIgnoreCase))
            {
                Refrigerated = false;
            }
        }
    }
}

