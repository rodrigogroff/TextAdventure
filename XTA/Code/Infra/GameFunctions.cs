using System;

namespace XTA.Code.Infra
{
    public class GameFunctions
    {
        public float[] GenerateLogarithmicArray(int numberOfSteps)
        {
            float[] result = new float[numberOfSteps];

            for (int i = 0; i < numberOfSteps; i++)
            {
                float t = (float)i / (numberOfSteps - 1);
                // Use the Math.Pow method to calculate the logarithmic value
                float logarithmicValue = (float)Math.Pow(10, t) - 1;
                logarithmicValue /= 9; // Normalize the value to the range [0, 1]
                result[i] = logarithmicValue;
            }

            return result;
        }
    }
}
