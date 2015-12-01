namespace YouScanTestAssesment.Messages
{
    public class ScanMessage
    {
        public ScanMessage(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
