namespace Business.Tools;

public static class Converter
{ 
    public static float LbToKb(float value) {
        return (2.20462f * value);
    }
}