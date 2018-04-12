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
            command.CommandText = String.Format(@"SELECT serial_no,inspection_time,wip_entity_name,sdcm,cct,x,y,Box_LOT_NO,NC12,result FROM v_tester_measurements_all WHERE wip_entity_name = @Zlecenie AND result='OK' ORDER BY inspection_time DESC;");
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
            command.CommandText = String.Format(@"SELECT serial_no,inspection_time,wip_entity_name,sdcm,cct,x,y,Box_LOT_NO,NC12,result FROM v_tester_measurements_all WHERE serial_no = @serial AND result='OK' ORDER BY inspection_time DESC;");
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
            command.CommandText = String.Format(@"SELECT serial_no,inspection_time,wip_entity_name,sdcm,cct,x,y,Box_LOT_NO,NC12,result FROM v_tester_measurements_all WHERE Box_LOT_NO = @box AND result='OK' ORDER BY inspection_time DESC;");
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



            return sqlTableLot.Rows[0]["NC12_wyrobu"].ToString();
        }
    }
}
