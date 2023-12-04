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
}