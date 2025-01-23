namespace Business.Tools;

public static class Converter
{ 
    public static float LbToKg(float value) {
        return (value / 2.20462f);
    }
    public static float KmToMiles(float value) {
        return (value / 1.60934f);
    }
    public static float StepsToKm(float height, float steps) {
        return (height * steps * 0.415f) / 1000;
    }
}