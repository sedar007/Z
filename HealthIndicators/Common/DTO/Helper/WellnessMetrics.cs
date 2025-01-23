namespace Common.DTO.Helper;

public static class WellnessMetrics {
    public static WellnessMetricsDTO ToDto(this WellnessMetricsDAO wellnessMetricsDAO) {
        return new WellnessMetricsDTO {
            Id = wellnessMetricsDAO.Id,
            UserId = wellnessMetricsDAO.UserId,
            Steps = wellnessMetricsDAO.Steps,
            SleepDuration = wellnessMetricsDAO.SleepDuration,
            HeartRate = wellnessMetricsDAO.HeartRate
        };
    }
}
