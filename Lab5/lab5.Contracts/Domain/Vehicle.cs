// =============================================================================
// Файл: lab5.Contracts/Domain/Vehicle.cs
// Назначение: общая модель транспортных средств для host и плагинов.
// Иерархия: Vehicle → PassengerVehicle / CargoVehicle; VehicleTypeInfo — фабрика.
// =============================================================================

using System;

namespace lab5.Contracts.Domain
{
    /// <summary>Базовый класс любого ТС в приложении (встроенного или из плагина).</summary>
    [Serializable]
    public abstract class Vehicle
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; }

        /// <summary>Краткая строка для ListBox (реализуется в наследниках).</summary>
        public abstract string GetInfo();

        /// <summary>Редактирование общих полей через InputBox (пустая строка = не менять).</summary>
        public virtual void EditCommon(string? nameInput, string? yearInput)
        {
            if (!string.IsNullOrWhiteSpace(nameInput))
            {
                Name = nameInput;
            }

            if (int.TryParse(yearInput, out int year))
            {
                Year = year;
            }
        }

        /// <summary>Диалог редактирования конкретного типа ТС.</summary>
        public abstract void Edit();

        public override string ToString() => $"{GetType().Name} - {GetInfo()}";
    }

    /// <summary>Пассажирский транспорт: вместимость + общие поля Vehicle.</summary>
    [Serializable]
    public abstract class PassengerVehicle : Vehicle
    {
        public int PassengerCapacity { get; set; }

        public virtual void EditPassengerCapacity(string? capacityInput)
        {
            if (int.TryParse(capacityInput, out int capacity))
            {
                PassengerCapacity = capacity;
            }
        }
    }

    /// <summary>Грузовой транспорт: максимальная загрузка в кг.</summary>
    [Serializable]
    public abstract class CargoVehicle : Vehicle
    {
        public double MaxLoadKg { get; set; }

        public virtual void EditMaxLoad(string? loadInput)
        {
            if (double.TryParse(loadInput, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double load))
            {
                MaxLoadKg = load;
            }
        }
    }

    /// <summary>
    /// Описание типа в реестре (паттерн «Фабрика»): id, имя в UI и Func для создания экземпляра.
    /// </summary>
    public sealed class VehicleTypeInfo
    {
        public string Id { get; }
        public string DisplayName { get; }
        /// <summary>Откуда тип: Built-in или имя плагина.</summary>
        public string Source { get; }
        /// <summary>Фабричный метод: () => new Car() и т.д.</summary>
        public Func<Vehicle> Factory { get; }

        public VehicleTypeInfo(string id, string displayName, Func<Vehicle> factory, string source = "Built-in")
        {
            Id = id;
            DisplayName = displayName;
            Factory = factory;
            Source = source;
        }

        public override string ToString() => DisplayName;
    }
}
