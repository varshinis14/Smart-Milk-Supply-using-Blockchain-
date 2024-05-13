using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using Sha2;
using System.Collections.ObjectModel;

namespace SmartMilkSupply
{
    public class MyConnection
    {
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataAdapter adp = null;
        MySqlConnection contemp = null;

        string con_temp = "server=localhost;database=smartmilksupplytemp;user id=root;port=3306;";

        string databasename = "smartmilksupply";
        string databasenametemp = "smartmilksupplytemp";

        public void OpenConnection()
        {
            con = new MySqlConnection("server=localhost;database=smartmilksupply;username=root;port=3306;");
            con.Open();
        }
        public int LoginVerify(string UserId, string Password, string UserType)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = "";
            if (UserType == "Admin")
            {
                sql = string.Format("select count(*) from adminmaster where Id={0} and Password='{1}'", UserId, Password);
            }
            else if (UserType == "VillageDairy")
            {
                sql = string.Format("select count(*) from villagedairy where VDId={0} and Password='{1}'", UserId, Password);
            }
            else if (UserType == "Farmer")
            {
                sql = string.Format("select count(*) from farmermaster where FarmerId={0} and Password='{1}'", UserId, Password);
            }
            else if (UserType == "MilkBooth")
            {
                sql = string.Format("select count(*) from milkbooth where MBId={0} and Password='{1}'", UserId, Password);
            }
            cmd.CommandText = sql;
            int result = int.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return result;
        }
        public string ChangePassword(int UserId, string Password, string UserType)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sql = "";
            if (UserType == "Admin")
            {
                sql = string.Format("Update adminmaster set Password={0} where Id={1}", Password, UserId);
            }
            else if (UserType == "VillageDairy")
            {
                sql = string.Format("Update villagedairy set Password={0} where VDId={1}", Password, UserId);
            }
            else if (UserType == "Farmer")
            {
                sql = string.Format("Update farmermaster set Password={0} where FarmerId={1}", Password, UserId);
            }
            else if (UserType == "MilkBooth")
            {
                sql = string.Format("Update milkbooth set Password={0} where MBId={1}", Password, UserId);
            }
            
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            con.Close();
            return result;
        }

        public int AddArea(string AreaName)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sqlcnt = string.Format("select count(*) from areamaster where AreaName='{0}'", AreaName);
            cmd.CommandText = sqlcnt;
            int cnt = int.Parse(cmd.ExecuteScalar().ToString());
            int result = 0;
            if (cnt == 0)
            {

                string sql = string.Format("insert into areamaster(AreaName)values('{0}')", AreaName);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();
            }
            else
            {
                result = 2;
            }
            con.Close();
            return result;
        }
        public DataTable GetArea()
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select * from areamaster");
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public int AddVillage(string VillageName)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sqlcnt = string.Format("select count(*) from villagemaster where VillageName='{0}'", VillageName);
            cmd.CommandText = sqlcnt;
            int cnt = int.Parse(cmd.ExecuteScalar().ToString());
            int result=0;
            if (cnt == 0)
            {

                string sql = string.Format("insert into villagemaster(VillageName)values('{0}')", VillageName);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();
            }
            else
            {
                result = 2;
            }
            con.Close();
            return result;
        }
        public DataTable GetVillage()
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select * from villagemaster");
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public int AddVillageDairy(int VDId, int VillageId, string Name, string Password, string MobileNo, string Address)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sqlcnt = string.Format("select count(*) from villagedairy where VillageId={0} and Name='{1}'", VillageId, Name);
            cmd.CommandText = sqlcnt;
            int cnt = int.Parse(cmd.ExecuteScalar().ToString());
            int result = 0;
            if (cnt == 0)
            {

                string sql = string.Format("insert into villagedairy(VDId,VillageId,Name,Password,MobileNo,Address,Balance)values({0},{1},'{2}','{3}','{4}','{5}',10000)", VDId, VillageId, Name, Password, MobileNo, Address);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();
            }
            else
            {
                result = 2;
            }
            con.Close();
            return result;
        }
        public int AddMilkBooth(int MilkBoothId, int AreaId, string Name, string Password, string MobileNo, string Address)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            int result = 0;
            string sql = string.Format("insert into milkbooth(MBId,AreaId,Name,Password,MobileNo,Address,Balance)values({0},{1},'{2}','{3}','{4}','{5}',0)", MilkBoothId, AreaId, Name, Password, MobileNo, Address);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public string CreateProduct(string ProductName, string Description, int Price)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sqlchk = string.Format("Select count(*) from productmaster where ProductName='{0}'", ProductName);
            cmd.CommandText = sqlchk;
            int cnt = int.Parse(cmd.ExecuteScalar().ToString());
            string result = "";
            if (cnt == 0)
            {
                string sql = string.Format("insert into productmaster(ProductName,Description,Price)values('{0}','{1}',{2})",ProductName, Description, Price);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();
            }
            else
            {
                result = "2";
            }
            con.Close();
            return result;
        }
        public DataTable GetProduct()
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("Select * from productmaster");
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public DataTable GetProduct_ProductId(int ProductId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("Select * from productmaster where ProductId={0}", ProductId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string UpdateProduct(int ProductId, int Price)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sql = string.Format("update productmaster set Price={0} where ProductId={1}", Price, ProductId);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            con.Close();
            return result;
        }

        public DataTable GetMilkBooth(int VillageId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select * from milkbooth where VillageId={0}",VillageId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public int AddMilkBoothDeposit(int MilkBoothId, int Amount)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            int result = 0;
            string sql = string.Format("insert into milkboothwallet(MBId,DDate,Amount)values({0},'{1}',{2})", MilkBoothId, DateTime.Now.ToString("dd/MM/yyyy"), Amount);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery();
            string sqlupd = string.Format("Update milkbooth set Balance=Balance+{0} where MBId={1}",Amount,MilkBoothId);
            cmd.CommandText = sqlupd;
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int AddFarmer(int FarmerId,int VDId,string Name,string Password,string MobileNo,string Address)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            int result = 0;
            string sql = string.Format("insert into farmermaster(FarmerId,VDId,Name,Password,MobileNo,Address,Balance)values({0},{1},'{2}','{3}','{4}','{5}',0)", FarmerId, VDId, Name, Password, MobileNo, Address);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
     

        public DataTable GetFarmer(int VDId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("Select * from farmermaster where VDId={0}",VDId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string Check_BMF(string TableName)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sqlcnt = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasename);
            cmd.CommandText = sqlcnt;
            string cnt = cmd.ExecuteScalar().ToString();
            string result = "";
            if (cnt == "1")
            {
                string sqlhv = string.Format("Select max(SlNo) from {0}", TableName);
                cmd.CommandText = sqlhv;
                string SlNoMax = cmd.ExecuteScalar().ToString();

                contemp = new MySqlConnection(con_temp);
                contemp.Open();
                cmd = new MySqlCommand();
                cmd.Connection = contemp;
                string sqltb = string.Format("Select * from {0} where SlNo={1}", TableName, SlNoMax);
                cmd.CommandText = sqltb;
                adp = new MySqlDataAdapter(cmd);
                DataTable tab = new DataTable();
                adp.Fill(tab);

                cmd.Connection = con;
                string sql = string.Format("Select count(*) from {0} where CHV='{1}'", TableName, tab.Rows[0]["CHV"].ToString());
                cmd.CommandText = sql;
                string cntchv = cmd.ExecuteScalar().ToString();
                if (cntchv == "0")
                {
                    result = "3";
                }
                else
                {
                    string sqlchv = string.Format("Select * from {0} where SlNo={1}", TableName, SlNoMax);
                    cmd.CommandText = sqlchv;
                    adp = new MySqlDataAdapter(cmd);
                    DataTable tabchv = new DataTable();
                    adp.Fill(tabchv);
                    contemp.Close();
                    con.Close();
                    result = tabchv.Rows[0]["CHV"].ToString();
                }
            }
            else
            {
                result = "0";
            }
            return result;
        }
        public string CreateTable_VDBMF(int FarmerId, string TableName, string LogDate, int ML, string PHV, string CHV)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sqlcnt = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasename);
            cmd.CommandText = sqlcnt;
            string cnt = cmd.ExecuteScalar().ToString();
            contemp = new MySqlConnection(con_temp);
            contemp.Open();
            if (cnt == "0") //If Table Not Found,Create Dynamic Table
            {

                string sql = string.Format(@"CREATE TABLE `{0}` (
                                    `SlNo` int(11) NOT NULL AUTO_INCREMENT,
                                    `FarmerId` int(11) DEFAULT NULL,
                                    `ML` int(11) DEFAULT NULL,
                                    `LogDate` varchar(200) DEFAULT NULL,
                                    `PHV` varchar(4000) DEFAULT NULL,
                                    `CHV` varchar(4000) DEFAULT NULL,
                                     PRIMARY KEY(`SlNo`)
                                    ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = latin1; ", TableName);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();

                //Temp Table Backup

                cmd.Connection = contemp;
                string resulttemp = "";
                string sqlcnttemp = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasenametemp);
                cmd.CommandText = sqlcnttemp;
                string cnttemp = cmd.ExecuteScalar().ToString();

                if (cnttemp == "0") //If Table Not Found, Create Dynamic Table
                {

                    string sqltemp = string.Format(@"CREATE TABLE `{0}` (
                                   `SlNo` int(11) NOT NULL AUTO_INCREMENT,
                                    `FarmerId` int(11) DEFAULT NULL,
                                    `ML` int(11) DEFAULT NULL,
                                    `LogDate` varchar(200) DEFAULT NULL,
                                    `PHV` varchar(4000) DEFAULT NULL,
                                    `CHV` varchar(4000) DEFAULT NULL,
                                     PRIMARY KEY(`SlNo`)
                                    ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = latin1; ", TableName);
                    cmd.CommandText = sqltemp;
                    resulttemp = cmd.ExecuteNonQuery().ToString();
                }

            }



            cmd.Connection = con;
            string sqlclogcs = string.Format("insert into {0} (FarmerId,ML,LogDate,PHV,CHV)values({1},{2},'{3}','{4}','{5}')", TableName, FarmerId, ML, LogDate, PHV, CHV);
            cmd.CommandText = sqlclogcs;
            result = cmd.ExecuteNonQuery().ToString();

            double ml = ML / 1000;
            double amount = ml * (int.Parse(ConfigurationManager.AppSettings["MilkPrice"].ToString()));

            string sqlmb = string.Format("update villagedairy set Balance=Balance-{0} where VDId={1}", amount, int.Parse(TableName.Split('_')[0].ToString()));
            cmd.CommandText = sqlmb;
            result = cmd.ExecuteNonQuery().ToString();

            string sqlfw = string.Format("insert into farmerwallet(FarmerId,DDate,Amount)values({0},'{1}',{2})", FarmerId,  DateTime.Now.ToString("dd/MM/yyyy"), amount);
            cmd.CommandText = sqlfw;
            result = cmd.ExecuteNonQuery().ToString();

            string sqlfb = string.Format("update farmermaster set Balance=Balance+{0} where FarmerId={1}", amount, FarmerId);
            cmd.CommandText = sqlfb;
            result = cmd.ExecuteNonQuery().ToString();

            string sqlcntsp = string.Format("Select count(*) from vdmilkstock where VDId={0} ", int.Parse(TableName.Split('_')[0]));
            cmd.CommandText = sqlcntsp;
            string cntsp = cmd.ExecuteScalar().ToString();
            string sqlscps = "";
            if (cntsp == "0")
            {
                sqlscps = string.Format("insert into vdmilkstock (VDId,MilkML)values({0},{1})", int.Parse(TableName.Split('_')[0].ToString()), ML);
            }
            else
            {
                sqlscps = string.Format("update vdmilkstock set MilkML=MilkML+{0} where VDId={1}", ML, int.Parse(TableName.Split('_')[0].ToString()));
            }
            cmd.CommandText = sqlscps;
            result = cmd.ExecuteNonQuery().ToString();


            //Temp Table
            cmd.Connection = contemp;
            string sqlclogcsTemp = string.Format("insert into {0} (FarmerId,ML,LogDate,PHV,CHV)values({1},{2},'{3}','{4}','{5}')", TableName, FarmerId, ML, LogDate, PHV, CHV);
            cmd.CommandText = sqlclogcsTemp;
            result = cmd.ExecuteNonQuery().ToString();
            contemp.Close();
            con.Close();
            return result;
        }

        public DataTable GetCMP_Log(string TableName)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select * from {0} order by SlNo desc", TableName);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string ChkVDMTTamper(int SlNo, string TableName)
        {
            OpenConnection();
            contemp = new MySqlConnection(con_temp);
            contemp.Open();
            cmd = new MySqlCommand();
            cmd.Connection = contemp;
            string sqltb = string.Format("Select * from {0} where SlNo={1}", TableName, SlNo);
            cmd.CommandText = sqltb;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            string hashvalue = "";
            if (SlNo == 1)
            {
                hashvalue = ShaKeyGeneration(tab.Rows[0]["FarmerId"].ToString(),  tab.Rows[0]["ML"].ToString(), tab.Rows[0]["LogDate"].ToString());
            }
            else
            {
                hashvalue = ShaKeyGeneration(tab.Rows[0]["FarmerId"].ToString(), tab.Rows[0]["ML"].ToString(), tab.Rows[0]["LogDate"].ToString(), tab.Rows[0]["PHV"].ToString());
            }
            cmd.Connection = con;
            string sql = string.Format("Select count(*) from {0} where CHV='{1}'", TableName, hashvalue);
            cmd.CommandText = sql;
            string result = cmd.ExecuteScalar().ToString();
            contemp.Close();
            con.Close();
            return result;

        }

        public string VDMTRecover(int SlNo, string TableName)
        {
            OpenConnection();
            contemp = new MySqlConnection(con_temp);
            contemp.Open();
            cmd = new MySqlCommand();
            cmd.Connection = contemp;

            string sqltbq = string.Format("Select * from {0} where SlNo={1}", TableName, SlNo);
            cmd.CommandText = sqltbq;
            adp = new MySqlDataAdapter(cmd);
            DataTable tabq = new DataTable();
            adp.Fill(tabq);

            cmd.Connection = con;

            string result = "";
            string sql = string.Format("update {0} set CHV='{1}',ML={2} where SlNo={3}", TableName, tabq.Rows[0]["CHV"].ToString(), int.Parse(tabq.Rows[0]["ML"].ToString()), SlNo);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            contemp.Close();
            con.Close();
            return result;

        }

        public string MDMPRecover(int SlNo, string TableName)
        {
            OpenConnection();
            contemp = new MySqlConnection(con_temp);
            contemp.Open();
            cmd = new MySqlCommand();
            cmd.Connection = contemp;

            string sqltbq = string.Format("Select * from {0} where SlNo={1}", TableName, SlNo);
            cmd.CommandText = sqltbq;
            adp = new MySqlDataAdapter(cmd);
            DataTable tabq = new DataTable();
            adp.Fill(tabq);

            cmd.Connection = con;

            string result = "";
            string sql = string.Format("update {0} set CHV='{1}',Qty={2} where SlNo={3}", TableName, tabq.Rows[0]["CHV"].ToString(), int.Parse(tabq.Rows[0]["Qty"].ToString()), SlNo);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            contemp.Close();
            con.Close();
            return result;

        }
        public DataTable GetVDTotal_ML(int VDId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select * from vdmilkstock where VDId={0}", VDId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }

        public string CreateTable_VDSMDT(int VDId, string TableName, string LogDate, int ML, string PHV, string CHV)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sqlcnt = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasename);
            cmd.CommandText = sqlcnt;
            string cnt = cmd.ExecuteScalar().ToString();
            contemp = new MySqlConnection(con_temp);
            contemp.Open();
            if (cnt == "0") //If Table Not Found,Create Dynamic Table
            {

                string sql = string.Format(@"CREATE TABLE `{0}` (
                                    `SlNo` int(11) NOT NULL AUTO_INCREMENT,
                                    `VDId` int(11) DEFAULT NULL,
                                    `ML` int(11) DEFAULT NULL,
                                    `LogDate` varchar(200) DEFAULT NULL,
                                    `PHV` varchar(4000) DEFAULT NULL,
                                    `CHV` varchar(4000) DEFAULT NULL,
                                     PRIMARY KEY(`SlNo`)
                                    ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = latin1; ", TableName);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();

                //Temp Table Backup

                cmd.Connection = contemp;
                string resulttemp = "";
                string sqlcnttemp = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasenametemp);
                cmd.CommandText = sqlcnttemp;
                string cnttemp = cmd.ExecuteScalar().ToString();

                if (cnttemp == "0") //If Table Not Found, Create Dynamic Table
                {

                    string sqltemp = string.Format(@"CREATE TABLE `{0}` (
                                   `SlNo` int(11) NOT NULL AUTO_INCREMENT,
                                    `VDId` int(11) DEFAULT NULL,
                                    `ML` int(11) DEFAULT NULL,
                                    `LogDate` varchar(200) DEFAULT NULL,
                                    `PHV` varchar(4000) DEFAULT NULL,
                                    `CHV` varchar(4000) DEFAULT NULL,
                                     PRIMARY KEY(`SlNo`)
                                    ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = latin1; ", TableName);
                    cmd.CommandText = sqltemp;
                    resulttemp = cmd.ExecuteNonQuery().ToString();
                }

            }



            cmd.Connection = con;
            string sqlclogcs = string.Format("insert into {0} (VDId,ML,LogDate,PHV,CHV)values({1},{2},'{3}','{4}','{5}')", TableName, VDId, ML, LogDate, PHV, CHV);
            cmd.CommandText = sqlclogcs;
            result = cmd.ExecuteNonQuery().ToString();

            double ml = ML / 1000;
            double amount = ml * (int.Parse(ConfigurationManager.AppSettings["MilkPrice"].ToString()));

            string sqlmb = string.Format("update villagedairy set Balance=Balance+{0} where VDId={1}", amount, VDId);
            cmd.CommandText = sqlmb;
            result = cmd.ExecuteNonQuery().ToString();



            string sqlfb = string.Format("update vdmilkstock set MilkML=MilkML-{0} where VDId={1}", ML, VDId);
            cmd.CommandText = sqlfb;
            result = cmd.ExecuteNonQuery().ToString();

            string sqlcntsp = string.Format("Select count(*) from mdmilkstock where MDId={0} ", 1);
            cmd.CommandText = sqlcntsp;
            string cntsp = cmd.ExecuteScalar().ToString();
            string sqlscps = "";
            if (cntsp == "0")
            {
                sqlscps = string.Format("insert into mdmilkstock (MDId,MilkML)values({0},{1})", 1, ML);
            }
            else
            {
                sqlscps = string.Format("update mdmilkstock set MilkML=MilkML+{0} where MDId={1}", ML, 1);
            }
            cmd.CommandText = sqlscps;
            result = cmd.ExecuteNonQuery().ToString();


            //Temp Table
            cmd.Connection = contemp;
            string sqlclogcsTemp = string.Format("insert into {0} (VDId,ML,LogDate,PHV,CHV)values({1},{2},'{3}','{4}','{5}')", TableName, VDId, ML, LogDate, PHV, CHV);
            cmd.CommandText = sqlclogcsTemp;
            result = cmd.ExecuteNonQuery().ToString();
            contemp.Close();
            con.Close();
            return result;
        }
        public DataTable GetVDMT_Log(string TableName)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select villagedairy.Name,{0}.ML,{0}.SlNo,{0}.LogDate from {0} inner join villagedairy on {0}.VDId=villagedairy.VDId order by {0}.SlNo desc", TableName);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string ChkVDMDTTamper(int SlNo, string TableName)
        {
            OpenConnection();
            contemp = new MySqlConnection(con_temp);
            contemp.Open();
            cmd = new MySqlCommand();
            cmd.Connection = contemp;
            string sqltb = string.Format("Select * from {0} where SlNo={1}", TableName, SlNo);
            cmd.CommandText = sqltb;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            string hashvalue = "";
            if (SlNo == 1)
            {
                hashvalue = ShaKeyGeneration(tab.Rows[0]["VDId"].ToString(), tab.Rows[0]["ML"].ToString(), tab.Rows[0]["LogDate"].ToString());
            }
            else
            {
                hashvalue = ShaKeyGeneration(tab.Rows[0]["VDId"].ToString(), tab.Rows[0]["ML"].ToString(), tab.Rows[0]["LogDate"].ToString(), tab.Rows[0]["PHV"].ToString());
            }
            cmd.Connection = con;
            string sql = string.Format("Select count(*) from {0} where CHV='{1}'", TableName, hashvalue);
            cmd.CommandText = sql;
            string result = cmd.ExecuteScalar().ToString();
            contemp.Close();
            con.Close();
            return result;

        }

        public string Check_CMP(string TableName)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sqlcnt = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasename);
            cmd.CommandText = sqlcnt;
            string cnt = cmd.ExecuteScalar().ToString();
            string result = "";
            if (cnt == "1")
            {
                string sqlhv = string.Format("Select max(SlNo) from {0}", TableName);
                cmd.CommandText = sqlhv;
                string SlNoMax = cmd.ExecuteScalar().ToString();

                contemp = new MySqlConnection(con_temp);
                contemp.Open();
                cmd = new MySqlCommand();
                cmd.Connection = contemp;
                string sqltb = string.Format("Select * from {0} where SlNo={1}", TableName, SlNoMax);
                cmd.CommandText = sqltb;
                adp = new MySqlDataAdapter(cmd);
                DataTable tab = new DataTable();
                adp.Fill(tab);

                cmd.Connection = con;
                string sql = string.Format("Select count(*) from {0} where CHV='{1}'", TableName, tab.Rows[0]["CHV"].ToString());
                cmd.CommandText = sql;
                string cntchv = cmd.ExecuteScalar().ToString();
                if (cntchv == "0")
                {
                    result = "3";
                }
                else
                {
                    string sqlchv = string.Format("Select * from {0} where SlNo={1}", TableName, SlNoMax);
                    cmd.CommandText = sqlchv;
                    adp = new MySqlDataAdapter(cmd);
                    DataTable tabchv = new DataTable();
                    adp.Fill(tabchv);
                    contemp.Close();
                    con.Close();
                    result = tabchv.Rows[0]["CHV"].ToString();
                }
            }
            else
            {
                result = "0";
            }
            return result;
        }
        public string CreateTable_MDMP(int ProductId, int SeriesId, string TableName, string LogDate, int Qty, string PHV, string CHV)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sqlcnt = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasename);
            cmd.CommandText = sqlcnt;
            string cnt = cmd.ExecuteScalar().ToString();
            contemp = new MySqlConnection(con_temp);
            contemp.Open();
            if (cnt == "0") //If Table Not Found,Create Dynamic Table
            {

                string sql = string.Format(@"CREATE TABLE `{0}` (
                                    `SlNo` int(11) NOT NULL AUTO_INCREMENT,
                                    `ProductId` int(11) DEFAULT NULL,
                                    `SeriesId` int(11) DEFAULT NULL,
                                    `Qty` int(11) DEFAULT NULL,
                                    `LogDate` varchar(200) DEFAULT NULL,
                                    `PHV` varchar(4000) DEFAULT NULL,
                                    `CHV` varchar(4000) DEFAULT NULL,
                                     PRIMARY KEY(`SlNo`)
                                    ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = latin1; ", TableName);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();

                //Temp Table Backup

                cmd.Connection = contemp;
                string resulttemp = "";
                string sqlcnttemp = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasenametemp);
                cmd.CommandText = sqlcnttemp;
                string cnttemp = cmd.ExecuteScalar().ToString();

                if (cnttemp == "0") //If Table Not Found, Create Dynamic Table
                {

                    string sqltemp = string.Format(@"CREATE TABLE `{0}` (
                                   `SlNo` int(11) NOT NULL AUTO_INCREMENT,
                                    `ProductId` int(11) DEFAULT NULL,
                                    `SeriesId` int(11) DEFAULT NULL,
                                    `Qty` int(11) DEFAULT NULL,
                                    `LogDate` varchar(200) DEFAULT NULL,
                                    `PHV` varchar(4000) DEFAULT NULL,
                                    `CHV` varchar(4000) DEFAULT NULL,
                                     PRIMARY KEY(`SlNo`)
                                    ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = latin1; ", TableName);
                    cmd.CommandText = sqltemp;
                    resulttemp = cmd.ExecuteNonQuery().ToString();
                }

            }
            cmd.Connection = con;
            string sqlclogcs = string.Format("insert into {0} (ProductId,SeriesId,Qty,LogDate,PHV,CHV)values({1},{2},{3},'{4}','{5}','{6}')", TableName, ProductId, SeriesId, Qty, LogDate, PHV, CHV);
            cmd.CommandText = sqlclogcs;
            result = cmd.ExecuteNonQuery().ToString();

            string sqlmp = string.Format("insert into milkproduction (ProductId,SeriesId,Balance,MDate)values({0},{1},{2},'{3}')", ProductId, SeriesId, Qty, DateTime.Now.ToString());
            cmd.CommandText = sqlmp;
            result = cmd.ExecuteNonQuery().ToString();



            //Temp Table
            cmd.Connection = contemp;
            string sqlclogcsTemp = string.Format("insert into {0} (ProductId,SeriesId,Qty,LogDate,PHV,CHV)values({1},{2},{3},'{4}','{5}','{6}')", TableName, ProductId, SeriesId, Qty, LogDate, PHV, CHV);
            cmd.CommandText = sqlclogcsTemp;
            result = cmd.ExecuteNonQuery().ToString();
            contemp.Close();
            con.Close();
            return result;
        }
        public DataTable GetMDMP_Log(string TableName)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select productmaster.ProductName,{0}.Qty,{0}.LogDate,{0}.SlNo,{0}.SeriesId from {0} inner join productmaster on productmaster.ProductId={0}.ProductId order by {0}.SlNo desc", TableName);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string ChkMDMPTamper(int SlNo, string TableName)
        {
            OpenConnection();
            contemp = new MySqlConnection(con_temp);
            contemp.Open();
            cmd = new MySqlCommand();
            cmd.Connection = contemp;
            string sqltb = string.Format("Select * from {0} where SlNo={1}", TableName, SlNo);
            cmd.CommandText = sqltb;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            string hashvalue = "";
            if (SlNo == 1)
            {
                hashvalue = ShaKeyGeneration(tab.Rows[0]["ProductId"].ToString(),tab.Rows[0]["SeriesId"].ToString(), tab.Rows[0]["Qty"].ToString(), tab.Rows[0]["LogDate"].ToString());
            }
            else
            {
                hashvalue = ShaKeyGeneration(tab.Rows[0]["ProductId"].ToString(), tab.Rows[0]["SeriesId"].ToString(), tab.Rows[0]["Qty"].ToString(), tab.Rows[0]["LogDate"].ToString(), tab.Rows[0]["PHV"].ToString());
            }
            cmd.Connection = con;
            string sql = string.Format("Select count(*) from {0} where CHV='{1}'", TableName, hashvalue);
            cmd.CommandText = sql;
            string result = cmd.ExecuteScalar().ToString();
            contemp.Close();
            con.Close();
            return result;

        }
        public string AddMBWallet(int MBId, int Amount)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sql = string.Format("insert into mbwallet(MBId,DDate,Amount) Values({0},'{1}',{2})", MBId, DateTime.Now.ToString("dd/MM/yyyy"), Amount);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            if (result == "1")
            {
                string sqluw = string.Format("update milkbooth set Balance=Balance+{0} where MBId={1}", Amount, MBId);
                cmd.CommandText = sqluw;
                result = cmd.ExecuteNonQuery().ToString();
            }
            con.Close();
            return result;
        }
        public DataTable GetMBWallet_Id(int MBId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format(@"SELECT mbwallet.DDate as DepositeDate, mbwallet.Amount, milkbooth.Balance, milkbooth.MBId
                                        FROM milkbooth INNER JOIN mbwallet ON milkbooth.MBId = mbwallet.MBId
                                        where milkbooth.MBId={0}", MBId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string MBOrderMP(int MBId, int ProductId, int Qty)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sql = string.Format("insert into mborder(MBId,ProductId,OrderDate,Qty,Status) Values({0},{1},'{2}',{3},'Pending')", MBId, ProductId, DateTime.Now.ToString("dd/MM/yyyy"), Qty);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            con.Close();
            return result;
        }
        public DataTable GetMBMPO_P(int ProductId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select milkbooth.MBId,milkbooth.Name,mborder.Qty,mborder.MBMPOId,mborder.OrderDate from mborder inner join milkbooth on mborder.MBId=milkbooth.MBId where mborder.ProductId={0} and mborder.Status='Pending'", ProductId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public DataTable GetMPStock(int ProductId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select SeriesId,Balance,MDate from milkproduction where ProductId={0}", ProductId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string Check_MBMPT(string TableName)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sqlcnt = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasename);
            cmd.CommandText = sqlcnt;
            string cnt = cmd.ExecuteScalar().ToString();
            string result = "";
            if (cnt == "1")
            {
                string sqlhv = string.Format("Select max(SlNo) from {0}", TableName);
                cmd.CommandText = sqlhv;
                string SlNoMax = cmd.ExecuteScalar().ToString();

                contemp = new MySqlConnection(con_temp);
                contemp.Open();
                cmd = new MySqlCommand();
                cmd.Connection = contemp;
                string sqltb = string.Format("Select * from {0} where SlNo={1}", TableName, SlNoMax);
                cmd.CommandText = sqltb;
                adp = new MySqlDataAdapter(cmd);
                DataTable tab = new DataTable();
                adp.Fill(tab);

                cmd.Connection = con;
                string sql = string.Format("Select count(*) from {0} where CHV='{1}'", TableName, tab.Rows[0]["CHV"].ToString());
                cmd.CommandText = sql;
                string cntchv = cmd.ExecuteScalar().ToString();
                if (cntchv == "0")
                {
                    result = "3";
                }
                else
                {
                    string sqlchv = string.Format("Select * from {0} where SlNo={1}", TableName, SlNoMax);
                    cmd.CommandText = sqlchv;
                    adp = new MySqlDataAdapter(cmd);
                    DataTable tabchv = new DataTable();
                    adp.Fill(tabchv);
                    contemp.Close();
                    con.Close();
                    result = tabchv.Rows[0]["CHV"].ToString();
                }
            }
            else
            {
                result = "0";
            }
            return result;
        }

        public string CreateTable_MBMPOS(int MBMPOId, int ProductId, int SeriesId, string TableName, string LogDate, int Qty, string PHV, string CHV)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";

            string sqldo = string.Format("update mborder set Status='Approve' where MBMPOId={0}", MBMPOId);
            cmd.CommandText = sqldo;
            result = cmd.ExecuteNonQuery().ToString();

            string sqlmp = string.Format("update milkproduction set Balance=Balance-{0} where SeriesId={1}", Qty, SeriesId);
            cmd.CommandText = sqlmp;
            result = cmd.ExecuteNonQuery().ToString();

            string sqlscs = string.Format("Select * from productmaster where ProductId={0}", ProductId);
            cmd.CommandText = sqlscs;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);

            string sqlmb = string.Format("update milkbooth set Balance=Balance-{0} where MBId={1}", int.Parse(tab.Rows[0]["Price"].ToString()) * Qty, int.Parse(TableName.Split('_')[0].ToString()));
            cmd.CommandText = sqlmb;
            result = cmd.ExecuteNonQuery().ToString();

            string sqldmp = string.Format("insert into mbmpsupply (MBId,ProductId,SeriesId,Balance,PDate)values({0},{1},{2},{3},'{4}')", int.Parse(TableName.Split('_')[1]),ProductId, SeriesId, Qty, DateTime.Now.ToString());
            cmd.CommandText = sqldmp;
            result = cmd.ExecuteNonQuery().ToString();

            string sqlcnt = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasename);
            cmd.CommandText = sqlcnt;
            string cnt = cmd.ExecuteScalar().ToString();
            contemp = new MySqlConnection(con_temp);
            contemp.Open();
            if (cnt == "0") //If Table Not Found,Create Dynamic Table
            {

                string sql = string.Format(@"CREATE TABLE `{0}` (
                                    `SlNo` int(11) NOT NULL AUTO_INCREMENT,
                                    `ProductId` int(11) DEFAULT NULL,
                                    `SeriesId` int(11) DEFAULT NULL,
                                    `Qty` int(11) DEFAULT NULL,
                                    `LogDate` varchar(200) DEFAULT NULL,
                                    `PHV` varchar(4000) DEFAULT NULL,
                                    `CHV` varchar(4000) DEFAULT NULL,
                                     PRIMARY KEY(`SlNo`)
                                    ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = latin1; ", TableName);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();

                //Temp Table Backup

                cmd.Connection = contemp;
                string resulttemp = "";
                string sqlcnttemp = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasenametemp);
                cmd.CommandText = sqlcnttemp;
                string cnttemp = cmd.ExecuteScalar().ToString();

                if (cnttemp == "0") //If Table Not Found, Create Dynamic Table
                {

                    string sqltemp = string.Format(@"CREATE TABLE `{0}` (
                                   `SlNo` int(11) NOT NULL AUTO_INCREMENT,
                                    `ProductId` int(11) DEFAULT NULL,
                                    `SeriesId` int(11) DEFAULT NULL,
                                    `Qty` int(11) DEFAULT NULL,
                                    `LogDate` varchar(200) DEFAULT NULL,
                                    `PHV` varchar(4000) DEFAULT NULL,
                                    `CHV` varchar(4000) DEFAULT NULL,
                                     PRIMARY KEY(`SlNo`)
                                    ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = latin1; ", TableName);
                    cmd.CommandText = sqltemp;
                    resulttemp = cmd.ExecuteNonQuery().ToString();
                }

            }



            cmd.Connection = con;
            string sqlclogcs = string.Format("insert into {0} (ProductId,SeriesId,Qty,LogDate,PHV,CHV)values({1},{2},{3},'{4}','{5}','{6}')", TableName, ProductId, SeriesId, Qty, LogDate, PHV, CHV);
            cmd.CommandText = sqlclogcs;
            result = cmd.ExecuteNonQuery().ToString();



            //Temp Table
            cmd.Connection = contemp;
            string sqlclogcsTemp = string.Format("insert into {0} (ProductId,SeriesId,Qty,LogDate,PHV,CHV)values({1},{2},{3},'{4}','{5}','{6}')", TableName, ProductId, SeriesId, Qty, LogDate, PHV, CHV);
            cmd.CommandText = sqlclogcsTemp;
            result = cmd.ExecuteNonQuery().ToString();
            contemp.Close();
            con.Close();
            return result;
        }
        public DataTable GetMBMPStock(int ProductId, int MBId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select * from mbmpsupply where MBId={0} and ProductId={1}", MBId, ProductId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string CreateTable_MBPSales(int ProductId, int SeriesId, string TableName, string LogDate, int Qty, string PHV, string CHV, int SMBMPId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";

            string sqlmp = string.Format("update mbmpsupply set Balance=Balance-{0} where SMBMPId={1}", Qty, SMBMPId);
            cmd.CommandText = sqlmp;
            result = cmd.ExecuteNonQuery().ToString();

            string sqlcnt = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasename);
            cmd.CommandText = sqlcnt;
            string cnt = cmd.ExecuteScalar().ToString();
            contemp = new MySqlConnection(con_temp);
            contemp.Open();
            if (cnt == "0") //If Table Not Found,Create Dynamic Table
            {

                string sql = string.Format(@"CREATE TABLE `{0}` (
                                    `SlNo` int(11) NOT NULL AUTO_INCREMENT,
                                    `ProductId` int(11) DEFAULT NULL,
                                    `SeriesId` int(11) DEFAULT NULL,
                                    `Qty` int(11) DEFAULT NULL,
                                    `LogDate` varchar(200) DEFAULT NULL,
                                    `PHV` varchar(4000) DEFAULT NULL,
                                    `CHV` varchar(4000) DEFAULT NULL,
                                     PRIMARY KEY(`SlNo`)
                                    ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = latin1; ", TableName);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();

                //Temp Table Backup

                cmd.Connection = contemp;
                string resulttemp = "";
                string sqlcnttemp = string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{1}' AND table_name = '{0}'", TableName, databasenametemp);
                cmd.CommandText = sqlcnttemp;
                string cnttemp = cmd.ExecuteScalar().ToString();

                if (cnttemp == "0") //If Table Not Found, Create Dynamic Table
                {

                    string sqltemp = string.Format(@"CREATE TABLE `{0}` (
                                   `SlNo` int(11) NOT NULL AUTO_INCREMENT,
                                    `ProductId` int(11) DEFAULT NULL,
                                    `SeriesId` int(11) DEFAULT NULL,
                                    `Qty` int(11) DEFAULT NULL,
                                    `LogDate` varchar(200) DEFAULT NULL,
                                    `PHV` varchar(4000) DEFAULT NULL,
                                    `CHV` varchar(4000) DEFAULT NULL,
                                     PRIMARY KEY(`SlNo`)
                                    ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = latin1; ", TableName);
                    cmd.CommandText = sqltemp;
                    resulttemp = cmd.ExecuteNonQuery().ToString();
                }

            }



            cmd.Connection = con;
            string sqlclogcs = string.Format("insert into {0} (ProductId,SeriesId,Qty,LogDate,PHV,CHV)values({1},{2},{3},'{4}','{5}','{6}')", TableName, ProductId, SeriesId, Qty, LogDate, PHV, CHV);
            cmd.CommandText = sqlclogcs;
            result = cmd.ExecuteNonQuery().ToString();



            //Temp Table
            cmd.Connection = contemp;
            string sqlclogcsTemp = string.Format("insert into {0} (ProductId,SeriesId,Qty,LogDate,PHV,CHV)values({1},{2},{3},'{4}','{5}','{6}')", TableName, ProductId, SeriesId, Qty, LogDate, PHV, CHV);
            cmd.CommandText = sqlclogcsTemp;
            result = cmd.ExecuteNonQuery().ToString();
            contemp.Close();
            con.Close();
            return result;
        }
        public DataTable GetMilkTransaction_FarmerId(int FarmerId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sqlvd = string.Format("select villagedairy.VDId,villagedairy.Name from villagedairy inner join farmermaster on farmermaster.VDId=villagedairy.VDId where farmermaster.FarmerId={0}", FarmerId);
            cmd.CommandText = sqlvd;
            adp = new MySqlDataAdapter(cmd);
            DataTable tabvd = new DataTable();
            adp.Fill(tabvd);

            string sql = string.Format("select villagedairy.Name,{0}.LogDate,{0}.ML from {0} inner join villagedairy on villagedairy.VDId={2}  where {0}.FarmerId={1}", tabvd.Rows[0]["VDId"].ToString() + "_" + tabvd.Rows[0]["VDId"].ToString(), FarmerId, int.Parse(tabvd.Rows[0]["VDId"].ToString()));
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public DataTable GetMilkPayment_FarmerId(int FarmerId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("SELECT * from farmerwallet where FarmerId={0}", FarmerId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }

        public DataTable GetBalance_FarmerId(int FarmerId)
        {
            OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("SELECT * from farmermaster where FarmerId={0}", FarmerId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }

        public string ShaKeyGeneration(string FarmerId, string Qty, string LogDate)
        {
            try
            {
                string data = FarmerId + "," + Qty + "," + LogDate;
                string strFilePath = HttpContext.Current.Server.MapPath("data.txt");
                if (File.Exists(strFilePath))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(strFilePath);
                }
                FileStream fp = new FileStream(strFilePath, FileMode.Create);
                StreamWriter wr = new StreamWriter(fp);
                wr.WriteLine(data);
                wr.Close();
                fp.Close();

                ReadOnlyCollection<byte> hash = Sha384mManaged.HashFile(File.OpenRead(strFilePath));

                return Util.ArrayToString(hash);
            }
            catch
            {
                return null;
            }
        }

        public string ShaKeyGeneration(string FarmerId, string Qty, string LogDate, string PHV)
        {
            try
            {
                string data = FarmerId + ","  +  Qty + "," + LogDate + "," + PHV;
                string strFilePath = HttpContext.Current.Server.MapPath("data.txt");
                if (File.Exists(strFilePath))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(strFilePath);
                }
                FileStream fp = new FileStream(strFilePath, FileMode.Create);
                StreamWriter wr = new StreamWriter(fp);
                wr.WriteLine(data);
                wr.Close();
                fp.Close();

                ReadOnlyCollection<byte> hash = Sha384mManaged.HashFile(File.OpenRead(strFilePath));

                return Util.ArrayToString(hash);
            }
            catch
            {
                return null;
            }
        }

        public string ShaKeyGeneration(string FarmerId,string SeriesId, string Qty, string LogDate, string PHV)
        {
            try
            {
                string data = FarmerId + "," + SeriesId + "," + Qty + "," + LogDate + "," + PHV;
                string strFilePath = HttpContext.Current.Server.MapPath("data.txt");
                if (File.Exists(strFilePath))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(strFilePath);
                }
                FileStream fp = new FileStream(strFilePath, FileMode.Create);
                StreamWriter wr = new StreamWriter(fp);
                wr.WriteLine(data);
                wr.Close();
                fp.Close();

                ReadOnlyCollection<byte> hash = Sha384mManaged.HashFile(File.OpenRead(strFilePath));

                return Util.ArrayToString(hash);
            }
            catch
            {
                return null;
            }
        }
    }
}