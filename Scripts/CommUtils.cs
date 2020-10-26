using System.IO;
using System.Text;
public static class CommUtils {
    public static string StreamToString(MemoryStream ms, Encoding encoding) {
        string readString = "";
        if (encoding == Encoding.UTF8) {
            using (var reader = new StreamReader(ms, encoding)) {
                readString = reader.ReadToEnd();
            }
        }
        return readString;
    }
}