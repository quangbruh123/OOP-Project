﻿using QLBaiDoXe.ParkingLotModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Xml.Linq;

namespace QLBaiDoXe.DBClasses
{
    public class Staffing
    {
        public static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static void AddStaffInfo(string name, string civilId, string phoneNumber, string address, DateTime dob, string accname, string password)
        {
            if (DataProvider.Ins.DB.Staffs.Any(x => x.CivilID == civilId))
            {
                Staff newStaff = new Staff()
                {
                    StaffName = name,
                    CivilID = civilId,
                    RoleID = 0,
                    PhoneNumber = phoneNumber,
                    StaffAddress = address,
                    DateOfBirth = dob
                };
                DataProvider.Ins.DB.Roles.FirstOrDefault(x => x.RoleID == 1).Staffs.Add(newStaff);
                DataProvider.Ins.DB.SaveChanges();
                AddStaffAccount(username, password);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Tồn tại nhân viên có số căn cước công dân bạn đã nhập!");
                return;
            }
            else
            {
                if (DataProvider.Ins.DB.Accounts.Any(x => x.AccountName == accname))
                {
                    MessageBox.Show("Tồn tại tài khoán có cùng tên đăng nhập mà bạn đã nhập!");
                    return;
                }
            }
            Role role = DataProvider.Ins.DB.Roles.FirstOrDefault(x => x.RoleID == 1);
            Staff newStaff = new Staff()
            {
                StaffName = name,
                CivilID = civilId,
                RoleID = role.RoleID,
                PhoneNumber = phoneNumber,
                StaffAddress = address,
                DateOfBirth = dob,
                Role = role
            };            
            DataProvider.Ins.DB.Staffs.Add(newStaff);
            role.Staffs.Add(newStaff);
            DataProvider.Ins.DB.SaveChanges();

            Staff staff = DataProvider.Ins.DB.Staffs.FirstOrDefault(x => x.CivilID == civilId);            
            SHA256 sha256hash = SHA256.Create();
            string passwordhash = GetHash(sha256hash, password);
            Account staffAccount = new Account()
            {
                AccountName = accname,
                AccountPassword = passwordhash,
                RoleID = 0,
                StaffID = staff.StaffID,
                Staff = staff,
                Role = role
            };
            staff.Accounts.Add(staffAccount);
            role.Accounts.Add(staffAccount);
            DataProvider.Ins.DB.Accounts.Add(staffAccount);
            DataProvider.Ins.DB.SaveChanges();
        }

        public static void AddAdminInfo(string name, string civilId, string phoneNumber, string address, DateTime dob, string accname, string password)
        {
            if (DataProvider.Ins.DB.Staffs.Any(x => x.CivilID == civilId))
            {
                MessageBox.Show("Tồn tại nhân viên có số căn cước công dân bạn đã nhập!");
                return;
            }
            else
            {
                if (DataProvider.Ins.DB.Accounts.Any(x => x.AccountName == name))
                {
                    MessageBox.Show("Tồn tại tài khoán có cùng tên đăng nhập mà bạn đã nhập!");
                    return;
                }
            }
            Role role = DataProvider.Ins.DB.Roles.FirstOrDefault(x => x.RoleID == 2);
            Staff newStaff = new Staff()
            {
                StaffName = name,
                CivilID = civilId,
                RoleID = role.RoleID,
                PhoneNumber = phoneNumber,
                StaffAddress = address,
                DateOfBirth = dob,
                Role = role
            };
            DataProvider.Ins.DB.Staffs.Add(newStaff);
            role.Staffs.Add(newStaff);
            DataProvider.Ins.DB.SaveChanges();

            Staff admin = DataProvider.Ins.DB.Staffs.FirstOrDefault(x => x.CivilID == civilId);
            SHA256 sha256hash = SHA256.Create();
            string passwordhash = GetHash(sha256hash, password);
            Account adminAccount = new Account()
            {
                AccountName = accname,
                AccountPassword = passwordhash,
                RoleID = 1,
                StaffID = admin.StaffID,
                Staff = admin,
                Role = DataProvider.Ins.DB.Roles.FirstOrDefault(x => x.RoleID == 2)
            };
            admin.Accounts.Add(adminAccount);
            role.Accounts.Add(adminAccount);
            DataProvider.Ins.DB.Accounts.Add(adminAccount);
            DataProvider.Ins.DB.SaveChanges();
        }

        public static bool ChangeStaffInfo(string staffName, string staffNewName, string civilId, string role, string phoneNumber, string address, DateTime dob)
        {
            if (DataProvider.Ins.DB.Staffs.Any(x => x.StaffName == staffName))
            {
                Staff staff = DataProvider.Ins.DB.Staffs.FirstOrDefault(x => x.StaffName == staffName);
                staff.StaffName = staffNewName;
                staff.StaffAddress = address;
                staff.CivilID = civilId;
                staff.Role = DataProvider.Ins.DB.Roles.FirstOrDefault(x => x.RoleName == role);
                staff.PhoneNumber = phoneNumber;
                staff.DateOfBirth = dob;
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public static bool DeleteStaff(string name)
        {
            if (DataProvider.Ins.DB.Staffs.Any(x => x.StaffName == name))
            {
                Account account = DataProvider.Ins.DB.Accounts.FirstOrDefault(x => x.Staff.StaffName == name);
                DataProvider.Ins.DB.Accounts.Remove(account);
                Staff staff = DataProvider.Ins.DB.Staffs.FirstOrDefault(x => x.StaffName == name);
                DataProvider.Ins.DB.Staffs.Remove(staff);
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public static bool DeleteAccount(string username)
        {
            if (DataProvider.Ins.DB.Accounts.Any(x => x.AccountName == username))
            {
                Account unwantedAccount = DataProvider.Ins.DB.Accounts.Where(x => x.AccountName == username).FirstOrDefault();
                Role role = DataProvider.Ins.DB.Roles.FirstOrDefault(x => x.RoleID == unwantedAccount.RoleID);
                role.Accounts.Remove(unwantedAccount);
                DataProvider.Ins.DB.Accounts.Remove(unwantedAccount);
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public static Account LogIn(string username, string password)
        {
            if (DataProvider.Ins.DB.Accounts.Any(x => x.AccountName == username))
            {
                SHA256 sha256hash = SHA256.Create();
                string passwordhash = GetHash(sha256hash, password);
                Debug.WriteLine(passwordhash);
                Account account = DataProvider.Ins.DB.Accounts.FirstOrDefault(x => x.AccountName == username);
                if (account.AccountPassword == passwordhash)
                {
                    Timekeep timekeep = new Timekeep()
                    {
                        StaffID = account.StaffID,
                        LoginTime = DateTime.Now,
                        Staff = DataProvider.Ins.DB.Staffs.FirstOrDefault(x => x.StaffID == account.StaffID)
                    };
                    DataProvider.Ins.DB.Timekeeps.Add(timekeep);
                    DataProvider.Ins.DB.SaveChanges();
                    return account;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public static bool LogOut(string username)
        {
            if (DataProvider.Ins.DB.Accounts.Any(x => x.AccountName == username))
            {
                Account account = DataProvider.Ins.DB.Accounts.FirstOrDefault(x => x.AccountName == username);
                Timekeep timekeep = DataProvider.Ins.DB.Timekeeps.FirstOrDefault(x => x.StaffID == account.StaffID);
                timekeep.LogoutTime = DateTime.Now;
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public static bool ChangePassword(string username, string newPassword)
        {
            if (DataProvider.Ins.DB.Accounts.Any(x => x.AccountName == username))
            {
                SHA256 sha256hash = SHA256.Create();
                string passwordhash = GetHash(sha256hash, newPassword);
                Account account = DataProvider.Ins.DB.Accounts.FirstOrDefault(x => x.AccountName == username);
                if (account.AccountPassword != passwordhash)
                {
                    account.AccountPassword = newPassword;
                    return true;
                }                  
                else
                    return false;
            }
            else
                return false;
        }

        public static List<Staff> GetAllStaff()
        {
            return DataProvider.Ins.DB.Staffs.ToList();
        }

        public static List<Staff> FindStaffByName(string name)
        {
            return DataProvider.Ins.DB.Staffs.Where(x => x.StaffName == name).ToList();
        }

        public static List<Staff> FindStaffByCivilID(string CivilID)
        {
            return DataProvider.Ins.DB.Staffs.Where(x => x.CivilID == CivilID).ToList();
        }

        public static List<Staff> FindStaffByRoleID(string Role)
        {
            if (Role == "admin")
                return DataProvider.Ins.DB.Staffs.Where(x => x.RoleID == 2).ToList();
            if (Role == "staff")
                return DataProvider.Ins.DB.Staffs.Where(x => x.RoleID == 1).ToList();
            return null;
        }

        public static List<Staff> FindStaffByPhoneNumber(string PhoneNumber)
        {
            return DataProvider.Ins.DB.Staffs.Where(x => x.PhoneNumber == PhoneNumber).ToList();
        }
        public static List<Staff> FindStaffByStaffAddress(string StaffAddress)
        {
            return DataProvider.Ins.DB.Staffs.Where(x => x.StaffAddress == StaffAddress).ToList();
        }

        public static List<Timekeep> GetTimekeepForStaff(string name)
        {
            return DataProvider.Ins.DB.Timekeeps.Where(x => x.Staff.StaffName == name).ToList();
        }
    }
}
