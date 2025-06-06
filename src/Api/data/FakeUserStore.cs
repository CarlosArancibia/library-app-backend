namespace Api.Data
{
  public static class FakeUserStore
  {
    public static List<(string Username, string Password, string Role)> Users = new()
      {
          ("admin", "password", "Admin"),
          ("user1", "123456", "User"),
          ("carlos", "mipass", "User")
      };
  }
}
