using System;

namespace GarageManger.GarageLogic
{
    public abstract class Engine
    {
        // Data members:
        protected float m_MaxEnergyCapacity;
        protected float m_CurrentEnergyQuantity = 0;

        public Engine(float i_MaxEnergyCapacity)
        {
            m_MaxEnergyCapacity = i_MaxEnergyCapacity;
        }

        //getters/setters:
        public float MaxEnergyCapacity
        {
            get
            {
                return m_MaxEnergyCapacity;
            }
        }

        public float CurrentEnergyQuantity
        {
            get
            {
                return m_CurrentEnergyQuantity;
            }
            set
            {
                if (m_CurrentEnergyQuantity + value <= m_MaxEnergyCapacity)
                {
                    m_CurrentEnergyQuantity = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxEnergyCapacity - m_CurrentEnergyQuantity, "There is an exception in the engine capacity.");
                }
            }         
        }
    }
}
