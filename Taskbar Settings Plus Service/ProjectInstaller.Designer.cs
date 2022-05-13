
namespace Taskbar_Settings_Plus_Service
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TaskbarSettingsPlusServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.TaskbarSettingsPlusServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // TaskbarSettingsPlusServiceProcessInstaller
            // 
            this.TaskbarSettingsPlusServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.TaskbarSettingsPlusServiceProcessInstaller.Password = null;
            this.TaskbarSettingsPlusServiceProcessInstaller.Username = null;
            // 
            // TaskbarSettingsPlusServiceInstaller
            // 
            this.TaskbarSettingsPlusServiceInstaller.Description = "Taskbar Settings Plus";
            this.TaskbarSettingsPlusServiceInstaller.DisplayName = "Taskbar Settings Plus";
            this.TaskbarSettingsPlusServiceInstaller.ServiceName = "Taskbar Settings Plus";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.TaskbarSettingsPlusServiceProcessInstaller,
            this.TaskbarSettingsPlusServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller TaskbarSettingsPlusServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller TaskbarSettingsPlusServiceInstaller;
    }
}