using System;

namespace GarageManger.GarageLogic
{
     public class Wheel
    {
        // Date members:
        private string m_ProducerName;
        private float m_MaxAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_ProducerName,float i_MaxAirPressure, float i_CurrentAirPressure)
        {
            m_ProducerName = i_ProducerName;
            m_MaxAirPressure = i_MaxAirPressure;
            this.CurrentAirPressure = i_CurrentAirPressure; // check error
        }

        // getters/setters:
        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
            set
            {
                m_MaxAirPressure = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value >= 0 && value <= m_MaxAirPressure)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxAirPressure - m_CurrentAirPressure, "The amount of air in the wheel is out of range.");
                }
            }
        }

        public string PruducerName
        {
            get
            {
                return m_ProducerName;
            }
            set
            {
                m_ProducerName = value;
            }
        }

        // methods:
        public void Inflate(float i_AirToAdd) 
        {
            if (i_AirToAdd >= 0)
            {
                if (m_CurrentAirPressure + i_AirToAdd <= m_MaxAirPressure)
                {
                    m_CurrentAirPressure += i_AirToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxAirPressure, "The amount of the air to add is to high and will cause the wheel to explode!");
                }
            }
            else
            {
                throw new ArgumentException("The amount of air to add cannot be negative.");
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Producer name:{0}
current air pressure:{1}
", m_ProducerName, m_CurrentAirPressure);

        }
    }
}
