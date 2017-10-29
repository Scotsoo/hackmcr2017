namespace Assets.Scripts
{
    public class InventoryObject
    {
        public string Character { get; set; }

        public int CaptureCount { get; set; }

        public override string ToString()
        {
            return string.Format("Character: {0}, Count: {1}", Character, CaptureCount);
        }
    }
}
