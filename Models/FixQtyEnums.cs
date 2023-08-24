using System.Text.Json.Serialization;

namespace API.Models{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorEnum{
        OutStand = 0,
        CloseEndDate = 1,
        OutReason = 2,
        EndDate = 3,
        None = 4
    };
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EState{
        ToSupervise = 0,
        ToApprove = 1,
        Approved = 2
    };
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EReason{
        PointsOfUse = 0,
        DaysToCover = 1,
        Other = 2
    }
}
