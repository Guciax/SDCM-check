using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCM_check
{
    public class SqlOperations
    {
        public static DataTable GetMeasurementsForLot(string lot)
        {
            DataTable sqlTableLot = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT serial_no,inspection_time,wip_entity_name,sdcm,cct,x,y,lm,lm_w,cri,cct,v,Box_LOT_NO,NC12,result FROM v_tester_measurements_all WHERE wip_entity_name = @Zlecenie AND result='OK' ORDER BY inspection_time DESC;");
            command.Parameters.AddWithValue("@Zlecenie", lot);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTableLot);

            return sqlTableLot;
        }

        public static DataTable GetMeasurementsForPcb(string pcbSerial)
        {
            DataTable sqlTableLot = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT serial_no,inspection_time,wip_entity_name,sdcm,cct,lm,lm_w,cri,cct,v,x,y,Box_LOT_NO,NC12,result FROM v_tester_measurements_all WHERE serial_no = @serial AND result='OK' ORDER BY inspection_time DESC;");
            command.Parameters.AddWithValue("@serial", pcbSerial);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTableLot);

            return sqlTableLot;
        }

        public static DataTable GetMeasurementsForBox(string boxId)
        {
            DataTable sqlTableLot = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT serial_no,inspection_time,wip_entity_name,sdcm,cct,lm,lm_w,cri,cct,v,x,y,Box_LOT_NO,NC12,result FROM v_tester_measurements_all WHERE Box_LOT_NO = @box AND result='OK' ORDER BY inspection_time DESC;");
            command.Parameters.AddWithValue("@box", boxId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTableLot);

            return sqlTableLot;
        }

        public static DataTable GetArchivedMeasurementsForLot(string lot)
        {
            DataTable sqlTableLot = new DataTable();
           
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 600;
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT serial_no,inspection_time,wip_entity_name,sdcm,cct,x,y,Box_LOT_NO,NC12,result FROM v_tester_measurements_all2 WHERE wip_entity_name = @Zlecenie AND result='OK' ORDER BY inspection_time DESC;");
            command.Parameters.AddWithValue("@Zlecenie", lot);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTableLot);

            return sqlTableLot;
        }

        public static DataTable GetArchivedMeasurementsForPcb(string pcbSerial)
        {
            DataTable sqlTableLot = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 600;
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT serial_no,inspection_time,wip_entity_name,sdcm,cct,x,y,Box_LOT_NO,NC12,result FROM v_tester_measurements_all2 WHERE serial_no = @serial AND result='OK' ORDER BY inspection_time DESC;");
            command.Parameters.AddWithValue("@serial", pcbSerial);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTableLot);

            return sqlTableLot;
        }

        public static DataTable GetArchivedMeasurementsForBox(string boxId)
        {
            DataTable sqlTableLot = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60000;
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT serial_no,inspection_time,wip_entity_name,sdcm,cct,x,y,Box_LOT_NO,NC12,result FROM v_tester_measurements_all2 WHERE Box_LOT_NO = @box AND result='OK' ORDER BY inspection_time DESC;");
            command.Parameters.AddWithValue("@box", boxId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTableLot);

            return sqlTableLot;
        }

        public static string GetModelIdFromLot(string lot)
        {
            DataTable sqlTableLot = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT Nr_Zlecenia_Produkcyjnego,NC12_wyrobu FROM tb_Zlecenia_produkcyjne WHERE Nr_Zlecenia_Produkcyjnego=@lot;");
            command.Parameters.AddWithValue("@lot", lot);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTableLot);
            if (sqlTableLot.Rows.Count > 0)
            {
                return sqlTableLot.Rows[0]["NC12_wyrobu"].ToString();
            }
            else
            {
                return null;
            }
        }

        public static Dictionary<string, ModelSpecification> AddModelOpticalSpecFromDb(Dictionary<string, ModelSpecification> inputData)
        {
            DataTable sqlTableLot = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT  MODEL_ID,CCT,Vf_min,Vf_max,lm_min,lm_max,lmW_min,CRI_min FROM tb_MES_models_ex2;");
            
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTableLot);

            foreach (DataRow row in sqlTableLot.Rows)
            {
                string model = row["MODEL_ID"].ToString();

                double vfMin = 0;
                double vfMax = 0;
                double lmMin = 0;
                double lmMax = 0;
                double criMin = 0;
                double lmWMin = 0;
                double cct = 0;

                double.TryParse(row["Vf_min"].ToString(), out vfMin);
                double.TryParse(row["Vf_max"].ToString(), out vfMax);
                double.TryParse(row["lm_min"].ToString(), out lmMin);
                double.TryParse(row["lm_max"].ToString(), out lmMax);
                double.TryParse(row["CRI_min"].ToString(), out criMin);
                double.TryParse(row["lmW_min"].ToString(), out lmWMin);
                double.TryParse(row["CCT"].ToString(), out cct);


                if (!inputData.ContainsKey(model))
                {
                    inputData.Add(model, new ModelSpecification(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
                }
                double minCct = Math.Round(cct * 0.9, 0);
                double maxCct = Math.Round(cct * 1.1, 0);

                inputData[model].CctMin = minCct;
                inputData[model].CctMax = maxCct;
                inputData[model].Vf_Min = vfMin;
                inputData[model].Vf_Max = vfMax;
                inputData[model].Lm_Min = lmMin;
                inputData[model].Lm_Max = lmMax;
                inputData[model].CRI_Min = criMin;
                inputData[model].LmW_Min = lmWMin;

            }

            return inputData;
        }

    }
}
