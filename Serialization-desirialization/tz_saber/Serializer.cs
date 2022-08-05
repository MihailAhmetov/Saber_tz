using System.Text;

namespace tz_saber
{
    public class Serializer
    {
        public static void Serialize(FileStream fileStream, ListRand listToSerialize)
        {
            Dictionary<ListNode, int> nodeToIndex = GetNodeToIndex(listToSerialize);
            string listJson = GetListJson(listToSerialize, nodeToIndex);
            byte[] bytes = new UTF8Encoding(true).GetBytes(listJson);
            fileStream.Write(bytes, 0, bytes.Length);
        }

        private static string GetListJson(ListRand listToJson, Dictionary<ListNode, int> nodeToIndex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            HashSet<ListNode> visitedNodes = new HashSet<ListNode>();
            ListNode node = listToJson.Head;
            while (node != null)
            {
                visitedNodes.Add(node);
                WriteNode(node, stringBuilder, nodeToIndex);
                node = node.Next;
                if (node == null || visitedNodes.Contains(node))
                    break;

                stringBuilder.Append(",");
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        private static void WriteNode(ListNode node, StringBuilder stringBuilder, Dictionary<ListNode, int> nodeToIndex)
        {
            stringBuilder.Append("{");
            stringBuilder.Append($"Data='{node.Data}',");
            stringBuilder.Append($"Prev={(node.Prev == null ? "null" : nodeToIndex[node.Prev].ToString())},");
            stringBuilder.Append($"Next={(node.Next == null ? "null" : nodeToIndex[node.Next].ToString())},");
            stringBuilder.Append($"Rand={(node.Rand == null ? "null" : nodeToIndex[node.Rand].ToString())}");
            stringBuilder.Append("}");
        }

        private static Dictionary<ListNode, int> GetNodeToIndex(ListRand list)
        {
            Dictionary<ListNode, int> nodeToIndex = new Dictionary<ListNode, int>();
            ListNode node = list.Head;
            int index = 0;
            while (node != null && !nodeToIndex.ContainsKey(node))
            {
                nodeToIndex.Add(node, index++);
                node = node.Next;
            }
            return nodeToIndex;
        }
    }
}
