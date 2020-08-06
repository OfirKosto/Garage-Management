using System;
using System.Collections.Generic;

namespace GarageManger.GarageLogic
{
    public class Garage
    {
        // Date members:
        private Dictionary<int, VehicleInGarage> m_VehicelsList = new Dictionary<int, VehicleInGarage>();

        // Methods:
        public void AddVehical(VehicleInGarage i_VehicalToAdd)
        {
            m_VehicelsList.Add(i_VehicalToAdd.GetHashCode(), i_VehicalToAdd);
        }

        public bool IsVehicleInGarage(string i_LicensePlateToCheck)
        {
            return m_VehicelsList.ContainsKey(i_LicensePlateToCheck.GetHashCode());
        }

        private VehicleInGarage FindAndReturnVehicleInGarage(string i_LicensePlate)
        {
            VehicleInGarage vehicleToReturn;

            if (!m_VehicelsList.TryGetValue(i_LicensePlate.GetHashCode(), out vehicleToReturn)) // vehicle is not in the garage
            {
                throw new ArgumentException(string.Format("There is no vehicle with {0} license plate in the garage.", i_LicensePlate));
            }

            return vehicleToReturn;
        }

        public void ChangeVehicleStatus(string i_LicensePlate, VehicleInGarage.eStatus i_NewStatus)
        {
            VehicleInGarage vehicleInGarageToUpdate = FindAndReturnVehicleInGarage(i_LicensePlate);
            vehicleInGarageToUpdate.Status = i_NewStatus;
        }

        public List<string> ListOfVehicleLicensePlatesInGarageByStatus(VehicleInGarage.eStatus i_Status)
        {
            List<string> LicensePlateList = new List<string>();

            foreach (KeyValuePair<int, VehicleInGarage> vehicleToCheck in m_VehicelsList)
            {
                if (vehicleToCheck.Value.Status == i_Status)
                {
                    LicensePlateList.Add(vehicleToCheck.Value.Vehicle.LicensePlate);
                }
            }

            return LicensePlateList;
        }

        public void InflateWheelsToMax(string i_LicensePlate)
        {
            VehicleInGarage vehicleInGarageToUpdate = FindAndReturnVehicleInGarage(i_LicensePlate);
            Wheel[] VehicleWheelsToInflate = vehicleInGarageToUpdate.Vehicle.Wheels;

            for (int i = 0; i < VehicleWheelsToInflate.Length; i++)
            {
                VehicleWheelsToInflate[i].Inflate(VehicleWheelsToInflate[i].MaxAirPressure - VehicleWheelsToInflate[i].CurrentAirPressure);
            }
        }

        public void RefuelGasVehicle(string i_LicensePlate, GasEngine.eFuelType i_FuelType, float i_AmountOfFuelToAdd)
        {
            VehicleInGarage vehicleInGarageToUpdate = FindAndReturnVehicleInGarage(i_LicensePlate);

            if(vehicleInGarageToUpdate.Vehicle.Engine is GasEngine)
            {
                ((GasEngine)vehicleInGarageToUpdate.Vehicle.Engine).Refueling(i_AmountOfFuelToAdd, i_FuelType);
                vehicleInGarageToUpdate.Vehicle.UpdateEnergyPercentage();
            }
            else
            {
                throw new ArgumentException("This is not a fuel based vehicle.");
            }
        }

        public void ChargeElectricVehicle(string i_LicensePlate, float i_MinutesToCharge)
        {
            VehicleInGarage vehicleInGarageToUpdate = FindAndReturnVehicleInGarage(i_LicensePlate);

            if (vehicleInGarageToUpdate.Vehicle.Engine is ElecrticEngine)
            {
                ((ElecrticEngine)vehicleInGarageToUpdate.Vehicle.Engine).ChargeBattery(i_MinutesToCharge / 60);
                vehicleInGarageToUpdate.Vehicle.UpdateEnergyPercentage();
            }
            else
            {
                throw new ArgumentException("This is not an Electric vehicle.");
            }
        }

        public string GetVehicleInfo(string i_LicensePlate)
        {
            VehicleInGarage vehicleInGarageToShow = FindAndReturnVehicleInGarage(i_LicensePlate);
            return vehicleInGarageToShow.ToString();
        }

    }
}
