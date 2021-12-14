namespace Nim
{
    public class MoveInput
    {
        public int HeapId { get; }
        public int ObjectsToTake { get; }

        public MoveInput(int heapId, int objectsToTake)
        {
            HeapId = heapId;
            ObjectsToTake = objectsToTake;
        }
    }
}