using System;
using System.Text;

namespace GarageManger.GarageLogic
{
    public class Truck : Vehicle
    {
        // Data memebrs:
        private const int k_NumberOfTruckWheels = 16;
        private const int k_TruckMaxPsi = 28;
        private bool m_IsCarringHazardousMaterial;
        private float m_CargoCapacity;

        public Truck(string i_ModelName, string i_LicensePlate, float i_CurrentAirPressure, string i_ProducerName) : base(i_ModelName, i_LicensePlate)
        {
            setWheels(k_NumberOfTruckWheels, k_TruckMaxPsi, i_CurrentAirPressure, i_ProducerName);
        }

        // Getters/Setters:
        public bool IsCarringHazardousMaterial
        {
            get
            {
                return m_IsCarringHazardousMaterial;
            }
            set
            {
                m_IsCarringHazardousMaterial = value;
            }
        }

        public float CargoCapacity
        {
            get
            {
                return m_CargoCapacity;
            }
            set
            {
                m_CargoCapacity = value;
            }
        }

        // methods:
        public override string ToString()
        {
            return string.Format(
@"Vehicle type:Truck
License plate:{0}
Model name:{1}
The truck {2} carring hazardous material.
Cargo capacity:{3} 
Engine details:
{4}
Wheels details:
{5}", r_LicensePlate, r_ModelName, (m_IsCarringHazardousMaterial ? "is":"is not"), m_CargoCapacity, m_Engine.ToString(), getWheelsInfo());
        }
    }
}
