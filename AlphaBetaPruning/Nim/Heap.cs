namespace Nim
{
    public struct Heap
    {
        public int ObjectsCount { get; }
        public int ObjectsLeft { get; set; }

        public Heap(int objectsCount)
        {
            ObjectsCount = objectsCount;
            ObjectsLeft = objectsCount;
        }

        public static implicit operator Heap(int objectsCount)
        {
            return new Heap(objectsCount);
        }
    }
}