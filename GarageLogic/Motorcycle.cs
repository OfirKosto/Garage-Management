using System;
using System.Text;

namespace GarageManger.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        // Data members:
        private const int k_NumberOfMotorcycleWheels = 2;
        private const int k_MotorcycleMaxPsi = 30;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(string i_ModelName, string i_LicensePlate, float i_CurrentAirPressure, string i_ProducerName) : base(i_ModelName, i_LicensePlate)
        {
            setWheels(k_NumberOfMotorcycleWheels, k_MotorcycleMaxPsi, i_CurrentAirPressure, i_ProducerName);
        }

        // getters/setters:
        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                m_EngineVolume = value;
            }
        }

        // methods:
        public override string ToString()
        {
            return string.Format(
@"Vehicle type:Motorcycle
License plate:{0}
Model name:{1}
License type:{2}
Engine volume:{3} 
Engine details:
{4}
Wheels details:
{5}", r_LicensePlate, r_ModelName, Enum.GetName(typeof(eLicenseType), m_LicenseType), m_EngineVolume, m_Engine.ToString(), getWheelsInfo());

        }

        // Nested enums:
        public enum eLicenseType
        {
            A = 1,
            A1,
            AA,
            B,
        }
    }
}
