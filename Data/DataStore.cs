namespace crudone.Data;
using crudone.Dto;
public static class DataStore
{
    public static List<UserDto> userList = new List<UserDto>()
        {
            new UserDto{Id=1, Name="Aman"},
            new UserDto{Id=2,Name="Prashant"}

        };

    
}


