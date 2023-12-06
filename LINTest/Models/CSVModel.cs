using LINTest.Models;

namespace Backend.Model;

public class CSVModel
{
        public string ActuatorId { get; set; }
        public string CommunicationProtocol { get; set; }
        public string SerialNumber { get; set; }
        public string WorkOrderNumber { get; set; }
        public string PCBAUid { get; set; }
        public string ArticleNumber { get; set; }
        public string ArticleName { get; set; }
        public bool LINTestPassed { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Tester { get; set; }
        public int Bay { get; set; }
        public string MinServoPosition { get; set; }
        public string MaxServoPosition { get; set; }
        public string MinBuslinkPosition { get; set; }
        public string MaxBuslinkPosition { get; set; }
        public string ServoStroke { get; set; }
        public List<TestErrorModel> TestErrors { get; set; } = new();
}