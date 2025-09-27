using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlenderBender.Services
{
    /// <summary>
    /// Service responsible for toolbar application behavior and window management.
    /// Handles centering forms and toolbar-style positioning.
    /// </summary>
    public class ToolbarService
    {
        /// <summary>
        /// Centers a form on the screen.
        /// </summary>
        /// <param name="form">Form to center</param>
        public void CenterFormOnScreen(Form form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var screen = Screen.PrimaryScreen.WorkingArea;
            var x = (screen.Width - form.Width) / 2;
            var y = (screen.Height - form.Height) / 2;
            
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(x, y);
        }

        /// <summary>
        /// Positions a form in toolbar style (typically at the edge of screen).
        /// </summary>
        /// <param name="form">Form to position</param>
        /// <param name="position">Toolbar position preference</param>
        public void PositionAsToolbar(Form form, ToolbarPosition position = ToolbarPosition.BottomRight)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var screen = Screen.PrimaryScreen.WorkingArea;
            var margin = 20; // Distance from screen edge

            Point location = position switch
            {
                ToolbarPosition.TopLeft => new Point(margin, margin),
                ToolbarPosition.TopRight => new Point(screen.Width - form.Width - margin, margin),
                ToolbarPosition.BottomLeft => new Point(margin, screen.Height - form.Height - margin),
                ToolbarPosition.BottomRight => new Point(screen.Width - form.Width - margin, 
                                                        screen.Height - form.Height - margin),
                ToolbarPosition.TopCenter => new Point((screen.Width - form.Width) / 2, margin),
                ToolbarPosition.BottomCenter => new Point((screen.Width - form.Width) / 2, 
                                                         screen.Height - form.Height - margin),
                _ => new Point(screen.Width - form.Width - margin, screen.Height - form.Height - margin)
            };

            form.StartPosition = FormStartPosition.Manual;
            form.Location = location;
        }

        /// <summary>
        /// Configures a form for toolbar behavior with appropriate properties.
        /// </summary>
        /// <param name="form">Form to configure</param>
        /// <param name="alwaysOnTop">Whether form should stay on top</param>
        /// <param name="showInTaskbar">Whether form should appear in taskbar</param>
        public void ConfigureAsToolbar(Form form, bool alwaysOnTop = true, bool showInTaskbar = false)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            form.TopMost = alwaysOnTop;
            form.ShowInTaskbar = showInTaskbar;
            form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
        }

        /// <summary>
        /// Switches between toolbar mode and normal window mode.
        /// </summary>
        /// <param name="form">Form to switch</param>
        /// <param name="toolbarMode">True for toolbar mode, false for normal mode</param>
        /// <param name="toolbarHeight">Height when in toolbar mode</param>
        /// <param name="normalHeight">Height when in normal mode</param>
        public void ToggleToolbarMode(Form form, bool toolbarMode, int toolbarHeight = 156, int normalHeight = 632)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            if (toolbarMode)
            {
                // Switch to toolbar mode
                form.WindowState = FormWindowState.Normal;
                form.Height = toolbarHeight;
                PositionAsToolbar(form, ToolbarPosition.BottomRight);
                ConfigureAsToolbar(form, alwaysOnTop: true, showInTaskbar: false);
            }
            else
            {
                // Switch to normal mode
                form.WindowState = FormWindowState.Normal;
                form.Height = normalHeight;
                CenterFormOnScreen(form);
                
                // Reset normal window properties
                form.TopMost = false;
                form.ShowInTaskbar = true;
                form.FormBorderStyle = FormBorderStyle.Sizable;
                form.MaximizeBox = true;
                form.MinimizeBox = true;
            }
        }

        /// <summary>
        /// Ensures a form is visible within screen boundaries.
        /// </summary>
        /// <param name="form">Form to check and adjust</param>
        public void EnsureFormVisible(Form form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var screen = Screen.PrimaryScreen.WorkingArea;
            var location = form.Location;
            var size = form.Size;

            // Adjust if form is outside screen boundaries
            if (location.X < screen.Left)
                location.X = screen.Left;
            else if (location.X + size.Width > screen.Right)
                location.X = screen.Right - size.Width;

            if (location.Y < screen.Top)
                location.Y = screen.Top;
            else if (location.Y + size.Height > screen.Bottom)
                location.Y = screen.Bottom - size.Height;

            form.Location = location;
        }

        /// <summary>
        /// Creates a system tray icon for the application.
        /// </summary>
        /// <param name="form">Main form to associate with tray icon</param>
        /// <param name="icon">Icon to use (optional)</param>
        /// <param name="toolTipText">Tooltip text for the tray icon</param>
        /// <returns>Configured NotifyIcon</returns>
        public NotifyIcon CreateSystemTrayIcon(Form form, Icon icon = null, string toolTipText = "e-Shop Assistant")
        {
            var notifyIcon = new NotifyIcon
            {
                Icon = icon ?? form.Icon ?? SystemIcons.Application,
                Text = toolTipText,
                Visible = true
            };

            // Create context menu for tray icon
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Show", null, (s, e) => 
            {
                form.Show();
                form.WindowState = FormWindowState.Normal;
                form.BringToFront();
            });
            contextMenu.Items.Add("-"); // Separator
            contextMenu.Items.Add("Exit", null, (s, e) => Application.Exit());

            notifyIcon.ContextMenuStrip = contextMenu;

            // Double-click to show form
            notifyIcon.DoubleClick += (s, e) =>
            {
                form.Show();
                form.WindowState = FormWindowState.Normal;
                form.BringToFront();
            };

            return notifyIcon;
        }
    }

    /// <summary>
    /// Enumeration for toolbar positioning options.
    /// </summary>
    public enum ToolbarPosition
    {
        TopLeft,
        TopRight,
        TopCenter,
        BottomLeft,
        BottomRight,
        BottomCenter
    }
}