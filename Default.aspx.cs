using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Windows.Forms;

//实现本文件夹下 功能说明.png  图中所描述的功能
//利用reportview 报表实现 关于该报表功能，请参照网络说明

public partial class _Default : System.Web.UI.Page 
{
    //oracle连接取得
    public static OracleConnection conn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
   
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

       string ssSQL = " select " +
              "  b.ly_mc  储备来源 , " +
              "  sum(a.ZD_MJ) 已入库面积 , " +
              "  a.cb_ly   " +
              " from t_scdk a, " +
              " t_cblyzd  b " +
              " where a.cb_ly = b.ly_dm " +
              " and  to_char(NR_CB_SJ,'yyyy' )= '" + DropDownList1.Text + "'" +
              " group by b.ly_mc,a.cb_ly "+
              " order by a.cb_ly";
        conn.Open();
        OracleCommand cmd = new OracleCommand(ssSQL, conn);
        OracleDataAdapter ad = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        conn.Close();
        ReportDataSource rds = new ReportDataSource();
        rds.Name = "T_data";
        rds.Value = dt;

        string ssSQL1 = " select " +
                  " b.ly_mc as 资金来源, " +
                  " a.zj_ly as 资金来源编码, " +
                  " sum(a.rz_je) as 本年度新增, " +
                  " sum(a.ys_je)as 本年度预算 " +
                  " from T_TDRZ a,T_ZJLYZD b " +
                   "where a.zj_ly = b.ly_dm " +
                  " and a.nf ='" + DropDownList1.Text + "'" +
                  " group by a.zj_ly,b.ly_mc " +
                  " order by 资金来源编码  ";
        conn.Open();
        OracleCommand cmd1 = new OracleCommand(ssSQL1, conn);
        OracleDataAdapter ad1 = new OracleDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        ad1.Fill(dt1);
        conn.Close();

        ReportDataSource rds1 = new ReportDataSource();
        rds1.Name = "资金来源";
        rds1.Value = dt1;

        string ssSQL2 = "select sum(zd_mj) as 已入库 from t_scdk  where  to_char(NR_CB_SJ,'yyyy' ) = '" + DropDownList1.Text + "'";
        conn.Open();
        OracleCommand cmd2 = new OracleCommand(ssSQL2, conn);
        OracleDataAdapter ad2 = new OracleDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        ad2.Fill(dt2);
        conn.Close();

        string ssSQL3 = "select sum(zd_mj) as 收储中 from t_nscdk   where  to_char(SB_SJ,'yyyy' ) = '" + DropDownList1.Text + "'";
        conn.Open();
        OracleCommand cmd3 = new OracleCommand(ssSQL3, conn);
        OracleDataAdapter ad3 = new OracleDataAdapter(cmd3);
        DataTable dt3= new DataTable();
        ad3.Fill(dt3);
        conn.Close();

        string ssSQL4 = "select sum(zd_mj) as 已供应 from  T_TDGY   where  to_char(GY_SJ,'yyyy' ) = '" + DropDownList1.Text + "'";
        conn.Open();
        OracleCommand cmd4 = new OracleCommand(ssSQL4, conn);
        OracleDataAdapter ad4 = new OracleDataAdapter(cmd4);
        DataTable dt4 = new DataTable();
        ad4.Fill(dt4);
        conn.Close();

        string ssSQL5 = "select sum(DY_MJ) as 已抵押  from T_DY   where  to_char(JLCRSJ,'yyyy' ) = '" + DropDownList1.Text + "'";
        conn.Open();
        OracleCommand cmd5 = new OracleCommand(ssSQL5, conn);
        OracleDataAdapter ad5= new OracleDataAdapter(cmd5);
        DataTable dt5 = new DataTable();
        ad5.Fill(dt5);
        conn.Close();

        DataTable tblDatas = new DataTable();
        DataColumn dc = null;

        dc = tblDatas.Columns.Add("收储中", Type.GetType("System.Double"));
        dc = tblDatas.Columns.Add("已入库", Type.GetType("System.Double"));
        dc = tblDatas.Columns.Add("已抵押", Type.GetType("System.Double"));
        dc = tblDatas.Columns.Add("已供应", Type.GetType("System.Double"));

        DataRow newRow;
        newRow = tblDatas.NewRow();
        newRow[0]= dt2.Rows[0][0];
        newRow[1] = dt3.Rows[0][0];
        newRow[2] = dt4.Rows[0][0];
        newRow[3] = dt5.Rows[0][0];
        tblDatas.Rows.Add(newRow);
        
        ReportDataSource rds2 = new ReportDataSource();
        rds2.Name = "土地储备情况";
        rds2.Value = tblDatas;

        string ssSQL7 = " select  " +
         " (case when   gy_fs='01' then  '划拨' when   gy_fs='02' then  '招牌挂出让'  when   gy_fs='03' then  '协议出让' end ) as 供应方式," +
         " sum(case when substr( td_yt , 1,  2 )  in ('05') then zd_mj end) as 商务用地," +
         " sum(case when substr( td_yt , 1,  2 )  in ('06') then zd_mj end) as 工矿仓储," +
         " sum(case when substr( td_yt , 1,  2 )  in ('07') then zd_mj end) as 住宅用地," +
         " sum(case when substr( td_yt , 1,  2 ) not in ('05','06','07')  then zd_mj end) as 其他用地" +
         " from  t_tdgy" +
         " where   to_char(GY_SJ,'yyyy' ) = '" + DropDownList1.Text + "'" +
         " group by gy_fs";
        conn.Open();
        OracleCommand cmd7 = new OracleCommand(ssSQL7, conn);
        OracleDataAdapter ad7 = new OracleDataAdapter(cmd7);
        DataTable dt7= new DataTable();
        ad7.Fill(dt7);
        conn.Close();
        ReportDataSource rds7 = new ReportDataSource();
        rds7.Name = "供应情况";
        rds7.Value = dt7;

        string ssSQL8 = " select sum(a.RZ_JE) 融资金额,a.rz_fs  融资方式 ," +
        " b.RZFS_MC  融资方式名称" +
        " from T_TDRZ a, T_RZFS b  " +
        " where a.rz_fs = b.RZFS_DM" +
        " and to_char(CJ_SJ,'yyyy' ) = '" + DropDownList1.Text + "'" +
        " group by a.RZ_FS,b.RZFS_MC ";
        conn.Open();
        OracleCommand cmd8 = new OracleCommand(ssSQL8, conn);
        OracleDataAdapter ad8 = new OracleDataAdapter(cmd8);
        DataTable dt8= new DataTable();
        ad8.Fill(dt8);
        conn.Close();

        ReportDataSource rds8 = new ReportDataSource();
        rds8.Name = "土地融资";
        rds8.Value = dt8;


        string ssSQL9 = " select sum(b.DY_MJ) 抵押面积," +
        " a.rz_fs  融资方式," +
        " c.RZFS_MC  融资方式名称" +
        " from T_TDRZ a,T_DY b ,T_RZFS c" +
        " where " +
        " a.rzbh = b.rzbh" +
        " and c.RZFS_DM = a.rz_fs" +
        " and to_char(a.CJ_SJ,'yyyy' ) = '" + DropDownList1.Text + "'" +
        " group by a.RZ_FS,c.RZFS_MC";
        conn.Open();
        OracleCommand cmd9 = new OracleCommand(ssSQL9, conn);
        OracleDataAdapter ad9 = new OracleDataAdapter(cmd9);
        DataTable dt9 = new DataTable();
        ad9.Fill(dt9);
        conn.Close();

        ReportDataSource rds9 = new ReportDataSource();
        rds9.Name = "融资抵押面积";
        rds9.Value = dt9;

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(rds);
        ReportViewer1.LocalReport.DataSources.Add(rds1);
        ReportViewer1.LocalReport.DataSources.Add(rds2);
        ReportViewer1.LocalReport.DataSources.Add(rds7);
        ReportViewer1.LocalReport.DataSources.Add(rds8);
        ReportViewer1.LocalReport.DataSources.Add(rds9);
        ReportViewer1.LocalReport.Refresh();
    }
}
