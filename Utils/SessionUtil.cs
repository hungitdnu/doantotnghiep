using HeThongDangKyDuHoc.Context;
using HeThongDangKyDuHoc.Models;
using Microsoft.EntityFrameworkCore;

namespace HeThongDangKyDuHoc.Utils
{
    public class SessionUtil
    {
        public static Account? getAccountFromUsr(MyContext context, ISession session)
        {
            var username = session.GetString(Program.session_username);
            if (username == null)
            {
                return null;
            }
            var user = context.Accounts.FirstOrDefault(x => x.Username == username);
            return user;
        }
        public static SinhVien? getSinhVienFromUsr(MyContext context, ISession session)
        {
            var user = getAccountFromUsr(context, session);
            if(user == null || user.isAdmin)
            {
                return null;
            }
            var sinhvien = context.SinhViens.FirstOrDefault(x => x.Username == user.Username);
            return sinhvien;
        }
        public static GiangVien? getGiangVienFromUsr(MyContext context, ISession session)
        {
            var user = getAccountFromUsr(context, session);
            if(user == null || !user.isAdmin)
            {
                return null;
            }
            var giangvien = context.GiangViens.FirstOrDefault(x => x.Username == user.Username);
            return giangvien;
        }
    }
}
