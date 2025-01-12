
using API_Server_NoIP.Service;

var environment = Environment.GetEnvironmentVariables();

string? userName =  environment["USER_NAME"].ToString();
string? userPass =  environment["USER_PASSWORD"].ToString();
string? userDNS =  environment["DNS"].ToString();
string? url      =  environment["URL"].ToString();

if (userName == null || userPass == null || url == null || userDNS == null)
    throw new ArgumentNullException("USER_NAME, USER_PASSWORD, DNS or URL is null");

string base64user = Base64Encode($"{userName}:{userPass}");
string ip = await WebClient.GetIP();

string urlRequest = $"http://{base64user}@{url}?hostname={userDNS}&myip={ip}";

string response = await WebClient.SendData(urlRequest);

Console.WriteLine(response);

static string Base64Encode(string plainText) 
{
    byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
    return System.Convert.ToBase64String(plainTextBytes);
}