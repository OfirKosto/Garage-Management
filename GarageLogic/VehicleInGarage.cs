using System;
using System.Text;

namespace GarageManger.GarageLogic
{
    public class VehicleInGarage
    {
        // Date members:
        private readonly VehicleOwner r_Owner;
        private readonly Vehicle r_Vehicle;
        private eStatus m_Status = eStatus.InRepair;

        public VehicleInGarage(VehicleOwner i_Owner, Vehicle i_Vehicle)
        {
            r_Owner = i_Owner;
            r_Vehicle = i_Vehicle;
        }

        // getters/setters:
        public VehicleOwner Owner
        {
            get
            {
                return r_Owner;
            }
        }

        public eStatus Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        // methods:
        public override bool Equals(object obj)
        {
            bool isEqual = false;
            VehicleInGarage vehicleInGarageToCompare = obj as VehicleInGarage;

            if (vehicleInGarageToCompare != null)
            {
                isEqual = this.GetHashCode() == obj.GetHashCode();
            }

            return isEqual;
        }

        public override int GetHashCode() 
        {
            return r_Vehicle.LicensePlate.GetHashCode(); // uniqe license number
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
Vehicle status:{1}
{2}", r_Owner.ToString(), Enum.GetName(typeof(eStatus), m_Status), r_Vehicle.ToString());

        }

        // Nested enums
        public enum eStatus
        {
            InRepair = 1,
            Repaired,
            Paid,
        }
    }
}
