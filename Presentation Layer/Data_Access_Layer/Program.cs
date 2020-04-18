using Data_Access_Layer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Layer
{
    static class Program
    {

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_Main());
        }

        public static bool IsTextValide(this TextEdit txt)
        {
            if (txt.Text.Trim() == string.Empty)
            {
                txt.ErrorText = Frm_Master.ErrorText;
                return false;
            }
            return true;
        }
        public static bool IsDateValide(this DateEdit Dte)
        {
            if (Dte.DateTime.Year<1900)
            {
                Dte.ErrorText = Frm_Master.ErrorText;
                return false;
            }
            return true;
        }
        public static bool IsEditValueValide(this LookUpEditBase Lkp)
        {
            if (Lkp.IsEditValueOfTypeInt() == false|| Convert.ToInt32(Lkp.EditValue)==0)
            {
                Lkp.ErrorText = Frm_Master.ErrorText;
                return false;
            }
            return true;
        }
        public static bool IsEditValueOfTypeInt(this LookUpEditBase edit)
        {
            var val = edit.EditValue;
            return (val is int || val is byte);
        }

    }
}
