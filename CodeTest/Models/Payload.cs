namespace CodeTest.Models
{
    public class Payload
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public string GroupId { get; set; }
        public string DataType { get; set; }
        public string Data { get; set; }
        public long Timestamp { get; set; }
    }
}
