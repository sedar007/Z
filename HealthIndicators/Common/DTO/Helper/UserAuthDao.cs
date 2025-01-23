using Common.DAO;

namespace Common.DTO.Helper;

public static class UserAuth {
    public static UserAuthDto ToDto(this UserAuthDao userDao) {
        return new UserAuthDto {
            Id = userDao.Id,
            Username = userDao.Username,
            Password = userDao.Password
        };
    }
}