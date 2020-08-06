using System;

namespace GarageManger.GarageLogic
{
    public class VehicleCreator
    {
        // Gas engine vehicles const's:
        private const float k_CarFuelCapacity = 60;
        private const float k_MotorcycleFuelCapacity = 7;
        private const float k_TruckFuelCapacity = 120;
        // Electric engine vehicles const's:
        private const float k_CarBatteryCapacity = 2.1f;
        private const float k_MotorcycleBatteryCapacity = 1.2f;

        public static Vehicle CreateVehicle(eVehicleType i_VehicleToCreateType, string i_ModelName, string i_LicensePlate, float i_CurrentAirPressure, string i_ProducerName)
        {
            Vehicle vehicleToReturn = null;

            switch(i_VehicleToCreateType)
            {
                case eVehicleType.GasCar:
                {
                    vehicleToReturn = new Car(i_ModelName, i_LicensePlate, i_CurrentAirPressure, i_ProducerName);
                    vehicleToReturn.Engine = new GasEngine(k_CarFuelCapacity, GasEngine.eFuelType.Octan96);
                    break;
                }
                case eVehicleType.ElectricCar:
                {
                    vehicleToReturn = new Car(i_ModelName, i_LicensePlate, i_CurrentAirPressure, i_ProducerName);
                    vehicleToReturn.Engine = new ElecrticEngine(k_CarBatteryCapacity);
                    break;
                }
                case eVehicleType.GasMotorcycle:
                {
                    vehicleToReturn = new Motorcycle(i_ModelName, i_LicensePlate, i_CurrentAirPressure, i_ProducerName);
                    vehicleToReturn.Engine = new GasEngine(k_MotorcycleFuelCapacity, GasEngine.eFuelType.Octan95);
                    break;
                }
                case eVehicleType.ElectricMotorcycle:
                {
                    vehicleToReturn = new Motorcycle(i_ModelName, i_LicensePlate, i_CurrentAirPressure, i_ProducerName);
                    vehicleToReturn.Engine = new ElecrticEngine(k_MotorcycleBatteryCapacity);
                    break;
                }
                case eVehicleType.Truck:
                {
                    vehicleToReturn = new Truck(i_ModelName, i_LicensePlate, i_CurrentAirPressure, i_ProducerName);
                    vehicleToReturn.Engine = new GasEngine(k_TruckFuelCapacity, GasEngine.eFuelType.Soler);
                    break;
                }
            }

            return vehicleToReturn;
        }

        //Nested enums:
        public enum eVehicleType
        {
            GasCar = 1,
            ElectricCar,
            GasMotorcycle,
            ElectricMotorcycle,
            Truck,
        }
    }
}
