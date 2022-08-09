using System.Text;


namespace tz_saber
{    public struct NodeInfo
    {
        public readonly ListNode Node;
        public readonly string Prev;
        public readonly string Next;
        public readonly string Rand;

        public NodeInfo(ListNode node, string data, string prev, string next, string rand)
        {
            Prev = prev;
            node.Data = data;
            Next = next;
            Rand = rand;
            Node = node;
        }
    }

    public static class Deserializer
    {
        public static void Deserialize(FileStream fileStreamToRead, ListRand listToDeserialize)
        {
            CheckStreamBeginning(fileStreamToRead);

            List<NodeInfo> nodeInfos = new List<NodeInfo>();
            bool readNode = false;
            while (true)
            {
                int b = fileStreamToRead.ReadByte();

                if (b == '{' && !readNode)
                {
                    readNode = true;
                    nodeInfos.Add(DeserializeNode(fileStreamToRead));
                }
                else if (b == ',' && readNode)
                    readNode = false;
                else if (b == ']')
                    break;
            }
            FillList(listToDeserialize, nodeInfos);
        }

        private static void FillList(ListRand listToRestore, List<NodeInfo> nodeInfos)
        {
            if (nodeInfos.Count <= 0) return;
            ConnectNodes(nodeInfos);
            listToRestore.Head = nodeInfos.First().Node;
            listToRestore.Tail = nodeInfos.Last().Node;
            listToRestore.Count = nodeInfos.Count;
        }

        private static void ConnectNodes(List<NodeInfo> nodeInfos)
        {
            foreach (NodeInfo nodeInfo in nodeInfos)
            {
                if (nodeInfo.Next != "null")
                    nodeInfo.Node.Next = nodeInfos[int.Parse(nodeInfo.Next)].Node;
                if (nodeInfo.Prev != "null")
                    nodeInfo.Node.Prev = nodeInfos[int.Parse(nodeInfo.Prev)].Node;
                if (nodeInfo.Rand != "null")
                    nodeInfo.Node.Rand = nodeInfos[int.Parse(nodeInfo.Rand)].Node;
            }
        }

        private static NodeInfo DeserializeNode(FileStream fileStream)
        {
            ListNode node = new ListNode();
            Dictionary<string, string> fields = new Dictionary<string, string>();

            for (int i = 0; i < 3; ++i)
                ReadField(',', fileStream, fields);
            
            ReadField('}', fileStream, fields);

            return new NodeInfo(node, fields["Data"], fields["Prev"], fields["Next"], fields["Rand"]);
        }

        private static void ReadField(char stopChar, FileStream fileStream, Dictionary<string, string> fields)
        {
            string line = ReadUntil(fileStream, stopChar);
            string[] parts = line.Split(new[] { '=' }, 2);
            fields.Add(parts[0], parts[1]);
        }

        private static string ReadUntil(FileStream fileStream, char stopChar)
        {
            StringBuilder stringBuilder = new StringBuilder();
            while (true)
            {
                int b = fileStream.ReadByte();

                if ((char)b == stopChar)
                    break;

                if (b == '\'')
                    stringBuilder.Append(ReadUntil(fileStream, '\''));
                else
                    stringBuilder.Append((char)b);
            }
            return stringBuilder.ToString();
        }
    }
}
