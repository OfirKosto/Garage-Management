using System;
using System.Text;

namespace GarageManger.GarageLogic
{
    public abstract class Vehicle
    {
        // Date members:
        protected readonly string r_ModelName;
        protected readonly string r_LicensePlate;
        protected Wheel[] m_Wheels;
        protected Engine m_Engine;
        protected float m_EnergyPercentage = 0;

        public Vehicle(string i_ModelName, string i_LicensePlate)
        {
            r_ModelName = i_ModelName;
            r_LicensePlate = i_LicensePlate;
        }

        // getters/setters:
        public string ModelName
        {
            get
            {
                return r_ModelName;
            }
        }

        public string LicensePlate
        {
            get
            {
                return r_LicensePlate;
            }
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }
            set
            {
                m_Wheels = value;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
            set
            {
                m_Engine = value;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return m_EnergyPercentage;
            }
        }
        
        // methods:
        public void UpdateEnergyPercentage()
        {
            m_EnergyPercentage = m_Engine.MaxEnergyCapacity / m_Engine.CurrentEnergyQuantity;
        }

        protected void setWheels(int i_NumberOfWheels, float i_WheelsMaxAirPressure, float i_CurrentAirPressure, string i_ProducerName)
        {
            m_Wheels = new Wheel[i_NumberOfWheels];

            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(i_ProducerName, i_WheelsMaxAirPressure, i_CurrentAirPressure);
            }
        }

        protected string getWheelsInfo()
        {
            return string.Format(
@"Number of Wheels: {0}
Producer: {1}
Current air pressure: {2}
", m_Wheels.Length, m_Wheels[0].PruducerName, m_Wheels[0].CurrentAirPressure);

        }
    }
}


