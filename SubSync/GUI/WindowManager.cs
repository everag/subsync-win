using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubSync.GUI
{
    public static class WindowManager
    {
        private static Object oLock = new Object();

        public static void OpenWindow(Type type)
        {
            if (!typeof(Form).IsAssignableFrom(type))
                throw new ArgumentException("Type is not of a Form");

            lock (oLock)
            {
                var openedForm = (Application.OpenForms[type.Name] as Form);

                if (openedForm != null)
                {
                    if (!openedForm.Visible)
                    {
                        openedForm.Show();
                    }
                    else if (openedForm.WindowState == FormWindowState.Minimized)
                    {
                        openedForm.WindowState = FormWindowState.Normal;
                    }

                    openedForm.BringToFront();
                }
                else
                {
                    var newForm = (Form) Activator.CreateInstance(type);
                    
                    newForm.Show();
                }
            }
        }
    }
}
