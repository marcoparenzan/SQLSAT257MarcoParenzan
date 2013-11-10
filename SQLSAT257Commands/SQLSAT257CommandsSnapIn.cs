using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace SQLSAT257Commands
{
    /// <summary>
    /// Create a PowerShell snap-in for the Select-Str cmdlet.
    /// </summary>
    [RunInstaller(true)]
    public class SQLSAT257CommandsSnapIn : CustomPSSnapIn
    {
        /// <summary>
        /// Specify the name of the PowerShell snap-in.
        /// </summary>
        public override string Name
        {
            get
            {
                return "SQLSAT257CommandsSnapIn";
            }
        }

        /// <summary>
        /// Specify the vendor of the PowerShell snap-in.
        /// </summary>
        public override string Vendor
        {
            get
            {
                return "SQLSAT257";
            }
        }

        /// <summary>
        /// Specify the localization resource information for the vendor. 
        /// Use the format: SnapinName,VendorName.
        /// </summary>
        public override string VendorResource
        {
            get
            {
                return "SQLSAT257CommandsSnapIn,SQLSAT257";
            }
        }

        /// <summary>
        /// Specifiy the description of the PowerShell snap-in.
        /// </summary>
        public override string Description
        {
            get
            {
                return "SQLSAT257CommandsSnapIn";
            }
        }

        /// <summary>
        /// Specify the localization resource information for the description. 
        /// Use the format: SnapinName,Description. 

        /// </summary>
        public override string DescriptionResource
        {
            get
            {
                return "SQLSAT257CommandsSnapIn,SQLSAT257";
            }
        }

        private Collection<CmdletConfigurationEntry> _cmdlets;
        public override Collection<CmdletConfigurationEntry> Cmdlets
        {
            get
            {
                if (_cmdlets == null)
                {
                    _cmdlets = new Collection<CmdletConfigurationEntry>();
                    _cmdlets.Add(new CmdletConfigurationEntry("Get-Json", typeof(GetJsonCommand), null));
                    _cmdlets.Add(new CmdletConfigurationEntry("Get-Sql", typeof(GetSqlCommand), null));
                    _cmdlets.Add(new CmdletConfigurationEntry("Format-Pdf", typeof(FormatPdfCommand), null));
                }
                return _cmdlets;
            }
        }
    }
}
