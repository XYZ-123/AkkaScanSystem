namespace YouScanTestAssesment.Messages
{
    public class CalculateMessage
    {
        public CalculateMessage(bool flush)
        {
            Flush = flush;
        }

        public bool Flush { get; private set; }
    }
}
