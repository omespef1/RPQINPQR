using RPQINPQR.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RPQINPQR.DAO
{
    public class DAOFaClien
    {
        public static FaClien GetFaClien(int emp_codi, string cli_coda)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("  SELECT fc.EMP_CODI,                                  ");
            sql.Append("  fc.CLI_CODA,                                         ");
            sql.Append("  fd.ARB_CSUC,                                         ");
            sql.Append("  ga.ARB_NOMB,                                          ");
            sql.Append("  fc.TIP_CODI,                                         ");
            sql.Append("  gt.TIP_NOMB,                                         ");
            sql.Append("  fc.CLI_NOMB,                                         ");
            sql.Append("  fc.CLI_APEL,                                         ");
            sql.Append("  fd.DCL_NTEL,                                         ");
            sql.Append("  fd.DCL_MAIL,                                         ");
            sql.Append("  fd.DCL_DIRE,                                         ");
            sql.Append("  fd.PAI_CODI,                                         ");
            sql.Append("  fd.DEP_CODI,                                         ");
            sql.Append("  gd.REG_CODI,                                         ");
            sql.Append("  fd.MUN_CODI ,                                         ");
            sql.Append("  gi.TIP_ABRE                                            ");
            sql.Append("  FROM   FA_CLIEN fc                                   ");
            sql.Append("  INNER JOIN FA_DCLIE fd                               ");
            sql.Append("       ON fc.EMP_CODI = fd.EMP_CODI                    ");
            sql.Append("       AND fc.CLI_CODI = FD.CLI_CODI                   ");
            sql.Append("  INNER JOIN GN_TIPDO gt                               ");
            sql.Append("       ON fc.TIP_CODI = gt.TIP_CODI                    ");
            sql.Append("  INNER JOIN GN_PAISE gp                               ");
            sql.Append("       ON fd.PAI_CODI = gp.PAI_CODI                    ");
            sql.Append("  INNER JOIN GN_DEPAR gd                               ");
            sql.Append("       ON fd.PAI_CODI = fd.PAI_CODI                    ");
            sql.Append("       AND fd.DEP_CODI = gd.DEP_CODI                   ");
            sql.Append("  INNER JOIN GN_MUNIC gm                               ");
            sql.Append("       ON fd.PAI_CODI = gm.PAI_CODI                    ");
            sql.Append("       AND fd.DEP_CODI = gm.DEP_CODI                   ");
            sql.Append("       AND FD.MUN_CODI = GM.MUN_CODI                   ");
            sql.Append("  INNER JOIN GN_ARBOL ga         ");
            sql.Append("  ON fd.ARB_SUCU = ga.ARB_CONT   ");
            sql.Append("  INNER JOIN GN_TIPDO gi ON fc.TIP_CODI = gi.TIP_CODI   ");
            sql.Append("  AND ga.EMP_CODI = fd.EMP_CODI  ");
            sql.Append("  AND ga.TAR_CODI = 2            ");


            sql.Append("  WHERE fd.EMP_CODI = @EMP_CODI                        ");
            sql.Append("  AND FC.CLI_CODA =   @CLI_CODA                        ");
            sql.Append("  AND fd.DCL_CODD = 1                                  ");
            List<SQLParams> sQLParams = new List<SQLParams>()
            {
                new SQLParams("EMP_CODI",emp_codi),
                new SQLParams("CLI_CODA",cli_coda)
            };
            return new DbConnection().Get<FaClien>(sql.ToString(), sQLParams);

        }
    }
}