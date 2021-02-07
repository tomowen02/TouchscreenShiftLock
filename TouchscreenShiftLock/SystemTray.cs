using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TouchscreenShiftLock.Properties;
using WindowsInput;
using WindowsInput.Native;

namespace TouchscreenShiftLock
{
    class SystemTray : ApplicationContext
    {
       private static NotifyIcon trayIcon;

       public SystemTray()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };

            trayIcon.Click += new System.EventHandler(TrayIcon_Click);
        }

        private static void TrayIcon_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Do you want to enable shift lock for 5 seconds?", "Shift Lock", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
                {
                    ShiftLock();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error occured");;
            }
            
        }

        private static void ShiftLock()
        {
            InputSimulator InputSimulator = new InputSimulator();
            VirtualKeyCode keyCode = VirtualKeyCode.SHIFT;

            InputSimulator.Keyboard.KeyDown(keyCode); // Hold the key down
            Thread.Sleep(5000); // Wait 5 seconds
            InputSimulator.Keyboard.KeyUp(keyCode); // Release the key
            MessageBox.Show("Shift lock has ended", "Shift Lock", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private static void Exit(object sender, EventArgs e)
        {
            try
            {
                // Hide tray icon, otherwise it will remain shown until user mouses over it
                trayIcon.Visible = false;

                Application.Exit();
            }
            catch (Exception)
            {

                MessageBox.Show("Unknown error occured");;
            }
            
        }
    }
}
