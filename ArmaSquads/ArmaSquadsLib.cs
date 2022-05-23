using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.IO;
/// <ArmaSquads>
/// ArmaSquad class allows to access ArmaSquads WebAPI to edit squads and members
/// </ArmaSquads>
public static class ArmaSquadsLib
{
    /// <summary>
    /// WebAPI Personal Token
    /// </summary>
    public static string API_TOKEN { get; set; }
    /// <summary>
    /// ID of the Squad to manage in this instance
    /// </summary>
    public static string SquadID { get; set; }
    /// <summary>
    /// Name of the squad of this instance
    /// </summary>
    public static string SquadName { get; set; }
    /// <summary>
    /// Title of the squad of this instance
    /// </summary>
    public static string SquadTitle { get; set; }
    /// <summary>
    /// Logo of the squad of this instance (Base64 Image)
    /// </summary>
    public static string SquadLogo { get; set; }

    /// <summary>
    /// Obtain a list of SquadMember type containing all squad members based on Squad ID
    /// </summary>
    /// <param name="Token">WebAPI Personal Token</param>
    /// <param name="SquadID">ID of the squad</param>
    /// <returns>Return a List of SquadMember containing all squad members</returns>
    public static async Task<List<SquadMember>> GetSquadMembersAsync(string Token, string SquadID)
    {
        string URL = "https://armasquads.com/api/v1/squads/" + SquadID + "/members?key=" + Token;
        string response = await Http_GetRequest(URL);
        //Parsing JSON
        SquadMemberResponse members = Newtonsoft.Json.JsonConvert.DeserializeObject<SquadMemberResponse>(response);
        return members.results;
    }
    /// <summary>
    /// Obtain a list of SquadMember type containing all squad members based on Squad ID of this instance
    /// </summary>
    /// <returns>Return a List of SquadMember containing all squad members</returns>
    public static async Task<List<Team>> GetSquadIDAsync(string Token)
    {
        string URL = "https://armasquads.com/api/v1/squads?key=" + Token;
        string response = await Http_GetRequest(URL);
        //Parsing JSON
        TeamResponse team = Newtonsoft.Json.JsonConvert.DeserializeObject<TeamResponse>(response);
        List<Team> teamList = team.results;
        return teamList;
    }
    /// <summary>
    /// Update an existing member of the given squad ID
    /// </summary>
    /// <param name="username">Member updated Username</param>
    /// <param name="uuid">Member updated UUID</param>
    /// <param name="name">Member updated Name</param>
    /// <param name="email">Member updated eMail</param>
    /// <param name="icq">Member updated ICQ</param>
    /// <param name="remark">Member updated Remark</param>
    /// <param name="squadID">Squad ID of the member</param>
    /// <param name="Token">WebAPI Personal Token</param>
    /// <returns>Returns a string containing the result of the update action</returns>
    public static async Task<string> UpdateMemberAsync(string username, string uuid, string name, string email, string icq, string remark, string squadID, string Token)
    {
        SquadMember updateMember = new SquadMember(username, uuid, name, email, icq, remark);
        string URI = $"https://armasquads.com/api/v1/squads/{squadID}/members/{uuid}?key={Token}";
        string payload = Newtonsoft.Json.JsonConvert.SerializeObject(updateMember);
        return await Http_PutRequest(URI, payload);
    }
    /// <summary>
    /// Delete an existing member of the given Squad ID
    /// </summary>
    /// <param name="uuid">UUID of the member to delete</param>
    /// <param name="squadID">Squad ID of the member</param>
    /// <param name="Token">WebAPI Personal Token</param>
    /// <returns>Returns a string containing the result of the delete action</returns>
    public static async Task<string> DeleteMemberAsync(string uuid, string squadID, string Token)
    {
        string URI = "http://armasquads.com/api/v1/squads/" + squadID + "/members/" + uuid + "?key=" + Token;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        return await Http_DeleteRequest(URI);
    }
    /// <summary>
    /// Add a new member to the given Squad ID
    /// </summary>
    /// <param name="username">New member Username</param>
    /// <param name="uuid">New member UUID</param>
    /// <param name="name">New member Name</param>
    /// <param name="email">New member eMail</param>
    /// <param name="icq">New member ICQ</param>
    /// <param name="remark">New member Remark</param>
    /// <param name="squadID">Squad ID of the new member</param>
    /// <param name="Token">WebAPI Personal Token</param>
    /// <returns>Returns a string containing the result of the adding action</returns>
    public static async Task<string> AddMemberAsync(string username, string uuid, string name, string email, string icq, string remark, string squadID, string Token)
    {
        SquadMember addMember = new SquadMember(username, uuid, name, email, icq, remark);
        string URI = $"https://armasquads.com/api/v1/squads/{squadID}/members?key={Token}";
        string payload = Newtonsoft.Json.JsonConvert.SerializeObject(addMember);
        return await Http_PostRequest(URI, payload);
    }
    private static async Task<string> Http_GetRequest(string URI)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
        using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
        using (System.IO.Stream stream = response.GetResponseStream())
        using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
            return await reader.ReadToEndAsync();
    }
    private static async Task<string> Http_PutRequest(string URI, string jsonData)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        var data = new System.Net.Http.StringContent(jsonData, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
        using (var client = new System.Net.Http.HttpClient())
        {
            var response = await client.PutAsync(URI, data);
            return response.StatusCode.ToString();
        }
    }
    private static async Task<string> Http_PostRequest(string URI, string jsonData)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        var data = new System.Net.Http.StringContent(jsonData, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
        using (var client = new System.Net.Http.HttpClient())
        {
            var response = await client.PostAsync(URI, data);
            return response.StatusCode.ToString();
        }
    }
    private static async Task<string> Http_DeleteRequest(string URI)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        using (var client = new System.Net.Http.HttpClient())
        {
            var response = await client.DeleteAsync(URI);
            return response.StatusCode.ToString();
        }
    }
}

public struct SquadMemberResponse
    {
        public bool success { get; set; }
        public List<SquadMember> results { get; set; }
    }
public class SquadMember
{
    [Newtonsoft.Json.JsonProperty("uuid")]
    public string UUID { get; set; }
    [Newtonsoft.Json.JsonProperty("username")]
    public string Username { get; set; }
    [Newtonsoft.Json.JsonProperty("name")]
    public string Name { get; set; }
    [Newtonsoft.Json.JsonProperty("email")]
    public string eMail { get; set; }
    [Newtonsoft.Json.JsonProperty("icq")]
    public string Icq { get; set; }
    [Newtonsoft.Json.JsonProperty("remark")]
    public string Remark { get; set; }

    public SquadMember() { }
    public SquadMember(string user, string uuid, string name, string email, string icq, string remark)
    {
        Username = user; UUID = uuid; Name = name; eMail = email; Icq = icq; Remark = remark;
    }
}
public struct TeamResponse
    {
        public bool success { get; set; }
        public List<Team> results { get; set; }
    }
public struct Team
{
    [Newtonsoft.Json.JsonProperty("id")]
    public string ID { get; set; }
    [Newtonsoft.Json.JsonProperty("privateID")]
    private string PrivateID { get; set; }
    [Newtonsoft.Json.JsonProperty("tag")]
    private string Tag { get; set; }
    [Newtonsoft.Json.JsonProperty("name")]
    public string Name { get; set; }
    [Newtonsoft.Json.JsonProperty("email")]
    private string eMail { get; set; }
    [Newtonsoft.Json.JsonProperty("logo")]
    public string Logo { get; set; }
    [Newtonsoft.Json.JsonProperty("homepage")]
    private string Homepage { get; set; }
    [Newtonsoft.Json.JsonProperty("title")]
    public string Title { get; set; }
}