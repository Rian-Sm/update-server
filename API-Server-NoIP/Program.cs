
using API_Server_NoIP.Service;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        var environment = Environment.GetEnvironmentVariables();

        string path = $"{Directory.GetCurrentDirectory()}/Data";
        string fullpath = $"{path}/PreviusIp.data";

        string? userName = environment["USER_NAME"].ToString();
        string? userPass = environment["USER_PASSWORD"].ToString();
        string? userDNS = environment["DNS"].ToString();
        string? url = environment["URL"].ToString();

        if (userName == null || userPass == null || url == null || userDNS == null)
            throw new ArgumentNullException("USER_NAME, USER_PASSWORD, DNS or URL is null");

        string base64user = Base64Encode($"{userName}:{userPass}");
        string ip = await WebClient.GetIP();


        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        bool fileExits = File.Exists(fullpath);
        if (fileExits){
            string previusIp = File.ReadAllText(fullpath);
            if (previusIp.Equals(previusIp))
            {
                return 0;
            }
        }
        
        File.WriteAllText(fullpath, ip);
        
        string urlRequest = $"http://{base64user}@{url}?hostname={userDNS}&myip={ip}";

        string response = await WebClient.SendData(urlRequest);

        Console.WriteLine(response);

        return 0;
    }
    static string Base64Encode(string plainText)
    {
        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
}