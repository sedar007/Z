using Common.DAO;

namespace Common.DTO.Helper;

public static class User {
    public static UserDTO ToDto(this UserDAO userDao) {
        return new UserDTO {
            Id = userDao.Id,
            Name = userDao.Name,
            Age = userDao.Age,
            Weight = userDao.Weight,
            Height = userDao.Height
        };
    }
}