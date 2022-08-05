namespace tz_saber
{
    public class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            Serializer.Serialize(s, this);
        }

        public void Deserialize(FileStream s)
        {
            Deserializer.Deserialize(s, this);
        }
    }
}
