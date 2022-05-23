# ArmaSquads
This C# library allows ArmaSquads users to manage their Arma 3 squadron by using ArmaSquads API.

---- HOW TO USE ----

- Add the provided DLL file as Reference into your C# project.
- Add `using ArmaSquadsLib;` at the top of your Project code-behind file.
- Create a free API token here → https://armasquads.com/login (need to register)

---- TYPE OF OBJECTS ----
- SquadMember (UUID, Username, Name, eMail, ICQ, Remark)
- Team (ID, Name, Logo)

---- AVAILABLE METHODS TO CALL ----
```cs
public static async Task<List<SquadMember>> GetSquadMembersAsync(string Token, string SquadID) {}
public static async Task<List<Team>> GetSquadIDAsync(string Token) {}
public static async Task<string> UpdateMemberAsync(string username, string uuid, string name, string email, string icq, string remark, string squadID, string Token) {}
public static async Task<string> DeleteMemberAsync(string uuid, string squadID, string Token) {}
public static async Task<string> AddMemberAsync(string username, string uuid, string name, string email, string icq, string remark, string squadID, string Token) {}
```
